using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Networking;



public class loginU : MonoBehaviour
{
    // Tomar datos de UI Toolkit
    private UIDocument uIDocument;
    public GameObject esteobjeto;
    public UIDocument otroDocument;
    private TextField usernameField; // para mostrar registro si el user la cago
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
            usernameField = uIDocument.rootVisualElement.Q<TextField>("Nombre_Usuario");
            passwordField = uIDocument.rootVisualElement.Q<TextField>("User_Pasword");


            // Mover el registro del callback del botón aquí
            _button1 = uIDocument.rootVisualElement.Q<Button>("Login") as Button;
            _button1.RegisterCallback<ClickEvent>(evt => Login());

            _button2 = uIDocument.rootVisualElement.Q<Button>("Registro") as Button;
            _button2.RegisterCallback<ClickEvent>(evt => IrRegistro());

        }
    }

    public void IrRegistro()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Registro"); //carga el menu principal del juego
    }

    public string baseAPIUrl = "http://www.irrigationmanagementudec.somee.com/";

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
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

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
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
                        StartCoroutine(GetGamesAndScores(userId));
                        IrAMenu(); //carga el menu principal del juego

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


    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void IrAMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu"); //carga el menu principal del juego
    }


    private IEnumerator GetGamesAndScores(int userId)
    {
        // Construir la URL para obtener las asignaciones de juegos y puntuaciones del usuario
        string url = baseAPIUrl.TrimEnd('/') + $"/api/Users/allocations/{userId}";
        Debug.Log("URL: " + url);

        // GET
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error al obtener los juegos y las puntuaciones del usuario: " + request.error);
            }
            else
            {
                if (request.responseCode == 200)
                {
                    Allocation[] allocations = JsonUtility.FromJson<AllocationArrayWrapper>("{\"items\":" + request.downloadHandler.text + "}").items;
                    Debug.Log("Juegos y puntuaciones del usuario obtenidos correctamente:");

                    foreach (var allocation in allocations)
                    {
                        Debug.Log("Allocation Id: " + allocation.allocation_Systems_Id + ", Game Id: " + allocation.game_Id + ", Score Id: " + allocation.score_Id);

                        yield return GetGame(allocation.game_Id);
                    }
                }
                else
                {
                    Debug.LogError("Error al obtener los juegos y las puntuaciones del usuario: " + request.downloadHandler.text);
                }
            }
        }
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public List<int> gameIds = new List<int>();
    public List<int> scoreId = new List<int>();

    private IEnumerator GetGame(int gameId)
    {
        string url = baseAPIUrl.TrimEnd('/') + $"/api/Games/{gameId}";
        Debug.Log("URL Game: " + url);

        // GET
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error al obtener los detalles del juego con ID " + gameId + ": " + request.error);
            }
            else
            {
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

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------
    private IEnumerator GetScore(int scoreId)
    {
        string url = baseAPIUrl.TrimEnd('/') + $"/api/Score/{scoreId}";
        Debug.Log("URL Score: " + url);

        // GET
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error al obtener los detalles de la puntuación con ID " + scoreId + ": " + request.error);
            }
            else
            {
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

    // Clase para deserializar-----------------------------------------------------------------------------------------------------------------------
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
    }
    /*
    public void ImprimirDatosGuardados()
    {
        for (int i = 0; i < 3; i++) 
        {
            string gameIdKey = "GameId_Escena" + (i + 1);
            if (PlayerPrefs.HasKey(gameIdKey))
            {
                int gameId = PlayerPrefs.GetInt(gameIdKey);
                Debug.Log("ID de juego guardado para la escena " + (i + 1) + ": " + gameId);
            }
            else
            {
                Debug.Log("No se encontraron datos guardados para la escena " + (i + 1));
            }
        }

        if (PlayerPrefs.HasKey("score_Id"))
        {
            int scoreId = PlayerPrefs.GetInt("score_Id");
            Debug.Log("ID de puntuación guardado: " + scoreId);
        }
        else
        {
            Debug.Log("No se encontraron datos guardados para la puntuación");
        }
    }
    */
}
