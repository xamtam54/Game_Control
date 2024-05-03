using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Devices : MonoBehaviour
{
    public string DeviceName;                     // Nombre del dispositivo
    public bool IsActive;                         // Estado del dispositivo (desactivado activado o )
    public bool SisRiegoStatus;                    //Estado del sistema de riego(si le da a la "e" se muestra/oculta el canvas)
    public bool Status;                           // Estado del dispositivo (dañado o bien)
    public string ActuatorName;                   // Nombre del actuador (puede ser null)
    public string WaterManagementType;            // Tipo de gestión de agua (puede ser null)
    public string SensorType;                     // Tipo de sensor (puede ser null)
    public string MeasureUnit;                    // Unidad de medida (puede ser null)
    public string SensorData;                     // Data del sensor

    public GameObject WaterLevel;                  //objeto de tipo canvas que muestra el nivel del agua del Enviar_a
    
    public float Max_Water;                       // maximo de agua que puede contener o sacar en caso de ser aspersor
    public float Actual_Water;                    // cuanto tiene ahora (puede ser null) solo si es un tanque

    public Plants[] PlantData;                      // Data de las plantas
    //-----------------------------------------------------------------------------------------------------------
    public float max_Health = 100f;                 // vida 
    public float currentHealth;                     // vida actual 

    //public GameObject healthBar;                    // barra de vida de aqui tomar porcentaje ----Nk no se requiere ya que los tanques o los dispositivos no tienen vida como tal

    [SerializeField] private Image _life;

    //private bool pasarAgua;                         //USAR CUANDO SE TENGAN LOS BOTONES PARA SISRIEGO
    [SerializeField] private Image _Awa;

    //---------------nuevo

    public void Start()
    {
        
        _life.enabled = false;

        if (DeviceName == "")
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && WaterLevel!=null)
        {
            if (SisRiegoStatus)
            {
                ShowWater();
            }
            else
            {
                HideWater();
            }
        }
    }
    // Constructor
    public void CreateDevice(string deviceName, bool isActive, bool status, string actuatorName, string waterManagementType, string sensorType, string measureUnit, Plants[] plantData, float Max_Water, float Actual_Water)
    {
        this.DeviceName = deviceName;
        this.IsActive = isActive;
        this.Status = status;
        this.ActuatorName = actuatorName;
        this.WaterManagementType = waterManagementType;
        this.SensorType = sensorType;
        this.MeasureUnit = measureUnit;
        this.PlantData = plantData;
        this.currentHealth = max_Health;
        this.Max_Water = Max_Water;
        this.Actual_Water = Actual_Water;

        gameObject.SetActive(true);
    }

    public void UpdateHealth(int damage)
    {
        if (Status)
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
        _life.enabled = true;
    }

    
    public void UpdateWaterBar()
    {
        _Awa.fillAmount = Actual_Water / Max_Water;
    }

    // muerte
    private void Die()
    {
        Status = false; 
        IsActive = false;          //dañado
        if (WaterManagementType != null)
        {
            Actual_Water = Actual_Water - 5f;
        }
       
        // Cambiar animacion a dañado

    }


    void ShowWater()
    {
        WaterLevel.SetActive(false);
        SisRiegoStatus = !SisRiegoStatus;
    }

    void HideWater()
    {
        WaterLevel.SetActive(true);
        SisRiegoStatus = !SisRiegoStatus;
    }

}
