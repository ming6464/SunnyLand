using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool isCheery;
    private Animator m_anim;
    private string ANIM_FEEDBACK = "Feedback";
    private BoxCollider2D m_coll;
    private int m_state;
    

    private void Start()
    {
        m_anim = GetComponent<Animator>();
        m_coll = GetComponent<BoxCollider2D>();
        if (isCheery) m_state = TagAndKey.STATE_CHERRY;
        else m_state = TagAndKey.STATE_GEM;
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
    public int STATE
    {
        get => m_state;
        set => m_state = value;
    }
}