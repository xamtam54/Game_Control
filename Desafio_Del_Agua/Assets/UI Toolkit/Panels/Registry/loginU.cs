using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Networking;



public class loginU : MonoBehaviour
{
    // Tomar datos de UI Toolkit
    private UIDocument uIDocument;
    private TextField usernameField;
    private TextField passwordField;
    private Button _button1;
    private string _username;
    private string _password;

    void Start()
    {
        uIDocument = GetComponent<UIDocument>(); // Asignar a la variable de clase
        if (uIDocument != null)
        {
            Debug.Log("El documento existe :D");
            usernameField = uIDocument.rootVisualElement.Q<TextField>("Nombre_Usuario");
            passwordField = uIDocument.rootVisualElement.Q<TextField>("User_Pasword");
            check();

            // Mover el registro del callback del botón aquí
            _button1 = uIDocument.rootVisualElement.Q<Button>("Login") as Button;
            _button1.RegisterCallback<ClickEvent>(evt => Login());
        }
    }
    void check() //se puede borrar
    {
        if (usernameField != null && passwordField != null)
        {
            Debug.Log("Ninguno de los 2 es null");
        }
    }

    public string baseAPIUrl = "http://www.irrigationmanagementudec.somee.com/";

    public void Login()
    {
        string username = usernameField.text;
        string password = passwordField.text;
        Debug.Log(username + " " + password); //comprobacion de los datos insertados

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Debug.LogError("completa todos los campos.");
            return;
        }

        StartCoroutine(AuthenticateUser(username, password));
    }

    public IEnumerator AuthenticateUser(string username, string password)
    {
        // Construir la URL
        string url = this.baseAPIUrl + "login?username=" + username + "&password=" + password;

        // Realizar la solicitud GET
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error al enviar la solicitud: " + request.error);
            }
            else
            {
                if (request.responseCode == 200) 
                {
                    Debug.Log("Usuario autenticado correctamente");
                    // aqui va el codigo
                    UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu"); //carga el menu principal del juego
                }
                else
                {
                    Debug.LogError("Error de autenticación: " + request.downloadHandler.text);
                }
            }
        }
    }
}
