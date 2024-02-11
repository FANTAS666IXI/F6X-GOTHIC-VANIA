using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script que controla el comportamiento de la llave
public class KeyController : MonoBehaviour
{
    public AudioClip pickKeySound; // Sonido que reproduce la llave al recogerla
    private KeyIconController keyIcon; // Referencia al JumpPotionController

    // Obtener la referencia al componente
    private void Start()
    {
        keyIcon = FindAnyObjectByType<KeyIconController>();
    }

    // Al activarse si es con Player le otorga una llave
    // Activa el icono de llave en el HUD
    // La llave se destruye
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundController.Instance.PlaySound(pickKeySound);
            collision.gameObject.GetComponent<PlayerController>().PickKey();
            keyIcon.ShowKeyIcon();
            Destroy(gameObject);
        }
    }
}