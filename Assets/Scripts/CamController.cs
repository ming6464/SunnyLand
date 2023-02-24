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
    public void Running(Vector3 pos)
    {
        m_vtCurPosition.Set(Mathf.Clamp(pos.x, minX, maxX)
            , Mathf.Clamp(pos.y, minY, maxY)
            , m_vtCurPosition.z);
        transform.position = m_vtCurPosition;
        
    }
}
