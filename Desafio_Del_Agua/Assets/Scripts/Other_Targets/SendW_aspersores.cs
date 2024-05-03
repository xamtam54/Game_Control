using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendW_aspersores : MonoBehaviour
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
        device.UpdateWaterBar();
    }

    void Update()
    {
        if (sTargets != null)
        {
            Devices[] devicesToSendWater = null;

            if (device != null && device.DeviceName == "Tanque de agua")
            {
                string layerName = LayerMask.LayerToName(gameObject.layer);
                switch (layerName)
                {
                    case "Rice":
                        devicesToSendWater = sTargets.riceDevices;
                        break;
                    case "Sorgo":
                        devicesToSendWater = sTargets.sorgoDevices;
                        break;
                    case "Sesame":
                        devicesToSendWater = sTargets.sesameDevices;
                        break;
                    default:
                        Debug.LogError("La capa del aspersor no está configurada correctamente.");
                        break;
                }

                if (devicesToSendWater != null)
                {
                    SendWaterToDevices(devicesToSendWater, valoraPasar);
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


    public void SendWaterToDevices(Devices[] targetDevice, float amount)
    {
        foreach (var obj in targetDevice)
        {
            if (obj != null)
            {
                
                if (obj != null && obj.IsActive && obj.Status && device.WaterManagementType != null && amount > 0 && device.IsActive && device.Status)     //Que el objetivo no sea nulo, que este activo, que este vivo, que el tipo de manejo no sea nulo y que la cantidad que mande sea mayor a 0.
                {
                    
                    if (device.Actual_Water >= amount && obj.Actual_Water < obj.Max_Water)                    //tiene sufuciente agua y el destinatario suficiente espacio?
                    {
                        
                            device.Actual_Water -= amount;                        //resta agua al dispositivo

                            obj.Actual_Water += amount;           //suma agua al otro
                                                                  // Debug.Log(DeviceName + " ha enviado " + amount + " litros de agua a " + targetDevice.DeviceName);
                            device.UpdateWaterBar();
                        
                    }
                }
            }
            else
            {
                Debug.LogWarning("Se encontró un objeto nulo en el array targetDevice.");
            }
        }
    }
}
