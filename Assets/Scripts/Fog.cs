using System;
using System.Collections;
using UnityEngine;

public class Fog : Enemy
{
    private const string FOGSTATE = "FogState";
    private LayerMask m_layerGround;
    private float[,] m_arrRg =
    {
        {
            1.236794f, 0.8026575f,-0.04834557f, -0.1532257f,
            0.7911892f, 0.09275138f, -0.1044054f, 0.3528189f
        },
        {
            1.275534f,1.015759f,-0.1387548f,0.01790154f
            ,0.9720039f,0.09275126f,-0.2012691f,0.6111237f
            
        },
        {
            1.223877f,0.3699968f,
            -0.138752f,-0.3049797f,1.004292f,0.1444125f,-0.1851254f,0.126802f
        }
    };
    enum State
    {
        Idle,Jump,Fall
    }

    private State m_state;
    private int m_direct,m_count,m_pastDirect;
    private BoxCollider2D m_coll,m_childColl;
    private float m_velocJumpY,m_velocJumpX,m_jumpTime,m_time,m_ratioBetweenAndY,m_x,m_y;
    private bool m_isJumped;

    protected override void Awake()
    {
        base.Awake();
        m_coll = GetComponent<BoxCollider2D>();
        m_childColl = transform.Find("DeathPoint").GetComponent<BoxCollider2D>();
    }

    protected override void Start()
    {
        m_ratioBetweenAndY = 3 / 1;
        m_layerGround = LayerMask.GetMask("Ground");
        m_state = State.Idle;
        m_count = 1;
    }

    protected override void Update()
    {
        base.Update();
        if (this.anim.enabled)
        {
            if (m_isJumped)
            {
                if (m_state == State.Jump)
                {
                    m_time -= Time.deltaTime;
                    if (m_time <= 0)
                    {
                        m_state = State.Fall;
                        UpdateStateAnimator();
                    }
                }
            }
            else
            {
                if (CamController.Ins.transform.position.x > transform.position.x)
                    m_direct = -1;
                else
                    m_direct = 1;
                if (m_pastDirect != m_direct)
                {
                    m_pastDirect = m_direct;
                    UpdateDirect();
                }
            }
        }
    }

    private void UpdateDirect()
    {
        transform.localScale = new Vector3(m_direct, 1, 1);
    }

    void JumpedOn()
    {
        m_count--;
        if (m_count == 0)
        {
            m_isJumped = true;
            m_count = 2;
            m_x = Mathf.Clamp(Mathf.Abs(CamController.Ins.transform.position.x - transform.position.x), 0, 5);
            m_y = Mathf.Clamp(Mathf.Abs(CamController.Ins.transform.position.y - transform.position.y), 
                m_x / m_ratioBetweenAndY, 5 / m_ratioBetweenAndY);
            m_jumpTime = Mathf.Sqrt(m_y / 5);
            m_time = m_jumpTime;
            m_state = State.Jump;
            this.rigid.velocity = new Vector2((m_x / (2 * m_jumpTime)) * m_direct * -1, 10 * m_jumpTime);
            UpdateStateAnimator();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        GameObject gObj = col.gameObject;
        if (gObj.CompareTag(TagAndKey.T_GROUND))
        {
            if (m_coll.IsTouchingLayers(m_layerGround))
            {
                this.rigid.velocity = Vector2.zero;
                m_state = State.Idle;
                m_isJumped = false;
                UpdateStateAnimator();
            }
        }
        else if (gObj.CompareTag(TagAndKey.T_WALL))
            this.rigid.velocity = Vector2.zero;
    }

    void UpdateStateAnimator()
    {
        this.anim.SetInteger(FOGSTATE,(int)m_state);
        UpdateSizeRG();
    }

    private void UpdateSizeRG()
    {
        m_coll.size = new Vector2(m_arrRg[(int)m_state, 0],
            m_arrRg[(int)m_state, 1]);
        m_coll.offset = new Vector2(m_arrRg[(int)m_state, 2],
            m_arrRg[(int)m_state, 3]);
        m_childColl.size = new Vector2(m_arrRg[(int)m_state, 4],
            m_arrRg[(int)m_state, 5]);
        m_childColl.offset = new Vector2(m_arrRg[(int)m_state, 6],
            m_arrRg[(int)m_state, 7]);
    }
}