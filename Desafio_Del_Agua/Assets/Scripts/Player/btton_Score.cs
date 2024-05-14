using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class btton_Score : MonoBehaviour
{
    public string baseAPIUrl = "http://www.irrigationmanagementudec.somee.com/";

    public IEnumerator GetScores()
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
                    // Imprimir el texto de la respuesta para depuración
                    Debug.Log("Respuesta recibida: " + request.downloadHandler.text);

                    // Deserializar la respuesta JSON
                    ScoreResponse scoreResponse = JsonUtility.FromJson<ScoreResponse>(request.downloadHandler.text);

                    // Asignar valores predeterminados si son null
                    float totalValue = scoreResponse.total.HasValue ? scoreResponse.total.Value : 0f;

                    // Almacenar el valor total en PlayerPrefs
                    PlayerPrefs.SetString(playerPrefsKey, totalValue.ToString("0.##") + "%");
                    Debug.Log("Valor total cargado para " + playerPrefsKey + ": " + totalValue);
                }
                else
                {
                    Debug.LogError("Error al obtener los detalles de la puntuación con ID " + scoreId + ": " + request.downloadHandler.text);
                }
            }
        }
    }

    [System.Serializable]
    public class ScoreResponse
    {
        public int score_Id;
        public float? success_Rate;
        public float? water_Saved;
        public float? total;
        public bool isDeleted;
    }
}
