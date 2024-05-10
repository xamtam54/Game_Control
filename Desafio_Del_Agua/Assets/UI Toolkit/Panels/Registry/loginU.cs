using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Networking;


public class loginU : MonoBehaviour
{
    // Tomar datos de UI Toolkit
    private TextField usernameField;
    private TextField passwordField;

    void Start() 
    {
        // UIDocument uIDocument = GetComponent<UIDocument>();
        // usernameField = uIDocument.rootVisualElement.Q<TextField>("UsernameField");
        // passwordField = uIDocument.rootVisualElement.Q<TextField>("PasswordField");
    }

    public string baseAPIUrl = "http://www.irrigationmanagementudec.somee.com/";

    public void Login()
    {
        string username = usernameField.value;
        string password = passwordField.value;

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
                }
                else
                {
                    Debug.LogError("Error de autenticación: " + request.downloadHandler.text);
                }
            }
        }
    }
}
