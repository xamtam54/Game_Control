using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class score1esc : MonoBehaviour
{
    public WIN winScript;
    public bool paso = false;
    public int gameId;

    void Start()
    {
        gameId = PlayerPrefs.GetInt("score_Id1", 0);
    }

    void Update()
    {
        if (winScript != null && winScript.won && !paso)
        {
            StartCoroutine(EnviarUpdate(gameId, 1));
        }
    }

    IEnumerator EnviarUpdate(int gameId, int achievementId)
    {
        // URL de la API y el ID del juego
        string url = "http://www.irrigationmanagementudec.somee.com/api/Score/" + gameId;

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
                PlayerPrefs.SetInt("achievementE1", achievementId);

            }
        }

        // Marcar como ya enviado
        paso = true;
    }
}
