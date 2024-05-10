using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaverLife : MonoBehaviour
{
    public int maxHits = 2;
    public int hitsReceived = 0;
    private int previousHits = 0; // Almacenar el número de golpes recibidos en la última comprobación
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private FireWeaver fireWeaverComponent;

    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        fireWeaverComponent = GetComponent<FireWeaver>();
        previousHits = hitsReceived; // Al inicio, establecer el número anterior de golpes recibidos
    }

    void Update()
    {
        // Verificar si el número de golpes recibidos ha cambiado
        if (hitsReceived != previousHits)
        {
            // Si ha recibido daño, ejecutar la función ReceiveDamage()
            ReceiveDamage(hitsReceived - previousHits);

            // Actualizar el número anterior de golpes recibidos
            previousHits = hitsReceived;
        }
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
