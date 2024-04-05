using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MiCodigo : MonoBehaviour
{
    private UIDocument _document;
    private Button _button;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        _button = _document.rootVisualElement.Q<Button>("play") as Button;
        _button.RegisterCallback<ClickEvent>(OnPlayGameClick);
    }
    private void OnDisable()
    {
        _button.UnregisterCallback<ClickEvent>(OnPlayGameClick);
    }
    private void OnPlayGameClick(ClickEvent evt)
    {
        Debug.Log("Presionaste el boton");
    }
}
