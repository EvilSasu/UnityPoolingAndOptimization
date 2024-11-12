using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 5f;

    private ObjectPool objectPool;
    private HealthController health;
    [SerializeField] private HealthBar healthBar;

    private void Start()
    {
        health = GetComponent<HealthController>();
        objectPool = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<ObjectPool>();
        if(healthBar != null) healthBar.Initialize(transform, health);
    }

    private void OnEnable()
    {
        GetComponent<Collider2D>().enabled = true;
        if (healthBar != null) healthBar.Initialize(transform, health);
    }

    private void Update()
    {
        if (target != null && health.CurrentHealth > 0)
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        float directionX = target.position.x - transform.position.x;

        float step = moveSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + Mathf.Sign(directionX) * step, transform.position.y, transform.position.z);

        if (Mathf.Abs(transform.position.x - target.position.x) <= 0.1f)
        {
            ReturnToPool();
        }
    }


    private void ReturnToPool()
    {
        objectPool.ReturnObject(gameObject);
    }
}
