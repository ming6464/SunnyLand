using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameManager : Singleton<GameManager>
{
    public BlackHole backHole;
    public Tilemap tlm_ground, tlm_wall;
    public bool isOverGame,isUseSkill,isScreenHome;
    public int amountOfBlackHoleSkill;
    private int m_score,m_countMouseClick;
    private Vector3 m_mouseClickPos;
    private BlackHole m_backHoleA,m_backHoleB;
    public override void Awake()
    {
    }
    
    public override void Start()
    {
        if (isScreenHome) return;
        UIManager.Ins.UpScore(m_score);
        UIManager.Ins.UpdateSkill(amountOfBlackHoleSkill,1);
        
    }

    public override void Update()
    {
        if (isScreenHome || isOverGame) return;
        if (Input.GetKeyDown(KeyCode.Escape))
            UIManager.Ins.ShowPauseDialog(true);
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

    public void IncreaseScore(int buff)
    {
        m_score += buff;
        UIManager.Ins.UpScore(m_score);

    }

    public void GameOver()
    {
        AudioManager.Ins.PlayAudioEffect(3,0.5f);
        DestroyAllEnemyOnScene();
        isOverGame = true;
        UIManager.Ins.ShowOverDialog();
    }

    public void LoadScene(string scene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }

    public void Finish()
    {
        AudioManager.Ins.PlayAudioEffect(9,0.5f);
        DestroyAllEnemyOnScene();
        UIManager.Ins.ShowFinshDialog();
    }

    private void DestroyAllEnemyOnScene()
    {
        foreach (Enemy e in FindObjectsOfType<Enemy>())
        {
            e.End();
        }
    }

    public void UseBlackHole()
    {
        if (amountOfBlackHoleSkill > 0 && !isUseSkill)
        {
            isUseSkill = true;
            m_countMouseClick++;
            amountOfBlackHoleSkill--;
            UIManager.Ins.UpdateSkill(amountOfBlackHoleSkill,1);
            UIManager.Ins.SetActiveSkillPanel(true);
        }
        
    }

    private bool CheckTileAtPos(Vector3 pos)
    {
        bool check = false || tlm_ground.GetTile(tlm_ground.WorldToCell(pos))|| tlm_wall.GetTile(tlm_wall.WorldToCell(pos));
        return !check;
    }
}