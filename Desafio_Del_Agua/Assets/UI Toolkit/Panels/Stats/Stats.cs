using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Stats : MonoBehaviour
{
    public UIDocument uiDocument;
    private VisualElement visualElement1;
    private VisualElement visualElement2;
    private VisualElement visualElement3;


    // Start is called before the first frame update
    void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        visualElement1 = uiDocument.rootVisualElement.Q<VisualElement>("Logro1");
        visualElement2 = uiDocument.rootVisualElement.Q<VisualElement>("Logro2");
        visualElement3 = uiDocument.rootVisualElement.Q<VisualElement>("Logro3");

        

        visualElement1.style.display = DisplayStyle.None;
        visualElement2.style.display = DisplayStyle.None;
        visualElement3.style.display = DisplayStyle.None;

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPref.GetInt("achivementE1", 0) = 1)
        {
            visualElement1.style.display = DisplayStyle.Flex;
        }
        if (PlayerPref.GetInt("achivementE2", 0) = 2)
        {
            visualElement2.style.display = DisplayStyle.Flex;
        }
        if (PlayerPref.GetInt("achivementE1", 0) = 1)
        {
            visualElement3.style.display = DisplayStyle.Flex;
        }
    }
}
