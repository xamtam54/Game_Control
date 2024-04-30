using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaver_control : MonoBehaviour
{
    public GameObject plantPrefab;
    public Transform[] spawnPoints;
    public string plantTag = "Weaver";

    void Start()
    {

        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject weaverInstance = Instantiate(plantPrefab, spawnPoint.position, spawnPoint.rotation);
            weaverInstance.transform.position = spawnPoint.position;
            weaverInstance.transform.rotation = spawnPoint.rotation;

            NewBehaviourScript ricePlant = weaverInstance.GetComponent<NewBehaviourScript>();
            if (ricePlant != null)
            {

                ricePlant.gameObject.tag = plantTag;
            }
            else
            {
                Debug.LogWarning("El prefab plantPrefab no tiene el componente Plants adjunto.");
            }
        }

      


    }
}
