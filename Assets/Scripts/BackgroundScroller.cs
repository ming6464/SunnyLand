using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BackgroundScroller : MonoBehaviour
{
    public float speed;
    public float maxX = 18.83f, minX = -18.39f;

    private void Update()
    {
        if (transform.position.x <= minX)
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }
}
