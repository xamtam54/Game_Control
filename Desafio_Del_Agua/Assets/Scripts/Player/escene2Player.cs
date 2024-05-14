using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class escene2Player : MonoBehaviour
{
    public S_Targets sTargets;
    public WIN winScript;
    public int gameId;
    public bool paso = false;
    public bool pasito = false;
    void Start()
    {
        gameId = PlayerPrefs.GetInt("GameId_Escena2", 0);
    }

    void Update()
    {
        if (winScript != null && winScript.won && !paso && !pasito)
        {
            float livePercentage = CalculateLivePercentage();

            // Si el porcentaje de cultivos vivos es mayor al 60%, llamar a EnviarUpdate
            if (livePercentage > 60)
            {
                StartCoroutine(EnviarUpdate(gameId, 2));
            }
        } 
    }

    public float CalculateLivePercentage()
    {
        int totalPlants = 0;
        int alivePlants = 0;

        foreach (Plants plant in sTargets.ricePlants)
        {
            totalPlants++;
            if (plant.isAlive == 1)
            {
                alivePlants++;
            }
        }
        foreach (Plants plant in sTargets.sorgoPlants)
        {
            totalPlants++;
            if (plant.isAlive == 1)
            {
                alivePlants++;
            }
        }
        foreach (Plants plant in sTargets.sesamePlants)
        {
            totalPlants++;
            if (plant.isAlive == 1)
            {
                alivePlants++;
            }
        }

        if (totalPlants == 0)
        {
            return 0f;
        }
        else
        {
            //Debug.Log("porcentaje" + ((float)alivePlants / totalPlants) * 100);
            return ((float)alivePlants / totalPlants) * 100;
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
                PlayerPrefs.SetInt("achievementE2", achievementId);
                paso = true;
            }
            pasito = true;

        }




    }
}