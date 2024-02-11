using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script que controla el comportamiento de las pociones de vida
public class HealthPotionController : MonoBehaviour
{
    public int potionTier; // Tier de la pocion
    public float amountHeal; // Cantidad de la curacion
    public AudioClip pickPotionSound; // Sonido que reproduce la pocion al ser usada
    private PlayerController player; // Referencia al PlayerController
    private HealthPotionIconController healthPotionIcon; // Referencia al HealthPotionIconController

    // Obtener las referencias a los componentes
    private void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        healthPotionIcon = FindAnyObjectByType<HealthPotionIconController>();
    }

    // Al activarse si es con Player lo curara en base a la potencia de la pocion, a no ser que el jugador este al maximo de salud
    // Activa el icono de pocion de curacion en el HUD en base a su Tier
    // La pocion se destruye
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (player.GetCurrentHealth() < player.maxHealth)
            {
                SoundController.Instance.PlaySound(pickPotionSound);
                healthPotionIcon.ShowHealthPotionIcon(potionTier);
                player.Heal(amountHeal);
                Destroy(gameObject);
            }
        }
    }
}