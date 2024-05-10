using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 2f; 
    public LayerMask attackLayer;  
    public int damagePerHit = 1;
    public float attackCooldown = 1f;
    private bool quieto;

    private float lastAttackTime;
    void Start()
    {
        quieto = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        { quieto = !quieto; }

        if (!quieto)
        {
            if (Input.GetMouseButtonDown(0) && Time.time - lastAttackTime >= attackCooldown)
            {
                Attack();

                lastAttackTime = Time.time;
            }
        }
    }

    void Attack()
    {
        if (!quieto)
        {
            RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, attackRange, attackLayer);

            foreach (RaycastHit hit in hits)
            {
                WeaverLife weaverLife = hit.collider.GetComponent<WeaverLife>();

                if (weaverLife != null)
                {
                    weaverLife.ReceiveDamage(damagePerHit);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * attackRange, 0.2f);
    }
}
