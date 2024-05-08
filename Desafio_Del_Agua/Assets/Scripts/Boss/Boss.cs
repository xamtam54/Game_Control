using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int maxHealth = 100; 
    private int currentHealth;   
    public bool isAlive = true;

    public Image healthBar; 


    public Transform[] waypoints;   
    public float moveSpeed = 5f;    

    private int currentWaypointIndex = 0;  

    void Start()
    {
        currentHealth = maxHealth;

        if (waypoints.Length == 0)
        {
            Debug.LogError("No se han definido waypoints para el movimiento del jefe.");
            enabled = false; 
        }
    }

    void Update()
    {
        
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, moveSpeed * Time.deltaTime);

        UpdateHealthBar();
    }

    // M�todo para actualizar la barra de vida
    void UpdateHealthBar()
    {
        // Asegurarse de que la referencia a la barra de salud no sea nula y que la vida m�xima sea mayor que cero
        if (healthBar != null && maxHealth > 0)
        {
            // Calcular el llenado de la barra de salud en funci�n de la vida actual y m�xima del jefe
            float fillAmount = (float)currentHealth / maxHealth;

            // Establecer el llenado de la barra de salud
            healthBar.fillAmount = fillAmount;
        }
    }

    // M�todo para recibir da�o
    public void TakeDamage(int damage)
    {
        // Restar el da�o de la vida actual del jefe
        currentHealth -= damage;

        // Asegurarse de que la vida no sea negativa
        currentHealth = Mathf.Max(currentHealth, 0);

        if (currentHealth <= 0) {
            die();
        }
    }

    public void die()
    {
        isAlive = false;
        gameObject.SetActive(false);
    }
}
