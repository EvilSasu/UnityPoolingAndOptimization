using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool moveToTarget = false;
    public static float speed = 5f;
    public static float rotationSpeed = 200f;
    public static float proximityThreshold = 0.5f;
    public List<Transform> pathPoints;
    private int currentTargetIndex = 0;
    private ObjectPool bulletPool;
    private ObjectPool particleEffectPool;
    private Collider2D bulletCollider;

    public TrailRenderer trailRenderer;
    private HashSet<int> activatedMultipliers = new HashSet<int>();
    private int randomForMustHaveCollisionPoint;
    private void Start()
    {
        bulletPool = GameObject.FindGameObjectWithTag("BulletPool").GetComponent<ObjectPool>();
        particleEffectPool = GameObject.FindGameObjectWithTag("ParticleEffectPool").GetComponent<ObjectPool>();
        bulletCollider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        trailRenderer.gameObject.SetActive(true);
        randomForMustHaveCollisionPoint = Random.Range(8, 13);
    }

    private void OnDisable()
    {
        trailRenderer.Clear();
        trailRenderer.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (bulletCollider != null && !bulletCollider.enabled)
        {
            moveToTarget = true;
        }

        if (!moveToTarget)
        {
            Move();
            return;
        }

        if (currentTargetIndex >= pathPoints.Count)
        {
            ReturnToPool();
            return;
        }

        Transform targetPoint = pathPoints[currentTargetIndex];
        float distance = Vector2.Distance(transform.position, targetPoint.position);

        if (distance <= proximityThreshold)
        {
            TryActivateCollider();
            currentTargetIndex++;
        }

        if (currentTargetIndex < pathPoints.Count)
        {
            Vector2 direction = (targetPoint.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        }
    }

    private void Move()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ChangeRoad"))
        {
            moveToTarget = true;
        }
        else if (collision.CompareTag("Obstacle"))
        {
            Explode();
            collision.GetComponent<Obstacle>().DealDamage(1);
            ReturnToPool();
        }
        else if (collision.CompareTag("Enemy"))
        {
            Explode();
            collision.GetComponent<HealthController>().Deal(1);
            ReturnToPool();
        }
        else if (collision.CompareTag("Multiplier"))
        {
            int multiplierId = collision.GetInstanceID();
            if (!activatedMultipliers.Contains(multiplierId))
            {
                activatedMultipliers.Add(multiplierId);
                Multiplier mult = collision.GetComponent<Multiplier>();
                if (mult != null)
                    MultiplyBullet(mult.multiplierAmount - 1);
            }
        }
        else if (collision.CompareTag("BlackHole"))
        {
            ReturnToPool();
        }
    }

    public void ResetBullet()
    {
        moveToTarget = false;
        currentTargetIndex = 0;
        activatedMultipliers.Clear();
    }

    // Optimization
    private void TryActivateCollider()
    {
        if (bulletCollider.enabled) return;
        if (currentTargetIndex >= 11) return;

        if (currentTargetIndex >= randomForMustHaveCollisionPoint)
        {
            float activationFactor = 1f;
            if (currentTargetIndex == randomForMustHaveCollisionPoint)
                activationFactor = 0.5f;
            else if (currentTargetIndex == randomForMustHaveCollisionPoint + 1)
                activationFactor = 0.25f;
            else if (currentTargetIndex == randomForMustHaveCollisionPoint + 2)
                activationFactor = 0.125f;

            if (Random.Range(0f, 1f) < activationFactor)
            {
                Invoke(nameof(ActivateCollider), Random.Range(0f, 2f));
            }
        }
    }

    private void ActivateCollider()
    {
        bulletCollider.enabled = true;
    }

    private void MultiplyBullet(int multiplierAmount)
    {
        for(int i = 0; i < multiplierAmount; i++)
        {
            GameObject duplicateBullet = bulletPool.GetObject();
            duplicateBullet.transform.position = transform.position;
            duplicateBullet.transform.rotation = transform.rotation;

            Bullet duplicateBulletScript = duplicateBullet.GetComponent<Bullet>();
            duplicateBulletScript.pathPoints = new List<Transform>(pathPoints);
            duplicateBulletScript.ResetBullet();

            duplicateBulletScript.activatedMultipliers = new HashSet<int>(activatedMultipliers);
        }        
    }

    private void Explode()
    {
        if (particleEffectPool != null)
        {
            GameObject particleEffect = particleEffectPool.GetObject();
            particleEffect.transform.position = transform.position;
            particleEffect.transform.rotation = Quaternion.identity;

            //StartCoroutine(ReturnEffectToPool(particleEffect));
        }
    }
    /*private System.Collections.IEnumerator ReturnEffectToPool(GameObject particleEffect)
    {
        yield return new WaitForSeconds(particleEffect.GetComponent<ParticleSystem>().main.duration);
        particleEffectPool.ReturnObject(particleEffect);
    }*/

    private void ReturnToPool()
    {
        bulletPool.ReturnObject(gameObject);
    }
}
