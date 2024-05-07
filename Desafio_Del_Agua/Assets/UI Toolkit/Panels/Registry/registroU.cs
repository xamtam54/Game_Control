using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Networking;

public class registroU : MonoBehaviour
{
    private UIDocument uIDocument;
    private TextField usernameField;
    private TextField passwordField;
    private Button _button1;
    private Button _button2;
    private string _username;
    private string _password;

    void Start()
    {
        uIDocument = GetComponent<UIDocument>(); // Asignar a la variable de clase
        if (uIDocument != null)
        {
           // Debug.Log("El documento existe :D");
            usernameField = uIDocument.rootVisualElement.Q<TextField>("UsernameField");
            passwordField = uIDocument.rootVisualElement.Q<TextField>("PasswordField");
            

            // Mover el registro del callback del botón aquí
            _button1 = uIDocument.rootVisualElement.Q<Button>("Registro") as Button;
            _button1.RegisterCallback<ClickEvent>(evt => registrar());

            _button2 = uIDocument.rootVisualElement.Q<Button>("Cuenta") as Button;
            _button2.RegisterCallback<ClickEvent>(evt => Inicio());
        }
    }

   
    public void Inicio()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Log in"); //carga el menu principal del juego
    }
    public void registrar()
    {
        //Debug.Log("Entro al metodo registrar");
        string username = usernameField.text;
        string password = passwordField.text;
        Debug.Log(username + " " + password); //comprobacion de los datos insertados
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Debug.LogError("Por favor, completa todos los campos.");
            return;
        }
        StartCoroutine(CreateUser(username, password));
    }


    public string baseAPIUrl = "http://irrigationmanagementudec.somee.com/";

    public IEnumerator CreateUser(string userName, string password)
    {
        // Construir la URL completa con los parámetros de consulta
        string url = baseAPIUrl.TrimEnd('/') + "/api/Users?" +
                     "username=" + UnityWebRequest.EscapeURL(userName) +
                     "&password=" + UnityWebRequest.EscapeURL(password) +
                     "&names=" + UnityWebRequest.EscapeURL("jugador") +
                     "&surnames=" + UnityWebRequest.EscapeURL("ext") +
                     "&email=" + UnityWebRequest.EscapeURL("generico@gmail.com") +
                     "&user_type_id=1";
        Debug.Log("URL: " + url);

        // Crear la solicitud POST
        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(url, ""))
        {
            // Enviar la solicitud
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error al enviar la solicitud: " + request.error);
            }
            else
            {

                Debug.Log("Usuario creado correctamente");
                Inicio();
            }
        }
    }
}
