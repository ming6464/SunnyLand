
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed,positionXMin,positionXMax;
    public int maxHeart, heart, amountCherry, amountGem, amountEnemyKill,blockDirect;
    public GameObject  slideRight;
    private LayerMask m_layer;
    Animator m_anim;
    private float m_x, m_y, m_pastRotationX;
    Rigidbody2D m_rg;
    BoxCollider2D m_boxColli,m_boxColli_slideRight;
    private Vector2 m_vt2_velocJump, m_vt2_velocHurt,m_vt2_colli;
    private bool m_isHurt,m_isFinish;
    public enum State
    {
        Idle,Run,Jump,Climp,Hurt,Crouch
    }
    [SerializeField]
    private State m_state;

    // Start is called before the first frame update
    void Start()
    {
        blockDirect = 10;
        speed = 5;
        m_layer = LayerMask.GetMask("Ground");
        m_pastRotationX = 1;
        m_state = State.Idle;
        m_rg = gameObject.GetComponent<Rigidbody2D>();
        m_anim = gameObject.GetComponent<Animator>();
        m_boxColli = gameObject.GetComponent<BoxCollider2D>();
        m_vt2_velocJump = new Vector2(0, 5 *Mathf.Sqrt(6));
        m_vt2_velocHurt = new Vector2((3 * Mathf.Sqrt(5) / 2), Mathf.Sqrt(20));
        if (slideRight) m_boxColli_slideRight = slideRight.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!m_isFinish)
            UpdateAnimationAndRun();
    }

    void UpdateAnimationAndRun()
    {
        if (!m_isHurt)
        {
            m_x = Input.GetAxisRaw("Horizontal");
            m_y = Input.GetAxisRaw("Vertical");
            if(m_x != 0)
            {
                if (m_x != blockDirect * m_x)
                {
                    transform.position = new Vector3(Mathf.Clamp(transform.position.x + Time.deltaTime * speed * m_x,
                        positionXMin,
                        positionXMax), transform.position.y, transform.position.z);
                }
                if (m_x != m_pastRotationX)
                {
                    float scale_x = 1;
                    if (m_x < 0)
                        scale_x = -1;
                    transform.localScale = new Vector3(scale_x, 1, 1);
                    m_pastRotationX = m_x;
                }
            }
        }
        else Physics2D.IgnoreLayerCollision(8,9);
        if (m_boxColli.IsTouchingLayers(m_layer))
        {
            if (m_isHurt && m_rg.velocity.y == 0)
            {
                m_isHurt = false;
                m_rg.velocity = Vector2.zero;
            }
            else if (!m_isHurt)
            {
                if (m_x != 0)
                {
                    m_state = State.Run;
                }else if (m_y == 0)
                {
                    m_state = State.Idle;
                }
                if (m_y > 0)
                {
                    m_state = State.Jump;
                    m_rg.velocity = m_vt2_velocJump;
                }
                else if(m_y < 0)
                {
                    m_state = State.Crouch;
                }
                UpdateAnimation();
            }
        }
        ChangeViewCam(transform.position.x, transform.position.y);
    }
    void ChangeViewCam(float positionX,float positionY)
    {
        CamController.Ins.Running(positionX, positionY);
    }

    void UpdateSizeColl()
    {
        State state = m_state;
        if (m_state == State.Run || m_state == State.Hurt) state = State.Idle;
        switch (state)
        {
            case State.Idle:
                m_boxColli.offset = UpdateVt2Colli(0.0336628f,-0.39694f);
                m_boxColli.size = UpdateVt2Colli(0.7566414f,1.195004f);
                m_boxColli_slideRight.offset = UpdateVt2Colli(0.4475422f,-0.3258033f);
                m_boxColli_slideRight.size = UpdateVt2Colli(0.04493332f,1.162652f);
                break;
            case State.Crouch :
                m_boxColli.offset = UpdateVt2Colli(0.1044941f,-0.5718031f);
                m_boxColli.size = UpdateVt2Colli(0.9248619f,0.8452773f);
                m_boxColli_slideRight.offset = UpdateVt2Colli(0.6113377f,-0.5462106f);
                m_boxColli_slideRight.size = UpdateVt2Colli(0.07149887f,0.8066208f);
                break;
            case State.Jump:
                m_boxColli.offset = UpdateVt2Colli(0.01152992f,-0.2198634f);
                m_boxColli.size = UpdateVt2Colli(0.7212257f,1.035635f);
                m_boxColli_slideRight.offset = UpdateVt2Colli(0.4209805f,-0.1452248f);
                m_boxColli_slideRight.size = UpdateVt2Colli(0.05379105f,0.9520093f);
                break;
        }
    }

    void UpdateAnimation()
    {
        m_anim.SetInteger(TagAndKey.ANIM_PLAYER,(int)m_state);
        UpdateSizeColl();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(TagAndKey.T_ENEMY))
        {
            if (!m_isHurt)
            {
                heart--;
                if (heart <= 0) m_boxColli.isTrigger = true;
                m_isHurt = true;
                int direct = 1;
                if (col.gameObject.transform.position.x > transform.position.x)
                    direct = -1;
                AnimHurt(direct);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        GameObject gObj = col.gameObject;
        if(gObj.CompareTag(TagAndKey.T_DEATHZONE))
        {
            heart = 0;
            m_rg.constraints = RigidbodyConstraints2D.FreezePositionY;
            m_anim.SetTrigger("Death");
        }
        else if (col.gameObject.CompareTag(TagAndKey.T_FINISH))
        {
            m_isFinish = true;
            UpDataCollected();
            GameManager.Ins.Finish();
        }
        else
        {
            if (!m_isHurt && col.gameObject.CompareTag(TagAndKey.T_DEATHPOINTENEMY))
            {
                amountEnemyKill++;
                m_rg.velocity = m_vt2_velocJump;
                m_state = State.Jump;
                UpdateAnimation();
                Enemy enemy = gObj.transform.parent.gameObject.GetComponent<Enemy>();
                enemy.End();
                GameManager.Ins.IncreaseScore(enemy.score);
            }

            if (gObj.CompareTag(TagAndKey.T_ITEM))
            {
                Item item = gObj.GetComponent<Item>();
                if (item.score == 50)
                    amountGem++;
                else
                    amountCherry++;
                item.Collected();
                GameManager.Ins.IncreaseScore(item.score);
            }
        }
        
    }
    void AnimHurt(int direct)
    {
        m_state = State.Hurt;
        m_rg.velocity = new Vector2(m_vt2_velocHurt.x * direct, m_vt2_velocHurt.y);
        UpdateAnimation();
    }

    void Death()
    {
        UpDataCollected();
        GameManager.Ins.GameOver();
        Destroy(this.gameObject);
    }

    void UpDataCollected()
    {
        PrefConsts.Ins.Cherry = amountCherry;
        PrefConsts.Ins.EnemyKill = amountEnemyKill;
        PrefConsts.Ins.Gem = amountGem;
    }

    Vector2 UpdateVt2Colli(float x, float y)
    {
        m_vt2_colli.Set(x,y);
        return m_vt2_colli;
    }
}
