using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public GameObject _sensorPrefab;
    public Transform[] spawnPoints;
    public string plantTag = "Device";

    void Start()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject sensorInstance = Instantiate(_sensorPrefab, spawnPoint.position, spawnPoint.rotation);
            sensorInstance.transform.position = spawnPoint.position;
            sensorInstance.transform.rotation = spawnPoint.rotation;

            Devices sensor = sensorInstance.GetComponent<Devices>();

            if (sensor != null)
            {
                sensor.CreateDevice("Sensor", true, true, null, null, "Sensor de humedad", "Litros", null, 0f, 0f);
                sensor.gameObject.tag = plantTag;

                Debug.Log("Estado del Tanque de agua: " + sensor.Status);
            }
            else
            {
                Debug.LogWarning("El prefab _sensorPrefab no tiene el componente Plants adjunto.");
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
