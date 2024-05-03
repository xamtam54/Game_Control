using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public GameObject _pipe;
    public Transform[] spawnPoints;
    public string plantTag = "Device";
    public string deviceLayer = "Rice";

    void Start()
    {
        int layerIndex = LayerMask.NameToLayer(deviceLayer);

        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject pipeInstance = Instantiate(_pipe, spawnPoint.position, spawnPoint.rotation);
            pipeInstance.transform.position = spawnPoint.position;
            pipeInstance.transform.rotation = spawnPoint.rotation;

            Devices pipe = pipeInstance.GetComponent<Devices>();

            if (pipe != null)
            {
                pipe.CreateDevice("Tuberia", false, true, "Tuberia1",null ,null , "Litros", null, 1f, 0f);
                pipe.gameObject.tag = plantTag;
                pipeInstance.layer = layerIndex;

                //Debug.Log("Estado del Tanque de agua: " + pipe.Status);
            }
            else
            {
                Debug.LogWarning("El prefab _pipe no tiene el componente Plants adjunto.");
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
