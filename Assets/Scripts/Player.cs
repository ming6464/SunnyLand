using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Player : MonoBehaviour
{
    public bool isHurt,isGround,isJump,isBlockDirect,isFinish;
    public float speed,positionXMin,positionXMax;
    public int maxHeart, heart, amountCherry, amountGem, amountEnemyKill;
    private SlideBottomPlayer m_slideBottom;
    private SlideRightPlayer m_slideRight;
    private Animator m_anim;
    private float m_x, m_curRotateX,m_velocHurtX,m_jumpTime,m_jumpStartTime;
    private Rigidbody2D m_rg;
    private BoxCollider2D m_boxCol;
    private Vector2 m_vt2_velocJump;
    private string m_animState;
    
    void Start()
    {
        isBlockDirect = false;
        speed = 5;
        m_curRotateX = 1;
        m_animState = TagAndKey.A_PLAYER_IDLE;
        m_rg = gameObject.GetComponent<Rigidbody2D>();
        m_anim = gameObject.GetComponent<Animator>();
        m_boxCol = gameObject.GetComponent<BoxCollider2D>();
        m_vt2_velocJump = new Vector2(0, 5 *Mathf.Sqrt(2));
        m_velocHurtX = 5 / Mathf.Sqrt(2);
        m_slideRight = GetComponentInChildren<SlideRightPlayer>();
        m_slideBottom = GetComponentInChildren<SlideBottomPlayer>();
        m_jumpTime = 2.65f / m_vt2_velocJump.y;
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreLayerCollision(8,9,isHurt);
        if(!isFinish && !isHurt)
            UpdateAnimationAndRun();
    }

    void UpdateAnimationAndRun()
    {
        m_x = Input.GetAxisRaw("Horizontal");
        if(m_x != 0)
        {
            if (!isBlockDirect)
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x + Time.deltaTime * speed * m_x,
                    positionXMin,
                    positionXMax), transform.position.y, transform.position.z);
            }
            if (m_x != m_curRotateX)
            {
                m_curRotateX = m_x;
                transform.localScale = new Vector3(m_curRotateX, 1, 1);
            }
        }
        if (isGround && !isJump)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                m_jumpStartTime = m_jumpTime;
                m_animState = TagAndKey.A_PLAYER_JUMPUP;
                isJump = true;
            }
            else if(Input.GetKey(KeyCode.DownArrow)) m_animState = TagAndKey.A_PLAYER_CROUND;
            else if (m_x != 0) m_animState = TagAndKey.A_PLAYER_RUN;
            else m_animState = TagAndKey.A_PLAYER_IDLE;
        }
        if (isJump)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                m_jumpStartTime -= Time.deltaTime;
                if (m_jumpStartTime > 0)
                    m_rg.velocity = m_vt2_velocJump;
                else isJump = false;
            }
            else isJump = false;
        }else if (!isGround && m_rg.velocity.y <= 0) m_animState = TagAndKey.A_PLAYER_FALL;
        UpdateAnimation();
        CamController.Ins.Running(transform.position.x, transform.position.y);
    }

    private void UpdateSizeCol()
    {
        m_slideRight.UpdateSizeCol(m_animState);
        m_slideBottom.UpdateSizeCol(m_animState);
        switch (m_animState)
        {
            case TagAndKey.A_PLAYER_CROUND :
                ChangeSizeCol(0.09434021f,-0.544036f,1.034773f,0.8269477f);
                break;
            case TagAndKey.A_PLAYER_JUMPUP:
                ChangeSizeCol(-0.01080763f,-0.1145868f,0.8272188f,0.9488536f);
                break;
            case TagAndKey.A_PLAYER_FALL:
                ChangeSizeCol(0.000759244f,-0.07572845f,0.804085f,1.213522f);
                break;
            default:
                ChangeSizeCol(-0.01080763f,-0.3015704f,0.8272188f,1.32282f);
                break;
        }
    }

    private void UpdateAnimation()
    {
        m_anim.Play(m_animState);
        UpdateSizeCol();
    }
    
    public void AnimHurt(int direct)
    {
        isHurt = true;
        m_animState = TagAndKey.A_PLAYER_HURT;
        m_rg.velocity = new Vector2(m_velocHurtX * direct, m_vt2_velocJump.y);
        UpdateAnimation();
    }

    void CheckDeath()
    {
        if(heart > 0)
            return;
        m_rg.constraints = RigidbodyConstraints2D.FreezePositionY;
        m_anim.SetTrigger("Death");
    }
    
    void Death()
    {
        UpDataCollected();
        GameManager.Ins.GameOver();
        Destroy(this.gameObject);
    }

    public void UpDataCollected()
    {
        PrefConsts.Ins.Cherry = amountCherry;
        PrefConsts.Ins.EnemyKill = amountEnemyKill;
        PrefConsts.Ins.Gem = amountGem;
    }
    private void ChangeSizeCol(float offX, float offY, float sizeX, float sizeY)
    {
        m_boxCol.offset = new Vector2(offX, offY);
        m_boxCol.size = new Vector2(sizeX, sizeY);
    }

    public void JumpKillEnemy()
    {
        amountEnemyKill++;
        m_rg.velocity = m_vt2_velocJump;
        m_animState = TagAndKey.A_PLAYER_JUMPUP;
        isJump = true;
        UpdateAnimation();
    }

}