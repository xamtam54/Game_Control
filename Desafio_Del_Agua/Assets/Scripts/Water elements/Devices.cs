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

    public Devices Enviar_a;
    public float valoraPasar;
    //private bool pasarAgua;                         //USAR CUANDO SE TENGAN LOS BOTONES PARA SISRIEGO
    [SerializeField] private Image _Awa;
    
    public void Start()
    {
        // pasarAgua = false;

        SendWater(Enviar_a, valoraPasar);
        _life.enabled = false;

        if (DeviceName == "")
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        UpdateWaterBar();
            
      SendWater(Enviar_a, valoraPasar);
                  
        

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

    
    private void UpdateWaterBar()
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

    public void SendWater(Devices targetDevice, float amount)
    {
        if (targetDevice != null && targetDevice.IsActive && targetDevice.Status && WaterManagementType != null && amount >0 )     //Que el objetivo no sea nulo, que este activo, que este vivo, que el tipo de manejo no sea nulo y que la cantidad que mande sea mayor a 0.
        {
            
            
            if (Actual_Water >= amount && targetDevice.Actual_Water < targetDevice.Max_Water)                    //tiene sufuciente agua y el destinatario suficiente espacio?
            {
                
                if (targetDevice.ActuatorName == "Aspersor" || targetDevice.ActuatorName == "Tuberia" )         //que sea un aspersor - tuberia    se tiene que enviar a un conjunto? o que solo la animacion se active? si mejor que solo se active gracias a una funcion asi se evitan problemas con la cantidad de agua trasferida a las plantas
                {
                    Actual_Water -= amount;                        //resta agua al dispositivo
                    targetDevice.Actual_Water += amount;           //suma agua al otro

                    //Debug.Log(DeviceName + " ha enviado " + amount + " litros de agua a " + targetDevice.DeviceName);
                    //empiece animacion     8888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888
                    foreach (Plants plant in targetDevice.PlantData)
                    {
                        plant.Actual_Water = targetDevice.Actual_Water;
                    }
                    targetDevice.Actual_Water = 0;                                             //se envia a las plantas por lo que se resta
                    //termina animacion     888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888
                }
                else if (targetDevice.WaterManagementType != null && targetDevice.Max_Water >= amount)          //que sea un tanque y tenga espacio
                {
                    Actual_Water -= amount;                        //resta agua al dispositivo

                    targetDevice.Actual_Water += amount;           //suma agua al otro
                                                                   // Debug.Log(DeviceName + " ha enviado " + amount + " litros de agua a " + targetDevice.DeviceName);
                }
            }
            else
            {
               // Debug.Log(DeviceName + " no tiene suficiente agua para enviar a " + targetDevice.DeviceName + " o espacio");
            }

            
        }
        else
        {
          //  Debug.Log("No se puede enviar agua de " + DeviceName + " a " + targetDevice.DeviceName + " porque el dispositivo de destino no está activo o está dañado.");
        }
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
