using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MiCodigo : MonoBehaviour
{
    private UIDocument _document;
    private Button _button1;
    private Button _button2;
    private Button _button3;
    private Button _button4;
    private Button _button5;
   //--------------------------------------------------
    public string Uno;
    public string Options;
    public string Tutorial;
    public string Credits;

    
   //------------------------------------------------
    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        _button1 = _document.rootVisualElement.Q<Button>("play") as Button;
        _button1.RegisterCallback<ClickEvent>(OnPlayGameClick);

        _button2 = _document.rootVisualElement.Q<Button>("options") as Button;
        _button2.RegisterCallback<ClickEvent>(OnOptionsClick);


        _button3 = _document.rootVisualElement.Q<Button>("tutorial") as Button;
        _button3.RegisterCallback<ClickEvent>(OnTutorialClick);

        _button4 = _document.rootVisualElement.Q<Button>("Credits") as Button;
        _button4.RegisterCallback<ClickEvent>(OnCreditsClick);

        _button5 = _document.rootVisualElement.Q<Button>("exit") as Button;
        _button5.RegisterCallback<ClickEvent>(Exit);

    }
    private void OnDisable()
    {
        _button1.UnregisterCallback<ClickEvent>(OnPlayGameClick);
        _button2.UnregisterCallback<ClickEvent>(OnOptionsClick);
        _button3.UnregisterCallback<ClickEvent>(OnTutorialClick);
        _button4.UnregisterCallback<ClickEvent>(OnCreditsClick);
        _button5.UnregisterCallback<ClickEvent>(Exit);
    }
    private void OnPlayGameClick(ClickEvent evt)
    {
        //Debug.Log("Presionaste el boton play");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Uno");
    }

    private void OnOptionsClick(ClickEvent evt)
    {
        //Debug.Log("Presionaste el boton opciones");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Options");
    }

    private void OnTutorialClick(ClickEvent evt)
    {
        //Debug.Log("Presionaste el boton tutorial");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial");
    }

    private void OnCreditsClick(ClickEvent evt)
    {
        //Debug.Log("Presionaste el boton Creditos");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
    }

    public void Exit(ClickEvent evt)
    {
        Application.Quit();
    }
}
