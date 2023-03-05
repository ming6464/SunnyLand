using UnityEngine;

public class SlideBottomPlayer : MonoBehaviour
{
    private Player m_parent;

    private void Start()
    {
        m_parent = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(TagAndKey.T_GROUND))
        {
            m_parent.isGround = true;
            m_parent.isHurt = false;
            m_parent.isJump = false;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(TagAndKey.T_GROUND))
        {
            m_parent.isGround = false;
        }
            
    }
}
