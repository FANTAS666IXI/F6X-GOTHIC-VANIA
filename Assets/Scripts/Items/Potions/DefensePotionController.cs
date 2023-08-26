using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script que controla el comportamiento de las pociones de defensa
public class DefensePotionController : MonoBehaviour
{
    public int potionTier; // Tier de la pocion
    public float defenseBuff; // Calidad del boosteo
    public float defenseBuffDuration; // Duracion del boosteo
    public AudioClip pickPotionSound; // Sonido que reproduce la pocion al ser usada
    private SpriteRenderer spr; // Referencia al SpriteRenderer
    private Collider2D col; // Referencia al Collider2D
    private PlayerController player; // Referencia al PlayerController
    private DefensePotionIconController defensePotionIcon; // Referencia al DefensePotionIconController

    // Obtener las referencias a los componentes
    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        player = FindAnyObjectByType<PlayerController>();
        defensePotionIcon = FindAnyObjectByType<DefensePotionIconController>();
    }


    // Al activarse si es con Player inicia la Coroutine de boosteo de defensa
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundController.Instance.PlaySound(pickPotionSound);
            StartCoroutine(ActivateDefensePotion());
        }
    }

    // Desactiva sprite y collider para no percibirse mas la pocion en escena
    // Activa el boosteo de defensa al jugador en base a su calidad
    // Activa el icono de pocion de defensa en el HUD en base a su Tier
    // La pocion se destruye
    IEnumerator ActivateDefensePotion()
    {
        spr.enabled = false;
        col.enabled = false;
        player.SetDefenseMultiplier(defenseBuff);
        defensePotionIcon.ShowDefensePotionIcon(potionTier, defenseBuffDuration);
        yield return new WaitForSeconds(defenseBuffDuration);
        player.SetDefenseMultiplier(1.0f);
        Destroy(gameObject);
    }
}