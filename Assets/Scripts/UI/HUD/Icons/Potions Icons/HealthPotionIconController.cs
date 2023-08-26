using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script que controla el comportamiento de las pociones de curacion en el HUD
public class HealthPotionIconController : MonoBehaviour
{
    public Sprite healtPotion1; // Imagen de la pocion de curacion Tier 1
    public Sprite healtPotion2; // Imagen de la pocion de curacion Tier 2
    public Sprite healtPotion3; // Imagen de la pocion de curacion Tier 3
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
        HideHealthPotionIcon();
    }

    // Activa el icono de la pocion en funcion de la Tier de la misma
    // Invoca la funcion para volver a desactivarlo tras 3"
    public void ShowHealthPotionIcon(int potionTier)
    {
        switch (potionTier)
        {
            case 1:
                potionIconImg.sprite = healtPotion1;
                break;
            case 2:
                potionIconImg.sprite = healtPotion2;
                break;
            case 3:
                potionIconImg.sprite = healtPotion3;
                break;
            default:
                break;
        }
        potionIconImg.enabled = true;
        potionIconBackgroundImg.enabled = true;
        Invoke("HideHealthPotionIcon", 3f);
    }

    // Desactiva el icono de la pocion
    private void HideHealthPotionIcon()
    {
        potionIconImg.enabled = false;
        potionIconBackgroundImg.enabled = false;
    }
}