using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprinkler : MonoBehaviour
{
    public GameObject _sprinkler;
    public Transform[] spawnPoints;
    public string plantTag = "Device";

    void Start()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject sprinklerInstance = Instantiate(_sprinkler, spawnPoint.position, spawnPoint.rotation);
            sprinklerInstance.transform.position = spawnPoint.position;
            sprinklerInstance.transform.rotation = spawnPoint.rotation;

            Devices sprinkler = sprinklerInstance.GetComponent<Devices>();

            if (sprinkler != null)
            {
                sprinkler.CreateDevice("Aspersor", true, true, "Aspersor1", null, null, "Litros", null, 0f, 0f);
                sprinkler.gameObject.tag = plantTag;

                Debug.Log("Estado del Tanque de agua: " + sprinkler.Status);
            }
            else
            {
                Debug.LogWarning("El prefab _sprinkler no tiene el componente Plants adjunto.");
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
