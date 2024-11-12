using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint;

    [SerializeField] private PlayerMovement player;
    [SerializeField] private List<Transform> bulletPathPoints;
    [SerializeField] private ObjectPool bulletPool;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        bulletPool = GameObject.FindGameObjectWithTag("BulletPool").GetComponent<ObjectPool>();
        player = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        float mouseY = Input.mousePosition.y / player.screenHeight * 2f - 1f;
        float targetY = player.initialPosition.y + mouseY * player.movementSpeed;
        targetY = Mathf.Clamp(targetY, player.boundaryBottom, player.boundaryTop);
        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = bulletPool.GetObject();
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = Quaternion.identity;

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.pathPoints = bulletPathPoints;
        bulletScript.ResetBullet();
        bullet.GetComponent<Collider2D>().enabled = true;
    }
}
