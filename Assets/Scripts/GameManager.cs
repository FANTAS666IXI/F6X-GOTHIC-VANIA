using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script que controla el estado de la partida
public class GameManager : MonoBehaviour
{
    public GameObject endGamePanel; // Panel que se muestra al terminar la partida
    private bool gameOver; // Bandera que indica si el juego ha terminado

    private void Start()
    {
        // Inicializar variable
        gameOver = false;
    }

    // Cambia el estado de la partida a terminada y activa el panel de partida terminada
    public void GameOver()
    {
        gameOver = true;
        endGamePanel.SetActive(true);
    }

    // Getter para saber el estado de la partida
    public bool GetGameOver()
    {
        return gameOver;
    }
}