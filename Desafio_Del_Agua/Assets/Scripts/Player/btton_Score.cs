using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class btton_Score : MonoBehaviour
{
    public string baseAPIUrl = "http://www.irrigationmanagementudec.somee.com/";

    private IEnumerator GetScores()
    {
        // Llamar a GetScore para cada scoreId
        yield return StartCoroutine(GetScore(PlayerPrefs.GetInt("score_Id1", 0), "scoreE1"));
        yield return StartCoroutine(GetScore(PlayerPrefs.GetInt("score_Id2", 0), "scoreE2"));
        yield return StartCoroutine(GetScore(PlayerPrefs.GetInt("score_Id3", 0), "scoreE3"));
    }

    private IEnumerator GetScore(int scoreId, string playerPrefsKey)
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
                    // Extraer el valor total de la respuesta
                    decimal total;
                    if (decimal.TryParse(request.downloadHandler.text, out total))
                    {
                        // Almacenar el valor total en PlayerPrefs
                        PlayerPrefs.SetString(playerPrefsKey, total.ToString() + "%");
                        Debug.Log("Valor total cargado para " + playerPrefsKey + ": " + total);
                    }
                    else
                    {
                        Debug.LogError("Error al convertir el valor total a decimal.");
                    }
                }
                else
                {
                    Debug.LogError("Error al obtener los detalles de la puntuación con ID " + scoreId + ": " + request.downloadHandler.text);
                }
            }
        }
    }
}
