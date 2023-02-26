using System;
using UnityEngine;

public class PlayerCol : MonoBehaviour
{
    public AudioClip auc_collect;
    private Player m_player;
    private Rigidbody2D m_rg;
    private bool m_isCheck;

    private void Start()
    {
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
        else if (col.gameObject.CompareTag(TagAndKey.T_FINISH))
        {
            m_player.isFinish = true;
            GameManager.Ins.Finish();
        }
        else
        {
            if (!m_player.isHurt && col.gameObject.CompareTag(TagAndKey.T_DEATHPOINTENEMY))
            {
                m_player.JumpKillEnemy();
                Enemy enemy = gObj.transform.parent.gameObject.GetComponent<Enemy>();
                enemy.End();
                GameManager.Ins.IncreaseScore(enemy.score);
            }
            if (gObj.CompareTag(TagAndKey.T_ITEM))
            {
                Item item = gObj.GetComponent<Item>();
                m_isCheck = false;
                if (item.score == 50)
                    m_isCheck = true;
                m_player.CollectItem(m_isCheck);
                item.Collected();
                GameManager.Ins.IncreaseScore(item.score);
                AudioManager.Ins.PlayAudioEffect(2,0.22f);
            }
        }
    }
}