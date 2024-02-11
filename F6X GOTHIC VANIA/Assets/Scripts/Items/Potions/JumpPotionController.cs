using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script que controla el comportamiento de las pociones de salto
public class JumpPotionController : MonoBehaviour
{
    public int potionTier; // Tier de la pocion
    public float jumpBuff; // Calidad del boosteo
    public float jumpBuffDuration; // Duracion del boosteo
    public AudioClip pickPotionSound; // Sonido que reproduce la pocion al ser usada
    private SpriteRenderer spr; // Referencia al SpriteRenderer
    private Collider2D col; // Referencia al Collider2D
    private PlayerController player; // Referencia al PlayerController
    private JumpPotionIconController jumpPotionIcon; // Referencia al JumpPotionIconController

    // Obtener las referencias a los componentes
    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        player = FindAnyObjectByType<PlayerController>();
        jumpPotionIcon = FindAnyObjectByType<JumpPotionIconController>();
    }

    // Al activarse si es con Player inicia la Coroutine de boosteo de salto
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundController.Instance.PlaySound(pickPotionSound);
            StartCoroutine(ActivateJumpPotion());
        }
    }

    // Desactiva sprite y collider para no percibirse mas la pocion en escena
    // Activa el boosteo de salto al jugador en base a su calidad
    // Activa el icono de pocion de salto en el HUD en base a su Tier
    // La pocion se destruye
    IEnumerator ActivateJumpPotion()
    {
        spr.enabled = false;
        col.enabled = false;
        player.SetJumpMultiplier(jumpBuff);
        jumpPotionIcon.ShowJumpPotionIcon(potionTier, jumpBuffDuration);
        yield return new WaitForSeconds(jumpBuffDuration);
        player.SetJumpMultiplier(1.0f);
        Destroy(gameObject);
    }
}