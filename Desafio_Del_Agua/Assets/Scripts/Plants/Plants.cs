using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Plants : MonoBehaviour
{
    //data
    public string plant_Name;
    public string specie;
    public float min_PH;
    public float max_PH;
    public float dailyWaterRequirements;
    public int isAlive;                             //para crop_status 1 vivo 2 muerto

    public float Actual_Water;                      //agua esta se debe restar en el tiempo

    public float max_Health = 100f;                 // vida 
    public float currentHealth;                     // vida actual 
    public float lifespan;                          // cuantos dias tarda 

    public GameObject healthBar;                    // barra de vida de aqui tomar porcentaje - convertir despues a la barra amarilla

    private bool isOnTheSmartPhone = false;         //Control de aparicion de las barras de vida y el estatus

    [SerializeField] private Image _life;

    [SerializeField] private Image _Awa;

    void Start()
    {
        UpdateWaterBar();

            healthBar.SetActive(false); //inicia las barras de vida escondidas
            if (plant_Name == "")
            {
                gameObject.SetActive(false);
            }
    }

    void Update()
    {
        //mostrar/ocultar barras de vida
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isOnTheSmartPhone)
            {
                MostrarVida();
            }
            else
            {
                OcultarVida();
            }
        }
    }

    // crear
    public void InitializePlant(string name, string species, float minPH, float maxPH, float waterRequirements, float Actual_Water, int isAlive, float lifespan)
    {
        this.plant_Name = name;
        this.specie = species;
        this.min_PH = minPH;
        this.max_PH = maxPH;
        this.dailyWaterRequirements = waterRequirements;
        this.lifespan = lifespan;
        this.isAlive = isAlive;
        this.Actual_Water = Actual_Water;
        this.currentHealth = max_Health;
        gameObject.SetActive(true);
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
  
    private void UpdateHealthBar()
    {
        _life.fillAmount = currentHealth / max_Health;
    }

    // muerte
    private void Die()
    {
        isAlive = 2;
        Debug.Log("La planta " + plant_Name + " ha muerto.");
        //Destroy(gameObject);                                                    // Destruye el objeto 
        gameObject.SetActive(false);
    }

    public void UpdateWaterBar()
    {
        _Awa.fillAmount = Actual_Water /( dailyWaterRequirements * 2);
    }

    //Mostrar vida
    void MostrarVida()
    {
        healthBar.SetActive(false);
        isOnTheSmartPhone = !isOnTheSmartPhone;
    }

    //Ocultar vida
    void OcultarVida()
    {
        healthBar.SetActive(true);
        isOnTheSmartPhone = !isOnTheSmartPhone;
    }
}
