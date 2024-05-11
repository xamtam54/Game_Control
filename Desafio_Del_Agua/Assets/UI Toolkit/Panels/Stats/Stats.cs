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
    private Button button1;
    private Button button2;

    void Awake()
    {
        uiDocument = GetComponent<UIDocument>();

        button1 = uiDocument.rootVisualElement.Q<Button>("Menu") as Button;
        button1.RegisterCallback<ClickEvent>(goToMenu);

        button2 = uiDocument.rootVisualElement.Q<Button>("Progreso") as Button;
        button2.RegisterCallback<ClickEvent>(goToStats);

    }
    // Start is called before the first frame update
    void Start()
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
        if (PlayerPrefs.GetInt("achivementE1", 0) == 1)
        {
            visualElement1.style.display = DisplayStyle.Flex;
        }
        if (PlayerPrefs.GetInt("achivementE2", 0) == 2)
        {
            visualElement2.style.display = DisplayStyle.Flex;
        }
        if (PlayerPrefs.GetInt("achivementE3", 0) == 1)
        {
            visualElement3.style.display = DisplayStyle.Flex;
        }
    }

    void goToMenu(ClickEvent evt)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    void goToStats(ClickEvent evt)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Progreso");
    }
}
