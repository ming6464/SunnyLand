using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public Sprite full, empty;
    public void ChangeState(HeartStatus state)
    {
        if (full && empty)
        {
            transform.localScale = new Vector3(1, 1, 1);
            Image heartImage = GetComponent<Image>();
            switch (state)
            {
                case HeartStatus.Full :
                    heartImage.sprite = full;
                    break;
                case HeartStatus.Empty:
                    heartImage.sprite = empty;
                    break;
                    
            }
        }
    }
}
public enum HeartStatus
{
    Empty = 0,
    Full = 1,
}
