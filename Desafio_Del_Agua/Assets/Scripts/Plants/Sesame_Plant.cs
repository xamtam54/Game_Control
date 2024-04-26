using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sesame_Plant : MonoBehaviour
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

            Plants sesamePlant = plantInstance.GetComponent<Plants>();
            if (sesamePlant != null)
            {
                sesamePlant.InitializePlant("Ajonjolí", "Sesamum indicum", 5.4f, 6.7f, 350f, 1, 100f);
                sesamePlant.gameObject.tag = plantTag;
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
