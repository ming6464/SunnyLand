using System;
using UnityEngine;

public class SlideRightPlayer : MonoBehaviour
{
    private Player m_parent;

    private void Start()
    {
        m_parent = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(TagAndKey.T_WALL))
        {
            m_parent.isBlockDirect = true;
        }
    }
    

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(TagAndKey.T_WALL))
        {
            m_parent.isBlockDirect = false;
        }
    }
}