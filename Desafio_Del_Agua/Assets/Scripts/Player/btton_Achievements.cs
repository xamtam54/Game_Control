using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class btton_Achievements : MonoBehaviour
{
    public string baseAPIUrl = "http://www.irrigationmanagementudec.somee.com/";

    public IEnumerator GetAchivements()
    {
        // Llamar a GetScore para cada gameId
        yield return StartCoroutine(GetScore(PlayerPrefs.GetInt("GameId_Escena1", 0), "achievementE1"));
        yield return StartCoroutine(GetScore(PlayerPrefs.GetInt("GameId_Escena2", 0), "achievementE2"));
        yield return StartCoroutine(GetScore(PlayerPrefs.GetInt("GameId_Escena3", 0), "achievementE3"));
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
                Debug.LogError("Error al obtener los detalles del juego con ID " + gameId + ": " + request.error);
            }
            else
            {
                if (request.responseCode == 200)
                {
                    // Imprimir el texto de la respuesta para depuración
                    Debug.Log("Respuesta recibida: " + request.downloadHandler.text);

                    // Deserializar la respuesta JSON
                    AchievementResponse achievementResponse = JsonUtility.FromJson<AchievementResponse>(request.downloadHandler.text);

                    // Verificar si achievementId es null y asignar 0 en ese caso
                    int achievementId = achievementResponse.achievementId.HasValue ? achievementResponse.achievementId.Value : 0;

                    // Almacenar el valor en PlayerPrefs
                    PlayerPrefs.SetInt(playerPrefsKey, achievementId);
                    Debug.Log("Valor total cargado para " + playerPrefsKey + ": " + achievementId);
                }
                else
                {
                    Debug.LogError("Error al obtener los detalles del juego con ID " + gameId + ": " + request.downloadHandler.text);
                }
            }
        }
    }

    [System.Serializable]
    public class AchievementResponse
    {
        public int? gameId;
        public int? achievementId;
    }

}