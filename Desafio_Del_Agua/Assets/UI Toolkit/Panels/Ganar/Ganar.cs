using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Ganar : MonoBehaviour
{
    public UIDocument uIDocument;
    public WIN winScript; // Referencia al script WIN
    private Button _button1;
    private Button _button2;

    void Awake()
    {
        Time.timeScale = 1f;
        uIDocument = GetComponent<UIDocument>();
    }

    void Update()
    {
        if (winScript != null && winScript.won)
        {
            uIDocument.enabled = true;
            Time.timeScale = 0f;
            Reinicio_captura();
            Volver_Menu();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Reinicia la escena que esta cargada
    }

    public void Reinicio_captura()
    {
        _button1 = uIDocument.rootVisualElement.Q<Button>("Reiniciar") as Button;
        _button1.RegisterCallback<ClickEvent>(evt => Restart());
    }

    public void Volver_Menu()
    {
        _button2 = uIDocument.rootVisualElement.Q<Button>("Menu") as Button;
        _button2.RegisterCallback<ClickEvent>(OnMenuClick);
    }



    private void OnMenuClick(ClickEvent evt)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
