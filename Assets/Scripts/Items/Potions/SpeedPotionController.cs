using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script que controla el comportamiento de las pociones de velocidad
public class SpeedPotionController : MonoBehaviour
{
    public int potionTier; // Tier de la pocion
    public float speedBuff; // Calidad del boosteo
    public float speedBuffDuration; // Duracion del boosteo
    public AudioClip pickPotionSound; // Sonido que reproduce la pocion al ser usada
    private SpriteRenderer spr; // Referencia al SpriteRenderer
    private Collider2D col; // Referencia al Collider2D
    private PlayerController player; // Referencia al PlayerController
    private SpeedPotionIconController speedPotionIcon; // Referencia al JumpPotionIconController

    // Obtener las referencias a los componentes
    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        player = FindAnyObjectByType<PlayerController>();
        speedPotionIcon = FindAnyObjectByType<SpeedPotionIconController>();
    }

    // Al activarse si es con Player inicia la Coroutine de boosteo de velocidad
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundController.Instance.PlaySound(pickPotionSound);
            StartCoroutine(ActivateSpeedPotion());
        }
    }

    // Desactiva sprite y collider para no percibirse mas la pocion en escena
    // Activa el boosteo de velocidad al jugador en base a su calidad
    // Activa el icono de pocion de velocidad en el HUD en base a su Tier
    // La pocion se destruye
    IEnumerator ActivateSpeedPotion()
    {
        spr.enabled = false;
        col.enabled = false;
        player.SetSpeedMultiplier(speedBuff);
        speedPotionIcon.ShowSpeedPotionIcon(potionTier, speedBuffDuration);
        yield return new WaitForSeconds(speedBuffDuration);
        player.SetSpeedMultiplier(1.0f);
        Destroy(gameObject);
    }
}