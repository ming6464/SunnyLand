using UnityEngine;

public class PlayerCol : MonoBehaviour
{
    private Player m_player;
    private Rigidbody2D m_rg;
    private int curIdItem;

    private void Start()
    {
        curIdItem = this.GetInstanceID();
        m_player = GetComponent<Player>();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(TagAndKey.T_ENEMY) && ! m_player.isHurt)
        {
            m_player.heart--;
            int direct = 1;
            if (col.gameObject.transform.position.x > transform.position.x) direct = -1;
            m_player.AnimHurt(direct);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        GameObject gObj = col.gameObject;
        if(gObj.CompareTag(TagAndKey.T_DEATHZONE))
        {
            m_player.heart = 0;
            m_player.Death();
        }
        else if (col.gameObject.CompareTag(TagAndKey.T_WIN))
        {
            m_player.isWin = true;
            GameManager.Ins.Win();
        }
        else
        {
            if (!m_player.isHurt && col.gameObject.CompareTag(TagAndKey.T_DEATHPOINTENEMY))
            {
                AudioManager.Ins.PlayAudio(TagAndKey.AUDIO_KILLENEMY,true);
                m_player.JumpKillEnemy();
                gObj.transform.parent.gameObject.GetComponent<Enemy>().End();
            }
            if (gObj.CompareTag(TagAndKey.T_ITEM))
            {
                if (curIdItem != gObj.gameObject.GetInstanceID())
                {
                    curIdItem = gObj.gameObject.GetInstanceID();
                    Item item = gObj.GetComponent<Item>();
                    item.Collected();
                    GameManager.Ins.IncreaseScore(item.STATE);
                    
                }
                
            }
        }
    }
}