using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercamera_sis_logic : MonoBehaviour
{
    public Camera camaraplayer;
    public AudioListener PlayerAudioListener;
    private bool isOnTheSmartPhone ;


    void Start()
    {
        isOnTheSmartPhone = true;
        camaraplayer = GetComponent<Camera>();
        PlayerAudioListener = GetComponent<AudioListener>();
    }


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
        camaraplayer.enabled = false;
        PlayerAudioListener.enabled = false;
        isOnTheSmartPhone = !isOnTheSmartPhone;
    }

    void DeactivateSisRiego()
    {
        camaraplayer.enabled = true;
        PlayerAudioListener.enabled = true;
        isOnTheSmartPhone = !isOnTheSmartPhone;
    }
}
