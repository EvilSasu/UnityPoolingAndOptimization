using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetEnemy : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        ResetMe();
    }

    private void OnDisable()
    {
        ResetMe();
    }

    public void ResetMe()
    {
        animator.StopPlayback();
        transform.rotation = Quaternion.identity;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
        animator.SetTrigger("Move");
    }
}
