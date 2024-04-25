using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plants : MonoBehaviour
{
    //data
    public string plant_Name;
    public string specie;
    public float min_PH;
    public float max_PH;
    public float dailyWaterRequirements;
    public int isAlive;                             //para crop_status 1 vivo 2 muerto
     
    public float max_Health = 100f;                 // vida 
    public float currentHealth;                     // vida actual 
    public float lifespan;                          // cuantos dias tarda 

    public GameObject healthBar;                    // barra de vida de aqui tomar porcentaje

    // crear
    public void InitializePlant(string name, string species, float minPH, float maxPH, float waterRequirements, int isAlive, float lifespan)
    {
        this.plant_Name = name;
        this.specie = species;
        this.min_PH = minPH;
        this.max_PH = maxPH;
        this.dailyWaterRequirements = waterRequirements;
        this.lifespan = lifespan;
        this.isAlive = isAlive;
        this.currentHealth = max_Health; 
    }

    // manejo de vida
    public void UpdateHealth(int damage)
    {
        if (isAlive == 1)
        {
            currentHealth -= damage;
            UpdateHealthBar();
            if (currentHealth <= 0)
            {
                Die();
            }
        }
        
    }
  

    // barra de vida
    private void UpdateHealthBar()
    {
        //float healthPercentage = currentHealth / max_Health;
        //healthBar.transform.localScale = new Vector3(healthPercentage, 1f, 1f);
    }

    // muerte
    private void Die()
    {
        isAlive = 2;
        Debug.Log("La planta " + plant_Name + " ha muerto.");
        //Destroy(gameObject);                                                    // Destruye el objeto 
        gameObject.SetActive(false);
    }

    
}
