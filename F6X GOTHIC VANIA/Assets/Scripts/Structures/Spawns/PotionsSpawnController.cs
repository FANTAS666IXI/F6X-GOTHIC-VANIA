using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script que controla el comportamiento de un spawn de pociones
public class PotionsSpawnController : MonoBehaviour
{
    public GameObject potionGenerated; // Pocion a generar

    // Inicia la Coroutine de generacion de pociones
    private void Start()
    {
        StartCoroutine(SpawnPotions());
    }

    private IEnumerator SpawnPotions()
    {
        // Entra en bucle infinito
        while (true)
        {
            // Si no se detecta ningun hijo en este objeto se generara una nueva pocion
            if (transform.childCount == 0)
            {
                GameObject potionSpawned = Instantiate(potionGenerated, transform.position, Quaternion.identity);
                potionSpawned.transform.parent = transform;
            }
            yield return new WaitForSeconds(5);
        }
    }
}