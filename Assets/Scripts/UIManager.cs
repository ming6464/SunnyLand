using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    public TextMeshProUGUI blackHoleText,scoreText;
    public GameObject useSkillPanel;
    public override void Awake()
    {

    }
    public void UpdateScoreOrSkill(int amount,int state)
    {
        switch (state)
        {
            case TagAndKey.STATE_SCORE:
                if (scoreText) scoreText.text = amount.ToString();
                break;
            case TagAndKey.STATE_BLACKHOLE:
                if (blackHoleText) blackHoleText.text = "x " + amount;
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
