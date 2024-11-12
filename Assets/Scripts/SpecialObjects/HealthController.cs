using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private Animator animator;
    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;
    public bool isEnemy = false;
    private ObjectPool pool;
    private void Awake()
    {
        currentHealth = maxHealth;
        //animator = GetComponent<Animator>();
        if(isEnemy) pool = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<ObjectPool>();
    }

    public void Setup()
    {
        currentHealth = maxHealth;
    }

    public void Deal(int dmg)
    {
        currentHealth -= dmg;
        if(currentHealth <= 0)
        {
            if(animator != null)
            {
                if(isEnemy) GetComponent<Collider2D>().enabled = false;
                animator.SetTrigger("Die");
                Invoke(nameof(Kill), 1f);
            }
            else
                Kill();
        }
            
    }

    private void Kill()
    {
        if(isEnemy && pool != null)
        {
            pool.ReturnObject(gameObject);
        }
        else
            Destroy(this.gameObject);
    }
}
