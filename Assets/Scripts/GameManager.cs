using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public bool isOverGame,isPause;
    private int m_score, m_bestScore;
    public override void Awake()
    {
       // base.Awake();
        m_bestScore = PrefConsts.Ins.BestScore;
    }

    public override void Update()
    {
        if (!isOverGame)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isPause = !isPause;
                GamePause();
            }

        }
        
    }

    public override void Start()
    {
        UIManager.Ins.UpBestScore(m_bestScore);
        UIManager.Ins.UpScore(m_score);
    }

    public void IncreaseScore(int buff)
    {
        m_score += buff;
        UIManager.Ins.UpScore(m_score);
        if (m_score > m_bestScore)
        {
            m_bestScore = m_score;
            UIManager.Ins.UpBestScore(m_bestScore);
            PrefConsts.Ins.BestScore = m_bestScore;
        }
        
    }

    public void GameOver()
    {
        DestroyAllEnemyOnScene();
        isOverGame = true;
        UIManager.Ins.ShowOverDialog(false);
    }

    void GamePause()
    {
        UIManager.Ins.ShowOrHideGamePause(isPause);
    }

    public void LoadScene(string scene)
    {
        Debug.Log(scene + "////");
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }

    public void Finish()
    {
        DestroyAllEnemyOnScene();
        UIManager.Ins.ShowOverDialog(true);
    }

    void DestroyAllEnemyOnScene()
    {
        foreach (Enemy e in FindObjectsOfType<Enemy>())
        {
            e.End();
        }
    }
}