using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float speed;
    [SerializeField]
    private Renderer m_rend;
    
    private void Update()
    {
        if (m_rend)
        {
            m_rend.material.mainTextureOffset += new Vector2(Time.deltaTime * speed, 0);
        }
    }
}
