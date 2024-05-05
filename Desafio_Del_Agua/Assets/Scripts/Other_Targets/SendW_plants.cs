using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SendW_plants : MonoBehaviour
{
    public S_Targets sTargets;
    public Devices device;

    public float valoraPasar = 0.1f;

    void Start()
    {
        sTargets = FindObjectOfType<S_Targets>();
        if (sTargets != null)
        {
            sTargets.S_plants();
        }
        else
        {
            Debug.LogError("No se encontró ningún objeto de la clase S_Targets en la escena.");
        }

        device = GetComponent<Devices>();
        
    }

    void Update()
    {
        if (sTargets != null)
        {
            Plants[] plantsToSendWater = null;

            if (device != null && (device.DeviceName == "Aspersor" || device.DeviceName == "Tuberia"))
            {
                string layerName = LayerMask.LayerToName(gameObject.layer);
                switch (layerName)
                {
                    case "Rice":
                        plantsToSendWater = sTargets.ricePlants;
                        break;
                    case "Sorgo":
                        plantsToSendWater = sTargets.sorgoPlants;
                        break;
                    case "Sesame":
                        plantsToSendWater = sTargets.sesamePlants;
                        break;
                    default:
                        Debug.LogError("La capa del aspersor no está configurada correctamente.");
                        break;
                }

                if (plantsToSendWater != null)
                {
                    
                   SendWaterToPlants(plantsToSendWater, valoraPasar);
                }
            }
            else
            {
                Debug.LogError("Este objeto no es un aspersor.");
            }
        }
        else
        {
            Debug.LogError("No se encontró el objeto S_Targets.");
        }
    }

    public void SendWaterToPlants(Plants[] targetPlants, float amount)
    {
        foreach (var plant in targetPlants)
        {
            
            if (plant != null)
            {   
                if (plant.isAlive == 1 && device.ActuatorName != null && amount > 0 && device.IsActive && device.Status) //  que este viva  que sea un actuador que envie mas que cero y que este bien
                {
                   
                    if (device.Actual_Water >= amount)                      //tiene sufuciente agua y al ser planta no necesita espacio
                    {
                        //resta agua
                        if (plant.Actual_Water <= plant.dailyWaterRequirements)           //capacidad maxima = plant.dailyWaterRequirements 
                        {
                            device.Actual_Water -= amount;        
                            plant.Actual_Water += amount * targetPlants.Length;                   //suma agua y si no se controla se desperdicia
                        }
                        if (Array.IndexOf(targetPlants, plant) == targetPlants.Length - 1 && plant.Actual_Water >= plant.dailyWaterRequirements * 2) //si esta en el ultimo elemento del arreglo y ese elemento esta lleno 
                        {
                            device.Actual_Water -= amount;
                        }

                        plant.UpdateWaterBar();

                        
                        //tambien actualizar de la planta
                    }
                }
                else
                {
                   // Debug.LogWarning("Se encontró un objeto nulo en el array de plantas.");
                }
            }
            else
            {
                Debug.Log("planta nuañ");
            }
        }
    }
}
