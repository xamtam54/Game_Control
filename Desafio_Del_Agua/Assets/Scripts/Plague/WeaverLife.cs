using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaverLife : MonoBehaviour
{
    public int maxHits = 2;
    public int hitsReceived = 0;
    private int previousHits = 0; // Almacenar el n�mero de golpes recibidos en la �ltima comprobaci�n
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private FireWeaver fireWeaverComponent;

    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        fireWeaverComponent = GetComponent<FireWeaver>();
        previousHits = hitsReceived; // Al inicio, establecer el n�mero anterior de golpes recibidos
    }

    void Update()
    {
        // Verificar si el n�mero de golpes recibidos ha cambiado
        if (hitsReceived != previousHits)
        {
            // Si ha recibido da�o, ejecutar la funci�n ReceiveDamage()
            ReceiveDamage(hitsReceived - previousHits);

            // Actualizar el n�mero anterior de golpes recibidos
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
