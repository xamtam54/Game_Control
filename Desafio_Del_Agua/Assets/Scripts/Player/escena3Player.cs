using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class escena3Player : MonoBehaviour
{
    public Boss boss;
    public int gameId;
    public bool paso = false;
    public bool pasito = false;
    void Start()
    {
        gameId = PlayerPrefs.GetInt("GameId_Escena3", 0);
    }

    void Update()
    {
        if (boss.isAlive == false && !paso && !pasito)
        {
            StartCoroutine(EnviarUpdate(gameId, 3));
        } 
    }


    IEnumerator EnviarUpdate(int gameId, int achievementId)
    {
        // URL de la API y el ID del juego
        string url = "http://www.irrigationmanagementudec.somee.com/api/Games/" + gameId;

        // Agregar el parámetro achievementId a la URL
        url += "?achievementId=" + achievementId;

        // Crear la solicitud PUT
        using (UnityWebRequest request = UnityWebRequest.Put(url, ""))
        {
            request.downloadHandler = new DownloadHandlerBuffer();

            // Enviar la solicitud
            yield return request.SendWebRequest();

            // Verificar si ocurrió algún error
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error al enviar la solicitud: " + request.error);
            }
            else
            {
                Debug.Log("Juego actualizado correctamente");
                PlayerPrefs.SetInt("achievementE3", achievementId);
                paso = true;
            }
            pasito = true;
        }

        


    }
}
