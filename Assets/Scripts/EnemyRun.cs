using UnityEngine;

public class EnemyRun : Enemy
{
    public float positionEndX,velocityOnSecond;
    int m_direction;
    float m_timeChangeDirection,m_maxX,m_minX,m_time;
    private Transform m_transPlayer;
    
    protected override void Start()
    {
        m_transPlayer = GameObject.FindWithTag("Player").transform;
        float curX = transform.position.x;
        if (curX < positionEndX)
        {
            m_direction = 1;
            m_minX = transform.position.x;
            m_maxX = positionEndX;
        }
        else
        {
            m_direction = -1;
            m_maxX = transform.position.x;
            m_minX = positionEndX;
        }
        m_timeChangeDirection = Mathf.Abs(m_maxX - m_minX) / velocityOnSecond;
        transform.localScale = new Vector3(m_direction, transform.localScale.y, 0);
        m_time = m_timeChangeDirection;
    }

    protected override void Update()
    {
        base.Update();
        if (this.anim.enabled)
        {
            if (!isDeath)
            {
                if (m_time < Time.deltaTime)
                {
                    transform.position += Vector3.right * velocityOnSecond * m_direction * m_time;
                    UpdateDirectionAndTime(false);
                    return;
                }
                m_time -= Time.deltaTime;
                transform.position += Vector3.right * velocityOnSecond * m_direction * Time.deltaTime;
            }
        }else if (m_transPlayer && (m_transPlayer.transform.position.x - transform.position.x) *
                      (positionEndX - m_transPlayer.transform.position.x) >= 0 && !GameManager.Ins.isOverGame) this.ActiveAnimator(true);

    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(TagAndKey.T_PLAYER))
        {
            UpdateDirectionAndTime(true);
        }
    }

    void UpdateDirectionAndTime(bool isColl)
    {

        if (!isColl)
        {
            m_direction *= -1;
            m_time = m_timeChangeDirection;
        }
        else if(m_transPlayer)
        {
            int m_d = 1;
            if (transform.position.x < m_transPlayer.position.x)
                m_d = -1;
            if (m_d != m_direction)
            {
                m_time = m_timeChangeDirection - m_time;
                m_direction *= -1;
            }
        }
        transform.localScale = new Vector3(m_direction, transform.localScale.y, 0);
    }

}