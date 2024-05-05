using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public GameObject objectToActivate;
    private float activationDelay = 180f; // 180 segundos

    void Start()
    {
        Invoke("ActivateObject", activationDelay);
    }

    void ActivateObject()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
        else
        {
            Debug.LogWarning("¡El objeto a activar no está asignado!");
        }
    }
}
