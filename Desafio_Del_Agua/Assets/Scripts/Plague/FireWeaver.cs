using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeaver : MonoBehaviour
{
    public int damageOnDeath = 10;
    public float damageRadius = 5f;

    void OnDestroy()
    {
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");

        foreach (GameObject boss in bosses)
        {
            float distance = Vector3.Distance(transform.position, boss.transform.position);

            if (distance < damageRadius)
            {
                Boss bossComponent = boss.GetComponent<Boss>();

                if (bossComponent != null)
                {
                    bossComponent.TakeDamage(damageOnDeath);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
