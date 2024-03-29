using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameManager : Singleton<GameManager>
{
    public GameObject blackHolePanel;
    public BlackHole backHole;
    public Tilemap tlm_ground, tlm_wall;
    public bool isOverGame,isUseSkill,isScreenHome,isLastChapter,isPause;
    public Dialog dialog;
    public int amountBlackHole,nextLevel;
    private int m_countMouseClick,m_score;
    private Vector3 m_mouseClickPos;
    private BlackHole m_backHoleA,m_backHoleB;
    private Animator m_animBlackHole;
    public override void Awake()
    {
    }
    
    public override void Start()
    {
        if (isScreenHome)
        {
            AudioManager.Ins.PlayAudio(TagAndKey.AUDIO_MUSICIDLE,false);
            return;
        }
        AudioManager.Ins.PlayAudio(TagAndKey.AUDIO_MUSICPLAYGAME,false);
        UIManager.Ins.UpdateScoreOrSkill(amountBlackHole,TagAndKey.STATE_BLACKHOLE);
        UIManager.Ins.UpdateScoreOrSkill(m_score, TagAndKey.STATE_SCORE);
        m_animBlackHole = blackHolePanel.GetComponent<Animator>();
    }

    public override void Update()
    {
        if (isScreenHome || isOverGame) return;
        if (Input.GetKeyDown(KeyCode.Escape)) GamePause(true);
            
        if (m_countMouseClick > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_mouseClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (CheckTileAtPos(m_mouseClickPos))
                {
                    if (m_countMouseClick == 1)
                    {
                        m_backHoleA = Instantiate(backHole, new Vector3(m_mouseClickPos.x,m_mouseClickPos.y,0), Quaternion.identity);
                        m_countMouseClick++;
                    }
                    else
                    {
                        isUseSkill = false;
                        m_backHoleB = Instantiate(backHole, new Vector3(m_mouseClickPos.x,m_mouseClickPos.y,0), quaternion.identity);
                        m_backHoleB.ChangeTransOut(m_backHoleA.gameObject.transform);
                        m_backHoleA.ChangeTransOut(m_backHoleB.gameObject.transform);
                        m_countMouseClick = 0;
                        UIManager.Ins.SetActiveSkillPanel(false);
                    }
                }
                    
            }
        }
    }

    public void IncreaseScore(int state)
    {
        switch (state)
        {
            case TagAndKey.STATE_CHERRY:
                m_score += TagAndKey.POINT_CHERRY;
                break;
            case TagAndKey.STATE_GEM:
                m_score += TagAndKey.POINT_GEM;
                break;
        }
        UIManager.Ins.UpdateScoreOrSkill(m_score,TagAndKey.STATE_SCORE);
        CheckExchangeSkill();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        AudioManager.Ins.StopMusic();
        AudioManager.Ins.PlayAudio(TagAndKey.AUDIO_OVER, true);
        isOverGame = true;
        dialog.ShowDialog(TagAndKey.STATE_DIALOGOVER,true);
    }

    public void LoadScene(string scene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }

    public void Win()
    {
        
        Time.timeScale = 0f;
        AudioManager.Ins.StopMusic();
        AudioManager.Ins.PlayAudio(TagAndKey.AUDIO_WIN, true);
        if (isLastChapter)
        {
            dialog.ShowDialog(TagAndKey.STATE_DIALOGFINISH,true);
            return;
        }
        PrefConst.Ins.LastLevel = nextLevel;
        dialog.ShowDialog(TagAndKey.STATE_DIALOGWIN,true);
    }

    public void GamePause(bool isPause)
    {
        this.isPause = isPause;
        Time.timeScale = 0f;
        AudioManager.Ins.PauseOrResumeMusic(isPause);
        if(isPause)
            dialog.ShowDialog(TagAndKey.STATE_DIALOGPAUSE,true);
        else
        {
            dialog.ShowDialog(1);
            Time.timeScale = 1f;
        }
        
    }
    

    public void UseBlackHole()
    {
        if (!isOverGame && !isPause && !isUseSkill && amountBlackHole > 0)
        {
            isUseSkill = true;
            m_countMouseClick++;
            amountBlackHole--;
            UIManager.Ins.UpdateScoreOrSkill(amountBlackHole,TagAndKey.STATE_BLACKHOLE);
            UIManager.Ins.SetActiveSkillPanel(true);
        }
    }

    private bool CheckTileAtPos(Vector3 pos)
    {
        bool check = false;
        if (tlm_wall)
            check = tlm_wall.GetTile(tlm_wall.WorldToCell(pos));
        if (!check && tlm_ground)
            check = tlm_ground.GetTile(tlm_ground.WorldToCell(pos));
        return !check;
    }

    private void CheckExchangeSkill()
    {
        if (m_score < 100) return;
        int blackHoleSkillTimes = m_score / 100;
        amountBlackHole += blackHoleSkillTimes;
        m_score -= 100 * blackHoleSkillTimes;
        UIManager.Ins.UpdateScoreOrSkill(m_score,TagAndKey.STATE_SCORE);
        if(m_animBlackHole) m_animBlackHole.Play("Increase");
        StartCoroutine(CountDownAnimationBlackHole(0.45f));
    }

    IEnumerator CountDownAnimationBlackHole(float time,bool isFinish = false)
    {
        yield return new WaitForSeconds(time);
        if (!isFinish)
        {
            UIManager.Ins.UpdateScoreOrSkill(amountBlackHole,TagAndKey.STATE_BLACKHOLE);
            StartCoroutine(CountDownAnimationBlackHole(0.45f, true));
        }
        else if(m_animBlackHole) m_animBlackHole.Play("Idle");
    }
    
}