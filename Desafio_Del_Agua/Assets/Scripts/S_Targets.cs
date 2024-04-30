using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Targets : MonoBehaviour
{

    public Transform[] targets;

    public void S_plants()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        List<Transform> plantTargets = new List<Transform>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("Plant") || obj.CompareTag("Device") || obj.CompareTag("Player"))
            {
                plantTargets.Add(obj.transform);
            }
        }


        this.targets = plantTargets.ToArray();
    }
}
