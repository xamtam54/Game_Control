using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform player;
    public Transform[] targets;
    private NavMeshAgent agent;
    private bool isAttacking;

    public float persecutionkRange = 10f;
    public float persecutionkRangeForPlayer = 0f;



    public float attackRange = 5f;
    public float attackDamageRate = 2f;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); 
        isAttacking = true;                                                                                //temporal para definir estado de ataque
    }

    

    void Update()
    {

        LifeController lifeController = player.GetComponent<LifeController>();
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (player != null && isAttacking && lifeController.isAlive && distanceToPlayer <= persecutionkRangeForPlayer)                                                                  //si el jugador empieza a atacar 
        {
            
            Debug.Log("Atacando al jugador");
            StartCoroutine(ApplyDamageOverTime(player));                                                    // Iniciar la corutina para aplicar daño periódico
            agent.destination = player.position;
        }
        else
        {
            //primero ataca dispositivos si estan activados
            //segundo plantas vivas
            //tercero agua

            Transform nearestTarget = null;

            if (nearestTarget == null) 
            { 
                nearestTarget = GetNearestTargetWithTag("Device");
            }

            if (nearestTarget == null /*&& isAlive*/)
            {
                nearestTarget = GetNearestTargetWithTag("Plant");
            }
            if (nearestTarget == null)
            {
                nearestTarget = GetNearestTargetWithTag("Water");
            }
            if (nearestTarget == null)
            {
                nearestTarget = GetNearestTargetWithTag("Player");
            }

            //no hay nada en rango
            if (nearestTarget == null && persecutionkRange <= 500)
            {
                persecutionkRange = persecutionkRange + 2;
            }
            
            if (nearestTarget != null)
            {
                //solo debug
                if (nearestTarget.CompareTag("Plant"))
                {
                    //Debug.Log("Atacando planta");
                }
                else if (nearestTarget.CompareTag("Water"))
                {
                    Debug.Log("Atacando agua");
                }
                else if (nearestTarget.CompareTag("Device"))
                {
                    Debug.Log("Atacando máquina");
                }
                //hasta aqui
            }
            

            if (nearestTarget != null)
            {
                float distanceToTarget = Vector3.Distance(transform.position, nearestTarget.position);
                if (distanceToTarget <= attackRange)
                {
                    StartCoroutine(ApplyDamageOverTime(nearestTarget)); // Iniciar la corutina para aplicar daño periódico
                }

                agent.destination = nearestTarget.position;
            }
        }
    }

    Transform GetNearestTargetWithTag(string tag)
    {
        Transform nearestTarget = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Transform target in targets)
        {
            if (target.CompareTag(tag) && tag == "Plant")
            {
                Plants plant = target.GetComponent<Plants>();
                if (plant != null && plant.isAlive == 1)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);
                    if (distanceToTarget <= persecutionkRange && distanceToTarget < shortestDistance)
                    {
                        shortestDistance = distanceToTarget;
                        nearestTarget = target;
                    }
                }
            }
            if (target.CompareTag(tag) && tag == "Device")
            {
                Devices device = target.GetComponent<Devices>();
                if (device != null && device.Status && device.IsActive)   //si esta bien status y si esta activo
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);
                    if (distanceToTarget <= persecutionkRange && distanceToTarget < shortestDistance)
                    {
                        shortestDistance = distanceToTarget;
                        nearestTarget = target;
                    }
                }
            }
                /*
                if (target.CompareTag(tag))
                {
                    LifeController lifeController = target.GetComponent<LifeController>();
                    if (lifeController != null && lifeController.isAlive)
                    {
                        float distanceToTarget = Vector3.Distance(transform.position, target.position);
                        if (distanceToTarget <= persecutionkRange && distanceToTarget < shortestDistance)
                        {
                            shortestDistance = distanceToTarget;
                            nearestTarget = target;
                        }
                    }
                }*/
            }

            return nearestTarget;
    }
    /*
    public void S_plants()
    {
        GameObject[] plantObjects = GameObject.FindGameObjectsWithTag("Plant", "Device");

        targets = new Transform[plantObjects.Length];
        for (int i = 0; i < plantObjects.Length; i++)
        {
            targets[i] = plantObjects[i].transform;
            //Debug.Log(targets[i]);
        }
        
    }*/
    public void S_plants()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>(); // Encuentra todos los objetos en la escena

        List<Transform> plantTargets = new List<Transform>(); // Lista para almacenar los objetos con los tags correctos

        foreach (GameObject obj in allObjects)
        {
            
            if (obj.CompareTag("Plant") || obj.CompareTag("Device")) // Verifica si el objeto tiene alguno de los tags deseados
            {
                plantTargets.Add(obj.transform); // Agrega el objeto a la lista de objetivos
            }
        }

        // Convierte la lista a un array si es necesario
        targets = plantTargets.ToArray();
    }
    /*
    public void S_devices()
    {
        Debug.Log("aksjdfaksjhaslhalshfkjahsfkj");
        GameObject[] deviceObjects = GameObject.FindGameObjectsWithTag("Device");
        targets = new Transform[deviceObjects.Length];
        for (int i = 0; i < deviceObjects.Length; i++)
        {
            targets[i] = deviceObjects[i].transform;
            Debug.Log(targets[i]);
        }
    }*/

    private IEnumerator ApplyDamageOverTime(Transform target)
    {
        string tag = target.tag;
        if (tag == "Plant")
        {
            Plants plant = target.GetComponent<Plants>();
            if (plant != null)
            {
                float elapsedTime = 0f;
                while (elapsedTime < 1f) // Daño se aplica durante 1 segundo
                {
                    yield return new WaitForSeconds(3f); // Retraso de 1 segundo
                    plant.UpdateHealth(Mathf.RoundToInt(attackDamageRate)); // Aplicar daño
                    elapsedTime += 1f;
                }
            }
        }else if (tag == "Device")
        {
            Devices device = target.GetComponent<Devices>();
            if (device != null)
            {
                float elapsedTime = 0f;
                while (elapsedTime < 1f) // Daño se aplica durante 1 segundo
                {
                    yield return new WaitForSeconds(3f); // Retraso de 1 segundo
                    device.UpdateHealth(Mathf.RoundToInt(attackDamageRate)); // Aplicar daño
                    elapsedTime += 1f;
                }
            }
        }
        else
        {
            LifeController lifeController = target.GetComponent<LifeController>();
            if (lifeController != null)
            {
                float elapsedTime = 0f;
                while (elapsedTime < 1f) // Daño se aplica durante 1 segundo
                {
                    yield return new WaitForSeconds(3f); // Retraso de 1 segundo
                    lifeController.TakeDamage(Mathf.RoundToInt(attackDamageRate)); // Aplicar daño
                    elapsedTime += 1f;
                }
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, persecutionkRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, persecutionkRangeForPlayer);
    }
}