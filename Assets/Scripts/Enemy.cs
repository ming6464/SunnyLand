using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected bool isDeath;
    protected Rigidbody2D rigid;
    public int score;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        UpdateActiveAnimator(false);
        rigid = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
    }
    public void End()
    {
        isDeath = true;
        anim.SetTrigger("Death");
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
    public void UpdateActiveAnimator(bool isActive)
    {
        anim.enabled = isActive;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(TagAndKey.T_MAINCAM) && !anim.enabled)
        {
            UpdateActiveAnimator(true);
        }
        if(col.gameObject.CompareTag(TagAndKey.T_DEATHZONE))
            End();
    }
    
}