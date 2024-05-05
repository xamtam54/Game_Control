using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaverLife : MonoBehaviour
{
    public int maxHits = 2; 
    public int hitsReceived = 0;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private FireWeaver fireWeaverComponent;

    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        fireWeaverComponent = GetComponent<FireWeaver>();

    }

    public void ReceiveDamage(int damage)
    {
        Debug.Log("Golpe");
        hitsReceived += damage;

        if (hitsReceived >= maxHits)
        {
            Destroy(gameObject);
        }
        else if (fireWeaverComponent != null)
        {
            if (navMeshAgent != null)
            {
                navMeshAgent.enabled = false;
            }
        }
    }
}
