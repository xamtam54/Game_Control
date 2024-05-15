using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

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

                    // Preprocesar el JSON para reemplazar "null" por "0.0"
                    string jsonResponse = PreprocessJson(request.downloadHandler.text);
                    Debug.Log("Preprocessed JSON Response: " + jsonResponse);

                    // Deserializar el JSON
                    ScoreResponse scoreResponse = JsonUtility.FromJson<ScoreResponse>(jsonResponse);

                    float totalValue = scoreResponse.total;

                    // Guardar el valor total en PlayerPrefs
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

    private string PreprocessJson(string json)
    {
        // Reemplaza "null" por "0.0" en el JSON
        return json.Replace(":null", ":0.0");
    }

    [System.Serializable]
    public class ScoreResponse
    {
        public int score_Id;
        public float success_Rate;
        public float water_Saved;
        public float total;
        public bool isDeleted;
    }
}