using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rice_Plant : MonoBehaviour
{
    public GameObject plantPrefab; 
    public Transform[] spawnPoints;
    public string plantTag = "Plant";

    void Start()
    {
        
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject plantInstance = Instantiate(plantPrefab, spawnPoint.position, spawnPoint.rotation);
            plantInstance.transform.position = spawnPoint.position;
            plantInstance.transform.rotation = spawnPoint.rotation;

            Plants ricePlant = plantInstance.GetComponent<Plants>();
            if (ricePlant != null)
            {
                
                ricePlant.InitializePlant("Arroz", "Oryza sativa", 5.5f, 7.0f, 20f, 1, 125f);
                ricePlant.gameObject.tag = plantTag;
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
