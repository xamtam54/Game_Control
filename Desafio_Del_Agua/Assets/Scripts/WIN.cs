using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WIN : MonoBehaviour
{
    public S_Targets sTargets;
    private bool lost = false; // Perdió
    public bool won = false; // Ganó
    public int totalSobrevivientes = 0; // Total de plantas que sobrevivieron

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

        // Contar el total de plantas y las plantas muertas
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

        // Calcular el porcentaje de plantas muertas
        float deathPercentage = (float)deadPlants / totalPlants * 100;

        // Calcular el total de plantas que sobrevivieron
        totalSobrevivientes = 100 - deathPercentage;

        // Si el porcentaje de plantas muertas es mayor que 40%, el jugador pierde
        if (deathPercentage > 40)
        {
            lost = true;
            Debug.Log("El jugador ha perdido.");
            return;
        }

        // Si todas las plantas han completado su progreso, el jugador gana
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