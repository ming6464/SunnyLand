using UnityEngine;

public class CanvasHome : MonoBehaviour
{
    public GameObject btnPanel, audioManagerPanel, listLevelPanel;

    private void Start()
    {
        btnPanel.SetActive(true);
        listLevelPanel.SetActive(false);
        audioManagerPanel.SetActive(false);
    }

    public void onClick(int state)
    {
        btnPanel.SetActive(false);
        listLevelPanel.SetActive(false);
        audioManagerPanel.SetActive(false);
        switch (state)
        {
            case 1:
                btnPanel.SetActive(true);
                break;
            case 2: 
                audioManagerPanel.SetActive(true);
                break;
            case 3:
                listLevelPanel.SetActive(true);
                break;
        }
    }
}
