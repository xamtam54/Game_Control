using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest_Control : MonoBehaviour
{
    public GameObject plantPrefab;
    public GameObject plantPrefab2;
    public Transform[] spawnPoints;
    public string plantTag = "Weaver";

    public float triggerRadius = 1f;  // Radio de activaci�n
    public LayerMask bossLayer;        // Capa que incluye al jefe (aseg�rate de configurar las capas correctamente en Unity)

    private static int nest = 1;
    public int nest2;

    public float chanceToTurnFire = 0.4f;


    void Start()
    {
        nest2 = nest + nest2;
        nest += 1;
    }
    
    void Update()
    {

        // Comprueba si el jugador est� dentro del radio de activaci�n
        Collider[] colliders = Physics.OverlapSphere(transform.position, triggerRadius, bossLayer);

        if (colliders.Length > 0 && nest2 != nest)
        {
            float randomValue = Random.value;
            Activate(randomValue);
            nest = nest2;
        }
    }

    void Activate(float randomValue)
    {
        



        foreach (Transform spawnPoint in spawnPoints)
        {
            
            
                
                
                if (randomValue < chanceToTurnFire)
                    {
                    GameObject weaverInstance = Instantiate(plantPrefab2, spawnPoint.position, spawnPoint.rotation);
                    weaverInstance.transform.position = spawnPoint.position;
                    weaverInstance.transform.rotation = spawnPoint.rotation;

                    NewBehaviourScript ricePlant = weaverInstance.GetComponent<NewBehaviourScript>();
                    
                    
                    ricePlant.gameObject.tag = plantTag;
                }
                else
                {
                    GameObject weaverInstance = Instantiate(plantPrefab, spawnPoint.position, spawnPoint.rotation);
                    weaverInstance.transform.position = spawnPoint.position;
                    weaverInstance.transform.rotation = spawnPoint.rotation;

                    NewBehaviourScript ricePlant = weaverInstance.GetComponent<NewBehaviourScript>();
                    

                    ricePlant.gameObject.tag = plantTag;
                }
            
            

            
        }

        

        
    }

    // Dibuja el radio de activaci�n en el editor para referencia visual
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, triggerRadius);
    }
}
