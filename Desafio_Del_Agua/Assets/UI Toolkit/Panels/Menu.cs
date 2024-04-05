using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MiCodigo : MonoBehaviour
{
    private void Awake()
    {
        Visualelement root = Getcomponent<UIDocument>().rootVisualElement;
    }  
}
