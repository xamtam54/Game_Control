using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class S_Targets : MonoBehaviour
{

    public Transform[] targets;
    public Transform[] devices;
    public Devices[] devicestosend;

    public Plants[] ricePlants; // layer "Rice"
    public Plants[] sorgoPlants; // layer "Sorgo"
    public Plants[] sesamePlants; // layer "Sesame"

    public Devices[] riceDevices;
    public Devices[] sorgoDevices; 
    public Devices[] sesameDevices;

    public Devices[] riceTank;
    public Devices[] sorgoTank;
    public Devices[] sesameTank;

    public Devices[] Tower;

    public void S_plants()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        List<Plants> ricePlantsList = new List<Plants>();
        List<Plants> sorgoPlantsList = new List<Plants>();
        List<Plants> sesamePlantsList = new List<Plants>();

        foreach (GameObject obj in allObjects)
        {
            Plants plantComponent = obj.GetComponent<Plants>(); 

            if (plantComponent != null)
            {
                if (obj.CompareTag("Plant") && obj.layer == LayerMask.NameToLayer("Rice"))
                {
                    ricePlantsList.Add(plantComponent);
                }
                else if (obj.CompareTag("Plant") && obj.layer == LayerMask.NameToLayer("Sorgo"))
                {
                    sorgoPlantsList.Add(plantComponent);
                }
                else if (obj.CompareTag("Plant") && obj.layer == LayerMask.NameToLayer("Sesame"))
                {
                    sesamePlantsList.Add(plantComponent);
                }
            }
        }

        this.ricePlants = ricePlantsList.ToArray();
        this.sorgoPlants = sorgoPlantsList.ToArray();
        this.sesamePlants = sesamePlantsList.ToArray();

        //-----------------------------------------------------------------------
        List<Devices> riceDevicesList = new List<Devices>();
        List<Devices> sorgoDevicesList = new List<Devices>(); 
        List<Devices> sesameDevicesList = new List<Devices>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("Device"))
            {
                if (obj.layer == LayerMask.NameToLayer("Rice"))
                {
                    Devices device = obj.GetComponent<Devices>();
                    if (device != null)
                    {
                        riceDevicesList.Add(device);
                    }
                }
                else if (obj.layer == LayerMask.NameToLayer("Sorgo"))
                {
                    Devices device = obj.GetComponent<Devices>();
                    if (device != null)
                    {
                        sorgoDevicesList.Add(device);
                    }
                }
                else if (obj.layer == LayerMask.NameToLayer("Sesame"))
                {
                    Devices device = obj.GetComponent<Devices>();
                    if (device != null)
                    {
                        sesameDevicesList.Add(device);
                    }
                }
            }
        }

        riceDevices = riceDevicesList.ToArray();
        sorgoDevices = sorgoDevicesList.ToArray();
        sesameDevices = sesameDevicesList.ToArray();
        //-------------------------------------------------------------------------
        List<Devices> riceTanksList = new List<Devices>();
        List<Devices> sorgoTanksList = new List<Devices>();
        List<Devices> sesameTanksList = new List<Devices>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("waterTank"))
            {
                if (obj.layer == LayerMask.NameToLayer("Rice"))
                {
                    Devices device = obj.GetComponent<Devices>();
                    if (device != null)
                    {
                        riceTanksList.Add(device);
                    }
                }
                else if (obj.layer == LayerMask.NameToLayer("Sorgo"))
                {
                    Devices device = obj.GetComponent<Devices>();
                    if (device != null)
                    {
                        sorgoTanksList.Add(device);
                    }
                }
                else if (obj.layer == LayerMask.NameToLayer("Sesame"))
                {
                    Devices device = obj.GetComponent<Devices>();
                    if (device != null)
                    {
                        sesameTanksList.Add(device);
                    }
                }
            }
        }

        riceTank = riceTanksList.ToArray();
        sorgoTank = sorgoTanksList.ToArray();
        sesameTank = sesameTanksList.ToArray();
        //-------------------------------------------------------------------------

        List<Devices> towerList = new List<Devices>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("Device"))
            {
                if (obj.layer == LayerMask.NameToLayer("WaterTower"))
                {
                    Devices device = obj.GetComponent<Devices>();
                    if (device != null)
                    {
                        towerList.Add(device);
                    }
                }
            }
        }

        Tower = towerList.ToArray();
        //-------------------------------------------------------------------------

        List<Transform> plantTargets = new List<Transform>();

        foreach (GameObject obj in allObjects)
        {
            if ((obj.CompareTag("Plant") || obj.CompareTag("Device") || obj.CompareTag("Player") || obj.CompareTag("waterTank")) && obj.layer != LayerMask.NameToLayer("WaterTower"))
            {
                plantTargets.Add(obj.transform);
            }
        }
        S_Devices();

        this.targets = plantTargets.ToArray();
    }

    public void S_Devices()
    {
        //Debug.Log("S_Devices() called");
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        List<Transform> plantTargets = new List<Transform>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("waterTank"))
            {
                plantTargets.Add(obj.transform);
            }
        }
        

        this.devices = plantTargets.ToArray();

        ConvertTransformsToDevices();
    }

    void ConvertTransformsToDevices()
    {
        devicestosend = new Devices[devices.Length];

        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i] != null)
            {
                devicestosend[i] = devices[i].GetComponent<Devices>();
                //Debug.Log();
            }
        }
    }

}
