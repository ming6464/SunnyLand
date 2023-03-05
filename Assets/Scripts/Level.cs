using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public Image lockImg;
    public TextMeshProUGUI nameChapterText;
    private Button m_btn;
    private string m_chapter;
    private bool m_isOpen;
    

    void Start()
    {
        GetComponent<Button>()?.onClick.AddListener(() => ActionClick());
    }

    private void ActionClick()
    {
        if (m_isOpen) SceneManager.LoadScene(m_chapter);
    }

    public void SetData(string chapter, bool isOpen)
    {
        if(nameChapterText)
            nameChapterText.text = chapter;
        if (isOpen)
        {
            if(lockImg) Destroy(lockImg.gameObject);
            m_isOpen = true;
            m_chapter = chapter;
        }
    }
}
