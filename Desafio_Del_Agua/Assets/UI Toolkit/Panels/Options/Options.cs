using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class Options : MonoBehaviour
{
    private UIDocument _document;
    private Button _button1;
//-----------------------------------
    public string MainMenu;

//------------------------------------
    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        _button1 = _document.rootVisualElement.Q<Button>("Menu") as Button;
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
