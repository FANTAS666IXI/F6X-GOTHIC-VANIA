using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script que controla el comportamiento de un spawn de enemigos
public class EnemiesSpawnController : MonoBehaviour
{
    public int maxEnemies; // Maximo de enemigos que puede tener a la vez
    public float minSpawnDelay; // Tiempo minimo entre generar un enemigo y el siguiente
    public float maxSpawnDelay; // Tiempo maximo entre generar un enemigo y el siguiente
    public float leftSpawnOffset; // Rango maximo hacia la izq en el que se puede generar un enemigo
    public float rightSpawnOffset; // Rango maximo hacia la der en el que se puede generar un enemigo
    public float probEnemy2Spawn; // Probabilidad de que se genere el segundo tipo de enemigo en lugar del principal
    public GameObject enemyGenerated1; // Enemigo principal a generar
    public GameObject enemyGenerated2; // Enemigo secundario a generar
    private int currentEnemies; // Enemigos vivos enlazados a este spawn
    private GameObject newEnemy; // Proximo enemigo a generar

    // Inicia la Coroutine de generacion de enemigos
    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        // Entra en bucle infinito
        while (true)
        {
            // Si los enemigos vivos del spawn son menos que el maximo
            // Generara un nuevo tiempo de espera al azar dentro de los limites
            // Generara una nueva posicion de aparicion dentro de los limites
            // Da la oportunidad al azar de generar el tipo secundario de enemigo
            // Sino generara el tipo principal de enemigo
            if (currentEnemies < maxEnemies)
            {
                float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
                yield return new WaitForSeconds(delay);
                float randomXOffset = Random.Range(leftSpawnOffset, rightSpawnOffset);
                Vector3 spawnPos = transform.position + new Vector3(randomXOffset, 0f, 0f);
                if (Random.value > probEnemy2Spawn)
                    newEnemy = Instantiate(enemyGenerated1, spawnPos, Quaternion.identity);
                else
                    newEnemy = Instantiate(enemyGenerated2, spawnPos, Quaternion.identity);
                newEnemy.transform.parent = transform;
                currentEnemies++;
            }
            // De lo contrario no hace nada
            else
            {
                yield return null;
            }
        }
    }

    // Funcion que llaman los enemigos de este spaen al ser derrotados
    // Permite generar nuevos enemigos al volver a estar por debajo del maximo
    public void ReduceCurrentEnemies()
    {
        currentEnemies--;
    }
}