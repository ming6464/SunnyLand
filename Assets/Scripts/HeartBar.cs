using System.Collections.Generic;
using UnityEngine;

public class HeartBar : MonoBehaviour
{
    public GameObject heartPrefabs;
    public Player playerHeart;
    private List<Heart> hearts;
    private int passHeart;

    private void Start()
    {
       DrawHearts();
       passHeart = playerHeart.heart;
    }

    private void Update()
    {
        if (passHeart != playerHeart.heart)
        {
            DrawHearts();
            passHeart = playerHeart.heart;
        }
    }

    void ClearHearts()
    {
        hearts = new List<Heart>();
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
    }
    
    void CreateHeart(HeartStatus status)
    {
        GameObject newHeart = Instantiate(heartPrefabs);
        newHeart.transform.SetParent(transform,false);
        Heart heartComponent = newHeart.GetComponent<Heart>();
        heartComponent.ChangeState(status);
        hearts.Add(heartComponent);
    }

    void DrawHearts()
    {
        ClearHearts();
        for (int i = 0; i < playerHeart.maxHeart; i++)
        {
            CreateHeart((HeartStatus)(Mathf.Clamp(playerHeart.heart - i,0,1)));
        }
    }
}
