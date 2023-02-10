using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : Singleton<CamController>
{
    public float minX,maxX,minY,maxY;
    Vector3 m_vtCurPosition;

    public override void Awake()
    {
        m_vtCurPosition = transform.position;
    }
    public void Running(float positionX,float positionY)
    {
        m_vtCurPosition.Set(Mathf.Clamp(positionX, minX, maxX)
            , Mathf.Clamp(positionY, minY, maxY)
            , m_vtCurPosition.z);
        transform.position = m_vtCurPosition;
        
    }
}
