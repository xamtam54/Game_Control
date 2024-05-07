using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocultar_mostrar : MonoBehaviour
{
    public GameObject canvas;
    private bool isOnTheSmartPhone = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOnTheSmartPhone = !isOnTheSmartPhone;
            if (isOnTheSmartPhone)
            {
                ActiveSisRiego();
            }
            else
            {
                DeactiveSisRiego();
            }
        }
    }
    void ActiveSisRiego()
    {
        canvas.SetActive(true);
    }    
    void DeactiveSisRiego()
    {
        canvas.SetActive(false);
    }
}
