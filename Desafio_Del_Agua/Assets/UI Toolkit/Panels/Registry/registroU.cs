using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Networking;

public class registroU : MonoBehaviour
{
    private UIDocument uIDocument;
    public UIDocument otroUIDocument; //para mostrar el de ingresar una vez el usuario se crea
    private TextField usernameField;
    private TextField passwordField;
    private Button _button1;
    private string _username;
    private string _password;

    void Awake()
    {
        // No es necesario hacer nada aquí.
    }

    void Start()
    {
        uIDocument = GetComponent<UIDocument>(); // Asignar a la variable de clase
        if (uIDocument != null)
        {
            Debug.Log("El documento existe :D");
            usernameField = uIDocument.rootVisualElement.Q<TextField>("UsernameField");
            passwordField = uIDocument.rootVisualElement.Q<TextField>("PasswordField");
            check();

            // Mover el registro del callback del botón aquí
            _button1 = uIDocument.rootVisualElement.Q<Button>("Registro") as Button;
            _button1.RegisterCallback<ClickEvent>(evt => registrar());
        }
    }
    void Update()
    {

    }
    void check()
    {
        if (usernameField != null && passwordField != null)
        {
            Debug.Log("Ninguno de los 2 es null");
        }
    }

    public void registrar()
    {
        Debug.Log("Entro al metodo registrar");
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
        string url = this.baseAPIUrl;

        // JSON
        string jsonBody = "{\"UserName\":\"" + userName +
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
                uIDocument.enabled = false;
                otroUIDocument.enabled = true;
            }
        }
    }
}
