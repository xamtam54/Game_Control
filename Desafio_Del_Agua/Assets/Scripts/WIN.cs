using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WIN : MonoBehaviour
{
    public S_Targets sTargets;
    private bool lost = false; // perdio
    public bool won = false; // gano

    void Start() 
    {
        sTargets = FindObjectOfType<S_Targets>();
        
    }

    void Update()
    {
        if (sTargets != null && !lost && !won)
        {
            CheckWinCondition();
        }
        else
        {
            Debug.LogError("No se encontró ningún objeto de la clase S_Targets en la escena.");
        }
    }

    void CheckWinCondition()
    {
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

        if (deathPercentage > 40)
        {
            lost = true;
            Debug.Log("El jugador ha perdido.");
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
            Debug.Log("El jugador ha ganado.");
        }
    }
}
