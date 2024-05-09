using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WIN : MonoBehaviour
{
    public S_Targets sTargets;
    public bool lost = false; // perdio
    public bool won = false; // gano
    float agua_actual = 0;

   
           
    

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
           // Debug.LogError("El array Tower está vacío en el objeto S_Targets.");
        }

    }

    void Update()
    {
        

        if (Time.timeScale == 1f)
        {
            if (sTargets != null && !lost && !won)
            {
                Devices torre = sTargets.Tower[0];
                agua_actual = torre.Actual_Water;
                CheckWinCondition();
            }
            else
            {
                Debug.LogError("No se encontró ningún objeto de la clase S_Targets en la escena.");
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
            Time.timeScale = 0f;
            won = true;
            Debug.Log("El jugador ha ganado.");
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }
    }
}
