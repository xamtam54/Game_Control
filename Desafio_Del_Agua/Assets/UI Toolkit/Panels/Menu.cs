using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MiCodigo : MonoBehaviour
{
    private UIDocument _document;
    private Button _button1;
    private Button _button2;
   // private Button _button3;
   //private Button _button4;
   //--------------------------------------------------
    public string Uno;
    public string Options;

    
   //------------------------------------------------
    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        _button1 = _document.rootVisualElement.Q<Button>("play") as Button;
        _button1.RegisterCallback<ClickEvent>(OnPlayGameClick);

        _button2 = _document.rootVisualElement.Q<Button>("options") as Button;
        _button2.RegisterCallback<ClickEvent>(OnOptionsClick);


    }
    private void OnDisable()
    {
        _button1.UnregisterCallback<ClickEvent>(OnPlayGameClick);
        _button2.UnregisterCallback<ClickEvent>(OnPlayGameClick);
        //_button3.UnregisterCallback<ClickEvent>(OnPlayGameClick);
        //_button4.UnregisterCallback<ClickEvent>(OnPlayGameClick);
    }
    private void OnPlayGameClick(ClickEvent evt)
    {
        Debug.Log("Presionaste el boton play");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Uno");
    }

    private void OnOptionsClick(ClickEvent evt)
    {
        Debug.Log("Presionaste el boton opciones");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Options");
    }
}
