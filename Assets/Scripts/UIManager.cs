using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    public TextMeshProUGUI scoreText, bestScoreText,blackHoleText;
    public GameObject gamePause,useSkillPanel;
    public OverOrFinishDialog gameOverDialog;
    public override void Awake()
    {

    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public void UpScore(int score)
    {
        if(scoreText) scoreText.SetText(score.ToString());
    }

    public void UpBestScore(int score)
    {
        if(bestScoreText) bestScoreText.SetText(score.ToString());
    }
    public void ShowOverDialog(bool isFinish)
    {
        gameOverDialog.Show(isFinish);
    }

    public void ShowOrHideGamePause(bool isShow)
    {
        if (isShow)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1;
        gamePause.SetActive(isShow);
    }

    public void UpdateSkill(int amount, int state)
    {
        switch (state)
        {
            case 1 :
                blackHoleText.text = " x" + amount;
                break;
        }
    }

    public void SetActiveSkillPanel(bool isActive)
    {
        if(isActive)
            Time.timeScale = 0.01f;
        else
            Time.timeScale = 1f;
        useSkillPanel.SetActive(isActive);
    }
    
}
