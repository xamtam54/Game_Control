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


    public float attackRange = 5f;
    public float attackDamageRate = 2f;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        isAttacking = false;                                                                                //temporal para definir estado de ataque

    }

    void Update()
    {

        LifeController lifeController = player.GetComponent<LifeController>();

            if (player != null && isAttacking && lifeController.isAlive)                                                                  //si el jugador empieza a atacar 
        {
            //float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            //if (distanceToPlayer <= persecutionkRange)
            //{
            agent.destination = player.position;
            Debug.Log("Atacando");
            //}
            if (player != null)
            {
                float distanceToTarget = Vector3.Distance(transform.position, player.position);
                if (distanceToTarget <= attackRange)
                {
                    StartCoroutine(ApplyDamageOverTime(player)); // Iniciar la corutina para aplicar daño periódico
                }

                agent.destination = player.position;
            }
        }
        else
        {


            Transform nearestTarget = GetNearestTargetWithTag("Plant");


            if (nearestTarget == null)
            {
                nearestTarget = GetNearestTargetWithTag("Water");
            }

            if (nearestTarget == null)
            {
                nearestTarget = GetNearestTargetWithTag("Device");
            }
            if (nearestTarget == null && persecutionkRange <= 500)
            {
                persecutionkRange = persecutionkRange + 2;
            }

            if (nearestTarget != null)
            {
                if (nearestTarget.CompareTag("Plant"))
                {
                    Debug.Log("Atacando planta");
                }
                else if (nearestTarget.CompareTag("Water"))
                {
                    Debug.Log("Atacando agua");
                }
                else if (nearestTarget.CompareTag("Device"))
                {
                    Debug.Log("Atacando máquina");
                }



                agent.destination = nearestTarget.position;


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
            }
        }

        return nearestTarget;
    }

    private IEnumerator ApplyDamageOverTime(Transform target)
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, persecutionkRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}