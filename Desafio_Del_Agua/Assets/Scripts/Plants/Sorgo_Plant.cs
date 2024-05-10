using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorgo_Plant : MonoBehaviour
{
    public GameObject plantPrefab;
    public Transform[] spawnPoints;
    public string plantTag = "Plant";
    public string plantLayer = "Sorgo";

    void Start()
    {
        int layerIndex = LayerMask.NameToLayer(plantLayer);

        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject plantInstance = Instantiate(plantPrefab, spawnPoint.position, spawnPoint.rotation);
            plantInstance.transform.position = spawnPoint.position;
            plantInstance.transform.rotation = spawnPoint.rotation;

            Plants sorgoPlant = plantInstance.GetComponent<Plants>();
            if (sorgoPlant != null)
            {
                sorgoPlant.InitializePlant("Sorgo", "Sorghum", 6.2f, 7.8f, 20f, 10f, 1, 125f);
                sorgoPlant.gameObject.tag = plantTag;
                plantInstance.layer = layerIndex;
            }
            else
            {
                Debug.LogWarning("El prefab plantPrefab no tiene el componente Plants adjunto.");
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
