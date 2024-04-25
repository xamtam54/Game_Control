using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pauselogic : MonoBehaviour
{
    bool pausar = false;
    void Pause ()
    {
        if (pausar)
        {
            gameObject.SetActive(!pausar);
            
            Time.timeScale = 0f;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausar = !pausar;
        }
        
    }
}
