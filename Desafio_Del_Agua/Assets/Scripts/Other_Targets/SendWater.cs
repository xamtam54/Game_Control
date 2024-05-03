using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendWater : MonoBehaviour
{
    public S_Targets sTargets;
    public Devices device;

    public float valoraPasar = 0.1f;

    void Start()
    {
        sTargets = FindObjectOfType<S_Targets>(); 
        if (sTargets != null)
        {
            sTargets.S_Devices();
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
            SendWaterToDevices(sTargets.devicestosend, valoraPasar);

            
        }
        else
        {
            Debug.LogError("No se encontró nin");
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
                        if (obj.WaterManagementType != null && obj.Max_Water >= amount)          //que sea un tanque y tenga espacio
                        {
                            device.Actual_Water -= amount;                        //resta agua al dispositivo

                            obj.Actual_Water += amount;           //suma agua al otro
                                                                  // Debug.Log(DeviceName + " ha enviado " + amount + " litros de agua a " + targetDevice.DeviceName);
                            device.UpdateWaterBar();
                            obj.UpdateWaterBar();
                        }
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
