using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaver_control : MonoBehaviour
{
    public GameObject plantPrefab;
    public Transform[] spawnPoints;
    public string plantTag = "Weaver";
    public float initialDelay = 5f;
    public float repeatDelay = 90f;

    void Start()
    {
        InvokeRepeating("SpawnWeaver", initialDelay, repeatDelay);
    }

    void SpawnWeaver()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject weaverInstance = Instantiate(plantPrefab, spawnPoint.position, spawnPoint.rotation);

                weaverInstance.transform.position = spawnPoint.position;
                weaverInstance.transform.rotation = spawnPoint.rotation;

                weaverInstance.tag = plantTag;
            }
        }
    }
}
