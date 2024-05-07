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
        string url = baseAPIUrl.TrimEnd('/') + "/api/Users?" +
                     "username=" + UnityWebRequest.EscapeURL(username) +
                     "&password=" + UnityWebRequest.EscapeURL(password);
        Debug.Log("URL: " + url);

        // GET
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
                    Users[] users = JsonUtility.FromJson<UsersArrayWrapper>("{\"items\":" + request.downloadHandler.text + "}").items;

                    bool authenticated = false;
                    int userId = -1;

                    foreach (var user in users)
                    {
                        if (user.userName == username && user.password == password)
                        {
                            authenticated = true;
                            userId = user.users_Id;
                            break;
                        }
                    }

                    if (authenticated)
                    {
                        Debug.Log("Usuario autenticado correctamente. ID del usuario: " + userId);
                        // cidigo
                        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu"); //carga el menu principal del juego
                    }
                    else
                    {
                        Debug.LogError("Error de autenticación: Nombre de usuario o contraseña incorrectos");
                    }
                }
                else
                {
                    Debug.LogError("Error de autenticación: " + request.downloadHandler.text);
                }
            }
        }
    }

    // Clase para deserializar la respuesta JSON
    [System.Serializable]
    public class Users
    {
        public int users_Id;
        public string userName;
        public string names;
        public string surnames;
        public string password;
        public string email;
        public string registration_Date;
        public int is_Active;
        public int user_Type_Id;
        public string user_Type;
        public bool isDeleted;
    }

    [System.Serializable]
    public class UsersArrayWrapper
    {
        public Users[] items;
    }
}
