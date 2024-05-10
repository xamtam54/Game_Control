using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class score2esc : MonoBehaviour
{
    public WIN winScript;
    public bool paso = false;
    public int scoreId;

    void Start()
    {
        scoreId = PlayerPrefs.GetInt("score_Id2", 0);
    }

    void Update()
    {
        if (winScript != null && winScript.won && !paso)
        {
            StartCoroutine(EnviarUpdate(scoreId, winScript.totalSobrevivientes));
        }
    }

    IEnumerator EnviarUpdate(int scoreId, decimal total)
    {
        string url = "http://www.irrigationmanagementudec.somee.com/api/Score/" + scoreId;

        url += "?total=" + total;

        using (UnityWebRequest request = UnityWebRequest.Put(url, ""))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error al enviar la solicitud: " + request.error);
            }
            else
            {
                Debug.Log("Juego actualizado correctamente");
                PlayerPrefs.SetString("scoreE2", total.ToString() + "%");

            }
        }
        paso = true;
    }
}
