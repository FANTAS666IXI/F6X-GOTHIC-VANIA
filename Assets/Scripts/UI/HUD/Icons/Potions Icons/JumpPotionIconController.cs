using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script que controla el comportamiento del icono de las pociones de salto en el HUD
public class JumpPotionIconController : MonoBehaviour
{
    public Sprite jumpPotion1; // Imagen de la pocion de salto Tier 1
    public Sprite jumpPotion2; // Imagen de la pocion de salto Tier 2
    public Sprite jumpPotion3; // Imagen de la pocion de salto Tier 3
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
        HideJumpPotionIcon();
    }

    // Activa el icono de la pocion en funcion de la Tier de la misma
    // Invoca la funcion para volver a desactivarlo al terminar el boosteo
    public void ShowJumpPotionIcon(int potionTier, float jumpBuffDuration)
    {
        switch (potionTier)
        {
            case 1:
                potionIconImg.sprite = jumpPotion1;
                break;
            case 2:
                potionIconImg.sprite = jumpPotion2;
                break;
            case 3:
                potionIconImg.sprite = jumpPotion3;
                break;
            default:
                break;
        }
        potionIconImg.enabled = true;
        potionIconBackgroundImg.enabled = true;
        Invoke("HideJumpPotionIcon", jumpBuffDuration);
    }

    // Desactiva el icono de la pocion
    public void HideJumpPotionIcon()
    {
        potionIconImg.enabled = false;
        potionIconBackgroundImg.enabled = false;
    }
}