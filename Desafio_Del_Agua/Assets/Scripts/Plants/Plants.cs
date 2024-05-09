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

    public float max_Health = 1000f;                 // vida 
    public float currentHealth;                     // vida actual 

    public float lifespan;                          // cuantos dias tarda 

    public GameObject healthBar;                    // barra de vida de aqui tomar porcentaje - convertir despues a la barra amarilla

    private bool isOnTheSmartPhone = false;         //Control de aparicion de las barras de vida y el estatus

    [SerializeField] private Image _life;

    [SerializeField] private Image _Awa;

    [SerializeField] private Image _Progress;

    //logica de progreso vida, etc de la planta
    private float waterDecreaseRate = 0.5f; // Tasa de disminución de agua por segundo                  //40 segundos
    private float lifeDecreaseRate = 1.42f; // Tasa de disminución de vida por segundo                  70 segundos
    private float progressIncreaseRate = 0.5f; // Tasa de aumento de progreso por segundo            5 mins

    //public float currentWater = 1.0f; // Agua actual de la planta (0 a 1)
    //public float currentLife = 1.0f; // Vida actual de la planta (0 a 1)
    public float progress = 0.0f; // Progreso actual de la planta (0 a 1)
    public bool progressComplete = false; // Variable para verificar si el progreso llegó al 100%


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

        DecreaseWaterAndLifeOverTime();
        IncreaseProgressIfWatered();
        UpdateWaterBar();
        UpdateProgressBar(); 
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
        //Destroy(gameObject);                                                    // Destruye el objeto 
        gameObject.SetActive(false);
    }

    public void UpdateWaterBar()
    {
        _Awa.fillAmount = Actual_Water /( dailyWaterRequirements);
    }

    public void UpdateProgressBar()
    {
        _Progress.fillAmount = progress / 1.0f;
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

    void DecreaseWaterAndLifeOverTime()
    {
        if (Actual_Water > 0)
        {
            Actual_Water -= waterDecreaseRate * Time.deltaTime;
        }
        else
        {
            currentHealth -= lifeDecreaseRate * Time.deltaTime;
            UpdateHealthBar();
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void IncreaseProgressIfWatered()
    {
        if (Actual_Water > 0)
        {
            progress += progressIncreaseRate * Time.deltaTime;
            if (progress >= 1.0f)
            {
                progressComplete = true;
                _Progress.color = Color.white;
            }
        }
    }
}
