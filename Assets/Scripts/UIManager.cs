using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    public TextMeshProUGUI scoreText,blackHoleText;
    public GameObject useSkillPanel;
    public Dialog dialog;
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
    public void ShowOverDialog()
    {
        dialog.ShowDialog(2);
    }

    public void ShowFinshDialog()
    {
        dialog.ShowDialog(1);
    }

    public void ShowPauseDialog(bool isShow)
    {
        if (isShow)
        {
            Time.timeScale = 0f;
            AudioManager.Ins.PauseMusic(true);
        }
        else
        {
            Time.timeScale = 1;
            AudioManager.Ins.PauseMusic(false);
        }
        dialog.ShowDialog(3,isShow);
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
