using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sisriego1logic : MonoBehaviour
{
    
    public Camera camaraSis;
    
    
    public AudioListener CameraAudioListener;
    private bool isOnTheSmartPhone ;
    
    void Start()
    {
        isOnTheSmartPhone = true;
        camaraSis = GetComponent<Camera>();        
        CameraAudioListener = GetComponent<AudioListener>();
        CameraAudioListener.enabled = false;
        camaraSis.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isOnTheSmartPhone)
            {
                ActiveSisRiego(); 
            }
            else
            {
                DeactivateSisRiego();
            }
        }
    }
    void ActiveSisRiego()
    {
        Debug.Log("Se activo SisRiego");
        camaraSis.enabled = true;       
        CameraAudioListener.enabled = true;        
        // Cambiar el estado después de activar todo
        isOnTheSmartPhone = !isOnTheSmartPhone;       
    }

    void DeactivateSisRiego()
    {
        Debug.Log("Se desactivo SisRiego");
        camaraSis.enabled = false;
        CameraAudioListener.enabled = false;          
        isOnTheSmartPhone = !isOnTheSmartPhone;// Cambiar el estado después de desactivar todo
    }
}
