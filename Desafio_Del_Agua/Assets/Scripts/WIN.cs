using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WIN : MonoBehaviour
{
    public S_Targets sTargets;
    public bool lost = false; // perdio
    public bool won = false; // gano
    public bool win = false; // para que los menus tengan en cuenta
    float agua_actual = 0;
    public int totalSobrevivientes = 0;


    public score1esc? score1; // hace referencia a los scripts de victoria
    public score2esc? score2;
    public score3esc? score3;

    public escena1Player? ach1; // referencia a los logros
    public escene2Player? ach2;
    public escena3Player? ach3;


    void Start()
    {
        sTargets = FindObjectOfType<S_Targets>();
        if (sTargets.Tower.Length > 0)
        {
            Devices torre = sTargets.Tower[0];
            agua_actual = torre.Actual_Water;
        }
        else
        {
           // Debug.LogError("El array Tower est� vac�o en el objeto S_Targets.");
        }

    }

    void Update()
    {
        

        if (Time.timeScale == 1f)
        {
            if (sTargets != null && !lost && !win)
            {
                Devices torre = sTargets.Tower[0];
                agua_actual = torre.Actual_Water;
                CheckWinCondition();
            }
            else
            {
                Debug.LogError("No se encontr� ning�n objeto de la clase S_Targets en la escena.");
            }
        }
    }

    void CheckWinCondition()
    {
        
        
        //Debug.Log("Nivel del agua dentor del metodo actual: " + agua_actual);

        int totalPlants = 0;
        int deadPlants = 0;
        
        foreach (Plants plant in sTargets.ricePlants)
        {
            totalPlants++;
            if (plant.isAlive == 2)
            {
                deadPlants++;
            }
        }
        foreach (Plants plant in sTargets.sorgoPlants)
        {
            totalPlants++;
            if (plant.isAlive == 2)
            {
                deadPlants++;
            }
        }
        foreach (Plants plant in sTargets.sesamePlants)
        {
            totalPlants++;
            if (plant.isAlive == 2) 
            {
                deadPlants++;
            }
        }

        float deathPercentage = (float)deadPlants / totalPlants * 100;

        totalSobrevivientes = (int)(100f - deathPercentage);

        if (deathPercentage > 40 || agua_actual <= 1)
        {
            lost = true;
            Time.timeScale = 0f;
            Debug.Log("El jugador ha perdido.");
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            return;
        }

        bool allProgressComplete = true;
        foreach (Plants plant in sTargets.ricePlants)
        {
            if (plant.isAlive == 1 && !plant.progressComplete)
            {
                allProgressComplete = false;
                break;
            }
        }
        foreach (Plants plant in sTargets.sorgoPlants)
        {
            if (plant.isAlive == 1 && !plant.progressComplete)
            {
                allProgressComplete = false;
                break;
            }
        }
        foreach (Plants plant in sTargets.sesamePlants)
        {
            if (plant.isAlive == 1 && !plant.progressComplete)
            {
                allProgressComplete = false;
                break;
            }
        }

        if (allProgressComplete)
        {
            won = true;

            //(se ejecuta cuando se cargen los datos)
            //Debug.Log("El jugador ha ganado.");
            if (score1 != null && ach1 != null)
            {
                //Debug.Log("esc 1");
                if (score1.paso)
                {   
                    Debug.Log("esc 1");
                    Time.timeScale = 0f;
                    UnityEngine.Cursor.lockState = CursorLockMode.None;
                    win = true;
                }
            }
            if (score2 != null && ach2 != null)
            {
                Debug.Log("esc 2");
                if (score2.paso)
                {
                    Debug.Log("esc 2");
                    Time.timeScale = 0f;
                    UnityEngine.Cursor.lockState = CursorLockMode.None;
                    win = true;
                }
            }
            if (score3 != null)
            {
                Debug.Log("esc 3");
                if (score3.paso && ach3.paso)
                {
                    Debug.Log("esc 3");
                    Time.timeScale = 0f;
                    UnityEngine.Cursor.lockState = CursorLockMode.None;
                    win = true;
                }
            }
            
        }
    }
}
