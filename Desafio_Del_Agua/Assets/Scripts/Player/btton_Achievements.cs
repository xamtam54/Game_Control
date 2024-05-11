using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class btton_Achievements : MonoBehaviour
{
    public string baseAPIUrl = "http://www.irrigationmanagementudec.somee.com/";

    public IEnumerator GetAchivements()
    {
        // Llamar a GetScore para cada scoreId
        yield return StartCoroutine(GetScore(PlayerPrefs.GetInt("GameId_Escena1", 0), "achievementE1"));
        yield return StartCoroutine(GetScore(PlayerPrefs.GetInt("GameId_Escena2", 0), "achievementE2"));
        yield return StartCoroutine(GetScore(PlayerPrefs.GetInt("GameId_Escena3", 0), "achievementE2"));
    }

    private IEnumerator GetScore(int gameId, string playerPrefsKey)
    {
        string url = baseAPIUrl.TrimEnd('/') + $"/api/Games/{gameId}";
        Debug.Log("URL Score: " + url);

        // GET
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error al obtener los detalles de la puntuación con ID " + gameId + ": " + request.error);
            }
            else
            {
                if (request.responseCode == 200)
                {
                    // Extraer el valor total de la respuesta
                    int achievementId;
                    if (int.TryParse(request.downloadHandler.text, out achievementId))
                    {
                        // Almacenar el valor total en PlayerPrefs
                        PlayerPrefs.SetInt(playerPrefsKey, achievementId);
                        Debug.Log("Valor total cargado para " + playerPrefsKey + ": " + achievementId);
                    }
                    else
                    {
                        Debug.LogError("Error al convertir el valor total a int.");
                    }
                }
                else
                {
                    Debug.LogError("Error al obtener los detalles de la puntuación con ID " + gameId + ": " + request.downloadHandler.text);
                }
            }
        }
    }
}
