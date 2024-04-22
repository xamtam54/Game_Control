using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public bool isAlive = true;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if (isAlive)
        {
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        isAlive = false;
        gameObject.SetActive(false); 
    }
}
