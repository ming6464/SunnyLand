using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected bool isDeath;
    protected Rigidbody2D rigid;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        ActiveAnimator(false);
        
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
    }
    public void End()
    {
        if (!isDeath)
        {
            isDeath = true;
            anim.SetTrigger("Death");
        }
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
    public void ActiveAnimator(bool isActive)
    {
        anim.enabled = isActive;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!GameManager.Ins.isOverGame && col.gameObject.CompareTag(TagAndKey.T_MAINCAM) && !anim.enabled)
        {
            ActiveAnimator(true);
        }
        if(col.gameObject.CompareTag(TagAndKey.T_DEATHZONE))
            End();
    }
    
}