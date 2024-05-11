using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Progreso : MonoBehaviour
{
    public UIDocument uiDocument;
    private Label p1;
    private Label p2;
    private Label p3;
    private Button button1;
    private Button button2;
    private string puntaje1;
    private string puntaje2;
    private string puntaje3;

    void Awake()
    {
        uiDocument = GetComponent<UIDocument>();

        button1 = uiDocument.rootVisualElement.Q<Button>("Menu") as Button;
        button1.RegisterCallback<ClickEvent>(goToMenu);

        button2 = uiDocument.rootVisualElement.Q<Button>("Logros") as Button;
        button2.RegisterCallback<ClickEvent>(goToLogros);

    }
    // Start is called before the first frame update
    void Start()
    {
        uiDocument = GetComponent<UIDocument>();

        p1 = uiDocument.rootVisualElement.Q<Label>("Puntaje1") as Label;
        p2 = uiDocument.rootVisualElement.Q<Label>("Puntaje2") as Label;
        p3 = uiDocument.rootVisualElement.Q<Label>("Puntaje3") as Label;

       
    }

    // Update is called once per frame
    void Update()
    {
        if(uiDocument!=null)
        {
           p1.text = PlayerPrefs.GetString("scoreE1", "");
           p2.text = PlayerPrefs.GetString("scoreE2", "");
           p3.text = PlayerPrefs.GetString("scoreE3", "");
        }
    }

    void goToMenu(ClickEvent evt)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    void goToLogros(ClickEvent evt)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Stats");
    }
}
