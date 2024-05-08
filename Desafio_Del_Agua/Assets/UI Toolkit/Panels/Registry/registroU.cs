using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Networking;

public class registroU : MonoBehaviour
{
    private TextField usernameField;
    private TextField passwordField;

    void Start()
    {
        //UIDocument uIDocument = GetComponent<UIDocument>();
        //usernameField = uIDocument.rootVisualElement.Q<TextField>("UsernameField");
        //passwordField = uIDocument.rootVisualElement.Q<TextField>("PasswordField");

        StartCoroutine(CreateUser("UsuarioReal", "12345678"));
    }

    public void registrar()
    {
        string username = usernameField.value;
        string password = passwordField.value;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Debug.LogError("Por favor, completa todos los campos.");
            return;
        }
        StartCoroutine(CreateUser(username, password));
    }


    public string baseAPIUrl = "http://www.irrigationmanagementudec.somee.com/";



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
                //codigo aqui
            }
        }
    }
}
