using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform player;
    public Transform[] targets;
    private UnityEngine.AI.NavMeshAgent agent;
    private bool isAttacking;

    public float persecutionkRange = 20f;
    public float persecutionkRangeForPlayer = 19f;        //distancia para atacar al jugador si esta atacando

    public S_Targets sTargets;

    public float attackRange = 5f;
    public float attackDamageRate = 0.1f;

    bool isCooldown = false;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();


        S_plants();
    }

    

    void Update()
    {



        Player jugador = player.GetComponent<Player>();
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (player != null && jugador.isAttacking && distanceToPlayer <= persecutionkRangeForPlayer )                                                                  //si el jugador empieza a atacar 
        {
            if (distanceToPlayer <= attackRange && !isCooldown)
            {
                StartCoroutine(AttackCooldown());
                Debug.Log("Atacando al jugador");
                StartCoroutine(ApplyDamageOverTime(player));
            }
            else if (agent != null && agent.enabled)
            {
                agent.destination = player.position;
            }

        }
        else
        {
   

            Transform nearestTarget = null;
            
            if (nearestTarget == null)
            {
                nearestTarget = GetNearestTargetWithTag("waterTank");
            }

            if (nearestTarget == null) 
            { 
                nearestTarget = GetNearestTargetWithTag("Device");
            }

            if (nearestTarget == null)
            {
                nearestTarget = GetNearestTargetWithTag("Plant");
            }

            //no hay nada en rango
            if (nearestTarget == null && persecutionkRange <= 500)
            {
                persecutionkRange = persecutionkRange + 2;
            }
            
            
            if (nearestTarget != null)
            {
                float distanceToTarget = Vector3.Distance(transform.position, nearestTarget.position);
                if (distanceToTarget <= attackRange && !isCooldown)
                {
                    StartCoroutine(AttackCooldown());
                    StartCoroutine(ApplyDamageOverTime(nearestTarget)); 
                }
                else if (agent != null && agent.enabled)
                {
                    agent.destination = nearestTarget.position;
                }
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
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                bool isValidTarget = false;

                if (tag == "Plant")
                {
                    Plants plant = target.GetComponent<Plants>();
                    isValidTarget = (plant != null && plant.isAlive == 1);
                }
                else if (tag == "Device" || tag == "waterTank")
                {
                    Devices device = target.GetComponent<Devices>();
                    isValidTarget = (device != null && device.Status && device.IsActive);
                }

                if (isValidTarget && distanceToTarget <= persecutionkRange && distanceToTarget < shortestDistance)
                {
                    shortestDistance = distanceToTarget;
                    nearestTarget = target;
                }
            }
        }

        return nearestTarget;
    }

    public void S_plants()
    {
        S_Targets sTargets = FindObjectOfType<S_Targets>();
        if (sTargets != null)
        {
            sTargets.S_plants();
            this.targets = sTargets.targets;
        }
        else
        {
            Debug.LogError("No se encontr� ning�n objeto de la clase S_Targets en la escena.");
        }
    }

    private IEnumerator ApplyDamageOverTime(Transform target)
    {
        string tag = target.tag;
        Plants plant = null;
        Devices device = null;
        Player player = null;

        switch (tag)
        {
            case "Plant":
                plant = target.GetComponent<Plants>();
                if (plant != null)
                {
                    plant.UpdateHealth(1); 
                }
                break;
            case "Device":
            case "waterTank":
                device = target.GetComponent<Devices>();
                if (device != null)
                {
                    device.UpdateHealth(1); 
                }
                break;
            default:
                player = target.GetComponent<Player>();
                if (player != null)
                {
                    //animacion ataque
                    player.StunPlayer(5f); 

                }
                break;
        }

        if (plant != null)
        {
            plant.UpdateHealth(1); 
        }
        else if (device != null)
        {
            device.UpdateHealth(1); 
        }
        else if (player != null)
        {
            //animacion ataque
            player.StunPlayer(5f); 
        }

        yield return null; 
    }
    IEnumerator AttackCooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(2f); 
        isCooldown = false; 
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