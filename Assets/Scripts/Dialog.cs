using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI curChapterText;
    public GameObject nextChapBtn,resumeBtn;
    public string curChapter;
    void Start()
    {
        curChapterText.text = curChapter;
    }
    public void ShowDialog(int state, bool isShow = false)
    {
        gameObject.SetActive(isShow);
        if (!isShow) return;
        nextChapBtn.SetActive(false);
        resumeBtn.SetActive(false);
        switch (state)
        {
            case TagAndKey.STATE_DIALOGWIN:
                nextChapBtn.SetActive(true);
                break;
            case TagAndKey.STATE_DIALOGPAUSE:
                resumeBtn.SetActive(true);
                break;
        }
    }
}
