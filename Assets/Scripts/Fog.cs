using UnityEngine;

public class Fog : Enemy
{
    private const string FOGSTATE = "FogState";
    private Transform m_playerTrans;
    enum State
    {
        Idle,Jump,Fall
    }
    private State m_state;
    private int m_direct,m_count,m_pastDirect;
    private float m_velocJumpY,m_velocJumpX,m_jumpTime,m_time,m_ratioBetweenAndY,m_x,m_y;
    private bool m_isJumped;
    protected override void Start()
    {
        m_ratioBetweenAndY = 3 / 1;
        m_state = State.Idle;
        m_count = 1;
        m_playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
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
            else if(m_playerTrans)
            {
                if (m_playerTrans.position.x > transform.position.x)
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
        if (m_count == 0 && m_playerTrans)
        {
            m_isJumped = true;
            m_count = 2;
            m_x = Mathf.Clamp(Mathf.Abs(m_playerTrans.position.x - transform.position.x), 0, 5);
            m_y = Mathf.Clamp(Mathf.Abs(m_playerTrans.position.y - transform.position.y), 
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
            this.rigid.velocity = Vector2.zero;
            m_state = State.Idle;
            m_isJumped = false;
            UpdateStateAnimator();
        }
        else if (gObj.CompareTag(TagAndKey.T_WALL))
            this.rigid.velocity = Vector2.zero;
    }

    void UpdateStateAnimator()
    {
        this.anim.SetInteger(FOGSTATE,(int)m_state);
    }
}