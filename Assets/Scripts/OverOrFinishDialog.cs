using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class OverOrFinishDialog : MonoBehaviour
{
    public TextMeshProUGUI amountCherryText, amountGemText,amountEnemyKillText,curChapterText;
    public GameObject nextChapPanel;
    public string curChapter;
    void Start()
    {
        curChapterText.text = curChapter;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpData(int cherry, int gem,int enemyKill)
    {
        amountCherryText.SetText("x" + cherry);
        amountGemText.SetText("x" +  gem);
        amountEnemyKillText.SetText("x" + enemyKill);
    }

    public void Show(bool isFinish)
    {
        UpData(PrefConsts.Ins.Cherry,PrefConsts.Ins.Gem,PrefConsts.Ins.EnemyKill);
        nextChapPanel.SetActive(isFinish);
        this.gameObject.SetActive(true);
    }
}
