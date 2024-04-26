using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Sisriego1logic : MonoBehaviour
{
    public UIDocument uIDocument;
    public GameObject camaraSis;
    public GameObject camaraplayer;
    private bool isOnTheSmartPhone = false;


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
        uIDocument.enabled = true;
        camaraSis.SetActive(true);
        camaraplayer.SetActive(false);
        isOnTheSmartPhone =!isOnTheSmartPhone;
    }

    void DeactivateSisRiego()
    {
        Debug.Log("Se desactivo SisRiego");
        uIDocument.enabled = false;
        camaraSis.SetActive(false);
        camaraplayer.SetActive(true);
        isOnTheSmartPhone = !isOnTheSmartPhone;
    }
}
