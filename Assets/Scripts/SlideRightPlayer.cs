using System;
using UnityEngine;

public class SlideRightPlayer : MonoBehaviour
{
    private Player m_parent;
    private BoxCollider2D m_boxCol;

    private void Start()
    {
        m_parent = GetComponentInParent<Player>();
        m_boxCol = GetComponent<BoxCollider2D>();
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

    public void UpdateSizeCol(string anim)
    {
        switch (anim)
        {
            case TagAndKey.A_PLAYER_JUMPUP:
                ChangeSizeCol(0.4516847f,-0.07400826f,0.07595825f,0.7384842f);
                break;
            case TagAndKey.A_PLAYER_FALL :
                ChangeSizeCol(0.4516847f,-0.05087413f,0.07595825f,1.043854f);
                break;
            case TagAndKey.A_PLAYER_CROUND:
                ChangeSizeCol(0.6562569f,-0.5061018f,0.04984283f,0.7112932f);
                break;
            default:
                ChangeSizeCol(0.4516847f,-0.2993535f,0.07595825f,1.12479f);
                break;
        }
    }

    private void ChangeSizeCol(float offX, float offY, float sizeX, float sizeY)
    {
        m_boxCol.offset = new Vector2(offX, offY);
        m_boxCol.size = new Vector2(sizeX, sizeY);
    }
}