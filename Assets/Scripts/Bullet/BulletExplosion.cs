using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    private ObjectPool particleEffectPool;
    private ParticleSystem pes;
    private void Start()
    {
        pes = GetComponent<ParticleSystem>();
        particleEffectPool = GameObject.FindGameObjectWithTag("ParticleEffectPool").GetComponent<ObjectPool>();
        if(pes != null)
            Invoke(nameof(ReturnToPool), pes.main.duration);
    }

    private void OnEnable()
    {
        if(pes != null)
            Invoke(nameof(ReturnToPool), pes.main.duration);
    }

    private void ReturnToPool()
    {
        particleEffectPool.ReturnObject(this.gameObject);
    }
}
