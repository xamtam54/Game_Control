using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; 

public class Pauselogic : MonoBehaviour
{
//para pausar----------------------
    public UIDocument uIdocument;
    private bool isPaused = false;
    //---------------------------------

    //para botones------------------------
    private UIDocument _document;
    private Button _button1;
    //------------------------------
    private void Awake()
    {
        Resume();
    }
    void Update()
    {
        //pausar y reanudar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        //-----------------------
    }
    //Metodos de pausar y reanudar------------------------------------------
    void Resume()
    {
        Debug.Log("Se Reanudó");
        uIdocument.enabled = false;
        Time.timeScale = 1f; // Vuelve al tiempo normal
        isPaused = !isPaused;
    }

    void Pause()
    {
        Debug.Log("Se pausó");
        uIdocument.enabled = true;
        Time.timeScale = 0f; // Pausa el tiempo en el juego
        Boton();
        isPaused = !isPaused;
    }

//----------------------------------------
    private void Boton()
    {
        _document = GetComponent<UIDocument>();
        _button1 = _document.rootVisualElement.Q<Button>("mainmenu") as Button;
        _button1.RegisterCallback<ClickEvent>(OnMenuClick);
    }

    private void OnDisable()
    {
        _button1.UnregisterCallback<ClickEvent>(OnMenuClick);
    }

    private void OnMenuClick(ClickEvent evt)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
//------------------------------------------------------

