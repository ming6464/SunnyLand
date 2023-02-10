using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeCollider : MonoBehaviour
{
    public Player player;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(TagAndKey.T_WALL))
        {
            player.blockDirect = 1;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(TagAndKey.T_WALL))
        {
            player.blockDirect = 10;

        }
    }
}
