using System.Collections.Generic;
using UnityEngine;

public class ListLevel : MonoBehaviour
{
    public GameObject level;
    public List<string> listLevel;
    void Start()
    {
        int lastLevel = PrefConst.Ins.LastLevel - 1;
        bool isOpen;
        if (level)
        {
            for (int i = 0; i < listLevel.Count; i++)
            {
                isOpen = false;
                if (lastLevel >= i) isOpen = true;
                GameObject newLevel = Instantiate(level);
                newLevel.transform.SetParent(transform,false);
                Level levelComponent = newLevel.GetComponent<Level>();
                levelComponent.SetData(listLevel[i],isOpen);
            }
        }
    }

}
