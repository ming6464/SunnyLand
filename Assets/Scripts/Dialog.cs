using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI amountCherryText, amountGemText,amountEnemyKillText,curChapterText;
    public GameObject nextChapBtn,resumeBtn;
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

    public void ShowDialog(int state, bool isShow = true)
    {
        gameObject.SetActive(isShow);
        if (!isShow) return;
        nextChapBtn.SetActive(false);
        resumeBtn.SetActive(false);
        UpData(PrefConsts.Ins.cherry, PrefConsts.Ins.gem, PrefConsts.Ins.enemyKill);
        if (state != 2)
        {
            if (state == 1)
                nextChapBtn.SetActive(true);
            else resumeBtn.SetActive(true);
        }
    }
}
