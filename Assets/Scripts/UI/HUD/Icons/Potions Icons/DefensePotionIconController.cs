using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script que controla el comportamiento del icono de las pociones de defensa en el HUD
public class DefensePotionIconController : MonoBehaviour
{
    public Sprite defensePotion1; // Imagen de la pocion de defensa Tier 1
    public Sprite defensePotion2; // Imagen de la pocion de defensa Tier 2
    public Sprite defensePotion3; // Imagen de la pocion de defensa Tier 3
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
        HideDefensePotionIcon();
    }

    // Activa el icono de la pocion en funcion de la Tier de la misma
    // Invoca la funcion para volver a desactivarlo al terminar el boosteo
    public void ShowDefensePotionIcon(int potionTier, float defenseBuffDuration)
    {
        switch (potionTier)
        {
            case 1:
                potionIconImg.sprite = defensePotion1;
                break;
            case 2:
                potionIconImg.sprite = defensePotion2;
                break;
            case 3:
                potionIconImg.sprite = defensePotion3;
                break;
            default:
                break;
        }
        potionIconImg.enabled = true;
        potionIconBackgroundImg.enabled = true;
        Invoke("HideDefensePotionIcon", defenseBuffDuration);
    }

    // Desactiva el icono de la pocion
    public void HideDefensePotionIcon()
    {
        potionIconImg.enabled = false;
        potionIconBackgroundImg.enabled = false;
    }
}