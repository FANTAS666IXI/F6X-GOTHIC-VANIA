using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script que controla el comportamiento del icono de la llave en el HUD
public class KeyIconController : MonoBehaviour
{
    public Sprite key; // Imagen de la llave
    public GameObject keyIcon; // Referencia al objeto que contiene la imagen del icono de la llave
    public GameObject keyIconBackground; // Referencia al objeto que contiene el fondo del  icono de la llave
    private Image keyIconImg; // Imagen del icono de la llave
    private Image keyIconBackgroundImg; // Imagen del fondo del icono de la llave

    // Obtener las referencias a los componentes
    // Desactiva el icono de la llave
    private void Start()
    {
        keyIconImg = keyIcon.GetComponent<Image>();
        keyIconBackgroundImg = keyIconBackground.GetComponent<Image>();
        HideKeyIcon();
    }

    // Activa el icono de la llave
    public void ShowKeyIcon()
    {
        keyIconImg.enabled = true;
        keyIconBackgroundImg.enabled = true;
    }

    // Desactiva el icono de la llave
    public void HideKeyIcon()
    {
        keyIconImg.enabled = false;
        keyIconBackgroundImg.enabled = false;
    }
}