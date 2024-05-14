using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Networking;

public class loginU : MonoBehaviour
{
    private UIDocument uIDocument;
    public GameObject esteobjeto;
    public UIDocument otroDocument;
    private TextField usernameField;
    private TextField passwordField;
    private Button _button1;
    private Button _button2;
    private string baseAPIUrl = "http://www.irrigationmanagementudec.somee.com/";

    void Start()
    {
        uIDocument = GetComponent<UIDocument>();
        if (uIDocument != null)
        {
            usernameField = uIDocument.rootVisualElement.Q<TextField>("Nombre_Usuario");
            passwordField = uIDocument.rootVisualElement.Q<TextField>("User_Pasword");

            _button1 = uIDocument.rootVisualElement.Q<Button>("Login");
            _button1.RegisterCallback<ClickEvent>(evt => Login());

            _button2 = uIDocument.rootVisualElement.Q<Button>("Registro");
            _button2.RegisterCallback<ClickEvent>(evt => IrRegistro());
        }
    }

    public void IrRegistro()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Registro");
    }

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

    private IEnumerator AuthenticateUser(string username, string password)
    {
        string url = baseAPIUrl.TrimEnd('/') + "/api/Users?" +
                     "username=" + UnityWebRequest.EscapeURL(username) +
                     "&password=" + UnityWebRequest.EscapeURL(password);
        Debug.Log("URL: " + url);

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            Debug.Log("Request enviado");

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error al enviar la solicitud: " + request.error);
            }
            else
            {
                Debug.Log("Respuesta recibida");
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
                        StartCoroutine(GetGamesAndScores(userId));
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

    private IEnumerator GetGamesAndScores(int userId)
    {
        string url = baseAPIUrl.TrimEnd('/') + $"/api/Users/allocations/{userId}";
        Debug.Log("URL: " + url);

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            Debug.Log("Solicitud de juegos y puntuaciones creada");
            request.timeout = 10;
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            yield return request.SendWebRequest();
            Debug.Log("Solicitud de juegos y puntuaciones enviada");
            Debug.Log("Request Result: " + request.result);
            Debug.Log("Request Error: " + request.error);
            Debug.Log("Response Code: " + request.responseCode);
            Debug.Log("Response Text: " + request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error al obtener los juegos y las puntuaciones del usuario: " + request.error);
            }
            else
            {
                Debug.Log("Respuesta recibida para juegos y puntuaciones");
                if (request.responseCode == 200)
                {
                    Allocation[] allocations = JsonUtility.FromJson<AllocationArrayWrapper>("{\"items\":" + request.downloadHandler.text + "}").items;
                    Debug.Log("Juegos y puntuaciones del usuario obtenidos correctamente:");

                    foreach (var allocation in allocations)
                    {
                        Debug.Log("Allocation Id: " + allocation.allocation_Systems_Id + ", Game Id: " + allocation.game_Id + ", Score Id: " + allocation.score_Id);
                        yield return GetGame(allocation.game_Id);
                    }
                    // Llamar a IrAMenu después de que todas las operaciones hayan terminado
                    IrAMenu();
                }
                else
                {
                    Debug.LogError("Error al obtener los juegos y las puntuaciones del usuario: " + request.downloadHandler.text);
                }
            }
        }
    }

    private List<int> gameIds = new List<int>();
    private List<int> scoreId = new List<int>();

    private IEnumerator GetGame(int gameId)
    {
        string url = baseAPIUrl.TrimEnd('/') + $"/api/Games/{gameId}";
        Debug.Log("URL Game: " + url);

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            Debug.Log("Solicitud de detalles del juego creada");

            yield return request.SendWebRequest();
            Debug.Log("Solicitud de detalles del juego enviada");

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error al obtener los detalles del juego con ID " + gameId + ": " + request.error);
            }
            else
            {
                Debug.Log("Respuesta recibida para detalles del juego");
                if (request.responseCode == 200)
                {
                    string jsonResponse = request.downloadHandler.text;
                    GameDetails gameDetails = JsonUtility.FromJson<GameDetails>(jsonResponse);
                    Debug.Log("Detalles del juego con ID " + gameId + ": " + jsonResponse);

                    gameIds.Add(gameId);
                    scoreId.Add(gameDetails.score_Id);

                    for (int i = 0; i < gameIds.Count; i++)
                    {
                        PlayerPrefs.SetInt("GameId_Escena" + (i + 1), gameIds[i]);
                        Debug.Log("Guardado GameId: " + gameIds[i]);
                        PlayerPrefs.SetInt("score_Id" + (i + 1), scoreId[i]);
                    }

                    yield return GetScore(gameDetails.score_Id);
                }
                else
                {
                    Debug.LogError("Error al obtener los detalles del juego con ID " + gameId + ": " + request.downloadHandler.text);
                }
            }
        }
    }

    private IEnumerator GetScore(int scoreId)
    {
        string url = baseAPIUrl.TrimEnd('/') + $"/api/Score/{scoreId}";
        Debug.Log("URL Score: " + url);

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            Debug.Log("Solicitud de detalles de la puntuación creada");

            yield return request.SendWebRequest();
            Debug.Log("Solicitud de detalles de la puntuación enviada");

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error al obtener los detalles de la puntuación con ID " + scoreId + ": " + request.error);
            }
            else
            {
                Debug.Log("Respuesta recibida para detalles de la puntuación");
                if (request.responseCode == 200)
                {
                    Debug.Log("Detalles de la puntuación con ID " + scoreId + ": " + request.downloadHandler.text);
                }
                else
                {
                    Debug.LogError("Error al obtener los detalles de la puntuación con ID " + scoreId + ": " + request.downloadHandler.text);
                }
            }
        }
    }

    private void IrAMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

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

    [System.Serializable]
    public class Allocation
    {
        public int allocation_Systems_Id;
        public int game_Id;
        public int score_Id;
    }

    [System.Serializable]
    public class AllocationArrayWrapper
    {
        public Allocation[] items;
    }

    [System.Serializable]
    public class GameDetails
    {
        public int game_Id;
        public int score_Id;
        public string game_Name;
    }
}
