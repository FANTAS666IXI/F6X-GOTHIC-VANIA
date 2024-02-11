using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script que controla la puntuacion como valor y texto en el HUD
public class ScoreManager : MonoBehaviour
{
    private int score; // Puntuacion actual
    public Text scoreTextHUD; // Referencia al texto en el HUD

    // Inicializa la variable
    private void Start()
    {
        score = 0;
    }

    //  Actualiza constantemente la puntuacion en el HUD
    public void Update()
    {
        scoreTextHUD.text = "score: " + score;
    }

    // Aumenta la puntuacion en una cantidad determinada
    public void IncreaseScore(int scoreGained)
    {
        score += scoreGained;
    }

    // Getter de la puntuacion actual
    public int GetScore()
    {
        return score;
    }
}