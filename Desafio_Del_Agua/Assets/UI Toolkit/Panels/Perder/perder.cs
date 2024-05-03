using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class perder : MonoBehaviour
{
    public UIDocument uIDocument;
    public GameObject torre_principal;
    private Button _button1;
    private Button _button2;
    private Devices devices;

    public void Awake()
    {
        Time.timeScale = 1f;
        uIDocument = GetComponent<UIDocument>();
        devices = torre_principal.GetComponent<Devices>();
    }

    public void Update()
    {
        if (devices != null && devices.Actual_Water == 0)
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
