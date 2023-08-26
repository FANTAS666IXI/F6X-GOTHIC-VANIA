using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script que controla el comportamiento del icono de las pociones de velocidad en el HUD
public class SpeedPotionIconController : MonoBehaviour
{
    public Sprite speedPotion1; // Imagen de la pocion de velocidad Tier 1
    public Sprite speedPotion2; // Imagen de la pocion de velocidad Tier 2
    public Sprite speedPotion3; // Imagen de la pocion de velocidad Tier 3
    public GameObject potionIcon; // Referencia al objeto que contiene la imagen del icono de la pocion
    public GameObject potionIconBackground; // Referencia al objeto que contiene el fondo del  icono de la pocion
    private Image potionIconImg; // Imagen del icono de la pocion
    private Image potionIconBackgroundImg; // Imagen del fondo del icono de la pocion

    // Obtener las referencias a los componentes
    // Desactiva el icono de la pocion
    private void Start()
    {
        potionIconImg = potionIcon.GetComponent<Image>();
        potionIconBackgroundImg = potionIconBackground.GetComponent<Image>();
        HideSpeedPotionIcon();
    }

    // Activa el icono de la pocion en funcion de la Tier de la misma
    // Invoca la funcion para volver a desactivarlo al terminar el boosteo
    public void ShowSpeedPotionIcon(int potionTier, float speedBuffDuration)
    {
        switch (potionTier)
        {
            case 1:
                potionIconImg.sprite = speedPotion1;
                break;
            case 2:
                potionIconImg.sprite = speedPotion2;
                break;
            case 3:
                potionIconImg.sprite = speedPotion3;
                break;
            default:
                break;
        }
        potionIconImg.enabled = true;
        potionIconBackgroundImg.enabled = true;
        Invoke("HideSpeedPotionIcon", speedBuffDuration);
    }

    // Desactiva el icono de la pocion
    public void HideSpeedPotionIcon()
    {
        potionIconImg.enabled = false;
        potionIconBackgroundImg.enabled = false;
    }
}