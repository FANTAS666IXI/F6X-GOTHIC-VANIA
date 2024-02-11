using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Script que controla el comportamiento del panel de fin de partida
public class EndGamePanelController : MonoBehaviour
{
    public Text currentScoreText; // Referencia al Text de la puntuacion de la partida actual
    public Text recordScoreText; // Referencia al Text del record de puntuacion
    public ScoreManager scoreManager; // Referencia al ScoreManager

    // Al activarse el GameObject cargara los textos a mostrar
    private void OnEnable()
    {
        LoadTexts();
    }

    // Carga los textos
    // En el primero muestra la puntuacion de la partida actual
    // En el segundo si se ha superado el record lo dira, de lo contrario mostrara cual es el record
    private void LoadTexts()
    {
        currentScoreText.text = ("score: " + scoreManager.GetScore());
        if (CompareScores())
        {
            recordScoreText.text = ("new record !!");
        }
        else
            recordScoreText.text = ("record: " + PlayerPrefs.GetInt("RecordScore"));
    }

    // Compara la puntuacion de la partida actual con el record y devuelve un bool en funcion al resultado
    private bool CompareScores()
    {
        if (scoreManager.GetScore() > PlayerPrefs.GetInt("RecordScore"))
        {
            PlayerPrefs.SetInt("RecordScore", scoreManager.GetScore());
            return true;
        }
        else
            return false;
    }

    // Funcion que se usara para OnClick del boton Restart
    // Recarga la escena para volver a jugar
    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    // Funcion que se usara para OnClick del boton Quit
    // Cierra la aplicacion
    public void QuitGame()
    {
        Application.Quit();
    }
}