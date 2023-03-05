using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float rotateSpeed;
    public Transform positionOut;

    void Update()
    {
        transform.Rotate(new Vector3(0,0,rotateSpeed * Time.deltaTime));
    }

    public void ChangeTransOut(Transform transOut)
    {
        positionOut = transOut;
        GetComponent<CircleCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (Vector2.Distance(transform.position, col.gameObject.transform.position) > 0.6f)
            col.gameObject.transform.position = new Vector3(positionOut.position.x, positionOut.position.y,0f);
    }
}
