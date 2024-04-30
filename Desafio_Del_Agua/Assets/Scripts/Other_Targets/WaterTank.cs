using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTank : MonoBehaviour
{
    public GameObject _waterTankPrefab;
    public Transform[] spawnPoints;
    public string plantTag = "Device";

    void Start()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject waterTankInstance = Instantiate(_waterTankPrefab, spawnPoint.position, spawnPoint.rotation);
            waterTankInstance.transform.position = spawnPoint.position;
            waterTankInstance.transform.rotation = spawnPoint.rotation;

            Devices waterTank = waterTankInstance.GetComponent<Devices>();

            if (waterTank != null)
            {
                waterTank.CreateDevice("Tanque de agua", true, true, null, "Medidor de agua", "Sensor de nivel", "Litros", null, 20f, 15f);
                waterTank.gameObject.tag = plantTag;
               
               // Debug.Log("Estado del Tanque de agua: " + waterTank.Status);
            }
            else
            {
                Debug.LogWarning("El prefab _waterTankPrefab no tiene el componente Plants adjunto.");
            }
        }

        
        NewBehaviourScript newBehaviourScript = FindObjectOfType<NewBehaviourScript>();
        if (newBehaviourScript == null)
        {
            Debug.LogError("No se encontró enemy_bh");
            return;
        }
        
        newBehaviourScript.S_plants();
        
        
    }
}
