using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Networking;


public class registroU : MonoBehaviour
{
    // tomar datos IUtoolkit 

    // enviar datos
    string test = "edison cañon";

    void Start()
    {
        Debug.Log("inicio");
        //StartCoroutine(EnviarTipoDeJugador(test));
        //StartCoroutine(EncontrarTiposDeJugador());
    }

    IEnumerator EnviarTipoDeJugador(string tipoDePlaga)
    {
        string url = "http://www.irrigationmanagementudec.somee.com/api/User_Types?User_Type_Name=" + test;

        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(url, ""))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError("Error al enviar" + request.error);
            }
            else
            {
                Debug.Log("Tipo enviado correctamente: " + test);
            }
        }
    }

    IEnumerator EncontrarTiposDeJugador()
    {
        string url = "http://www.irrigationmanagementudec.somee.com/api/User_Types" ;

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError("Error al enviar: " + request.error);
            }
            else
            {
                string responseText = request.downloadHandler.text;
                Debug.Log("Respuesta del servidor: " + responseText);
            }
        }
    }
}
