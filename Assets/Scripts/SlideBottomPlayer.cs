using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideBottomPlayer : MonoBehaviour
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
    public void UpdateSizeCol(string anim)
    {
        switch (anim)
        {
            case TagAndKey.A_PLAYER_JUMPUP:
                ChangeSizeCol(-0.004343987f,-0.635777f,0.7406874f,0.06152898f);
                break;
            case TagAndKey.A_PLAYER_FALL :
                ChangeSizeCol(-0.004343987f,-0.7371492f,0.7406874f,0.0610953f);
                break;
            case TagAndKey.A_PLAYER_CROUND:
                ChangeSizeCol(0.0740025f,-0.9946055f,0.9234967f,0.0496527f);
                break;
            default:
                ChangeSizeCol(-0.02687848f,-0.9941244f,0.6956184f,0.03714526f);
                break;
        }
    }

    private void ChangeSizeCol(float offX, float offY, float sizeX, float sizeY)
    {
        m_boxCol.offset = new Vector2(offX, offY);
        m_boxCol.size = new Vector2(sizeX, sizeY);
    }
}
