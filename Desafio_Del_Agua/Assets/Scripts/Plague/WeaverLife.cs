using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaverLife : MonoBehaviour
{
    public int maxHits = 2; 
    private int hitsReceived = 0; 

    public void ReceiveDamage(int damage)
    {
        Debug.Log("Golpe");
        hitsReceived += damage;

        if (hitsReceived >= maxHits)
        {
            Destroy(gameObject);
        }
    }
}
