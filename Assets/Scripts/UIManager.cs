using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public TextMeshProUGUI blackHoleText,scoreText;
    public GameObject useSkillPanel;
    public Slider musicSlider, sfxSlider;
    public override void Awake()
    {
    }

    public override void Start()
    {
        if (musicSlider && sfxSlider)
        {
            musicSlider.value = PrefConst.Ins.MusicAudioVol;
            sfxSlider.value = PrefConst.Ins.SfxAudioVol;
        }
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
        if(useSkillPanel) useSkillPanel.SetActive(isActive);
    }

    public void AudioVol(bool isSfx)
    {
        if(isSfx)
            AudioManager.Ins.AudioVol(sfxSlider.value,isSfx);
        else 
            AudioManager.Ins.AudioVol(musicSlider.value,isSfx);
    }
    
    
}
