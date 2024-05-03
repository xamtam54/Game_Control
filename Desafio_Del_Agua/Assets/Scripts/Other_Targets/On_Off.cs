using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class On_Off : MonoBehaviour
{
    /*public Devices[] riceDevices;
    public Devices[] sorgoDevices; 
    public Devices[] sesameDevices;

    public Devices[] riceTank;
    public Devices[] sorgoTank;
    public Devices[] sesameTank;

    public Devices[] Tower;*/
    //------------------------------------------------------------

    private S_Targets sTargets;

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


    }
    public void RiceDevicesOn()
    {
        SetDevicesActive(sTargets.riceDevices, true);
    }
    public void RiceDevicesOff()
    {
        SetDevicesActive(sTargets.riceDevices, false);
    }
    //------------------------------------------------------------
    public void SorgoDevicesOn()
    {
        SetDevicesActive(sTargets.sorgoDevices, true);
    }
    public void SorgoDevicesOff()
    {
        SetDevicesActive(sTargets.sorgoDevices, false);
    }
    //------------------------------------------------------------
    public void SesameDevicesOn()
    {
        SetDevicesActive(sTargets.sesameDevices, true);
    }
    public void SesameDevicesOff()
    {
        SetDevicesActive(sTargets.sesameDevices, false);
    }
    //------------------------------------------------------------
    public void RiceTankOn()
    {
        SetDevicesActive(sTargets.riceTank, true);
    }
    public void RiceTankOff()
    {
        SetDevicesActive(sTargets.riceTank, false);
    }
    //------------------------------------------------------------
    public void SorgoTankOn()
    {
        SetDevicesActive(sTargets.sorgoTank, true);
    }
    public void SorgoTankOff()
    {
        SetDevicesActive(sTargets.sorgoTank, false);
    }
    //------------------------------------------------------------
    public void SesameTankOn()
    {
        SetDevicesActive(sTargets.sesameTank, true);
    }
    public void SesameTankOff()
    {
        SetDevicesActive(sTargets.sesameTank, false);
    }
    //------------------------------------------------------------
    public void TowerOn()
    {
        SetDevicesActive(sTargets.Tower, true);
    }
    public void TowerOff()
    {
        SetDevicesActive(sTargets.Tower, false);
    }
    //--------------------------------------------------------------------------------------------------------------------------

    private void SetDevicesActive(Devices[] devices, bool active)
    {
        foreach (var device in devices)
        {
            if (device != null)
            {
                device.IsActive = active;
            }
            else
            {
                Debug.LogWarning("Se encontró un dispositivo nulo en la lista.");
            }
        }
    }
}
