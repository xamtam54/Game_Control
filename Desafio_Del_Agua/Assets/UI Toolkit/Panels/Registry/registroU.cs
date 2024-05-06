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
        UIDocument uIDocument = GetComponent<UIDocument>();
        usernameField = uIDocument.rootVisualElement.Q<TextField>("UsernameField");
        passwordField = uIDocument.rootVisualElement.Q<TextField>("PasswordField");
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
        string url = this.baseAPIUrl;

        // JSON
        string jsonBody =   "{\"UserName\":\"" + userName + 
                            "\",\"Names\":\"" + "jugador" + 
                            "\",\"Surnames\":\"" + "ext" + 
                            "\",\"Password\":\"" + password + 
                            "\",\"Email\":\"" + "generico@gmail.com" + 
                            "\",\"User_Type_Id\":" + 1 + "}";

        // POST 
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // Enviar la solicitud
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error al enviar la solicitud: " + request.error);
            }
            else
            {
                Debug.Log("Usuario creado correctamente");
            }
        }
    }

}
