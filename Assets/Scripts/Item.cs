using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Animator m_anim;
    private string ANIM_FEEDBACK = "Feedback";
    private BoxCollider2D m_coll;
    public int score;

    private void Start()
    {
        m_anim = GetComponent<Animator>();
        m_coll = GetComponent<BoxCollider2D>();
    }

    public void Collected()
    {
        m_anim.SetTrigger(ANIM_FEEDBACK);
        m_coll.enabled = false;
    }

    private void Feedback()
    {
        Destroy(this.gameObject);
    }
}