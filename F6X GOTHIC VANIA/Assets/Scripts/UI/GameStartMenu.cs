using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script que controla el comportamiento de los botones en la primera pantalla de la aplicacion
public class GameStartMenu : MonoBehaviour
{
    // Funcion que se usara para OnClick del boton Start
    // Carga la escena para comenzar a jugar
    public void StartGame()
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