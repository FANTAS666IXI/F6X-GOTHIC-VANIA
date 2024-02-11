using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script que controla el comportamiento de las gemas
public class GemController : MonoBehaviour
{
    public int gemTier; // Tier
    public AudioClip pickGemSound; // Sonido que emite la gema al ser recogida
    private ScoreManager scoreManager; // Referencia al ScoreManager


    // Obtener la referencia al componente
    private void Start()
    {
        scoreManager = FindAnyObjectByType<ScoreManager>();
    }


    // Al activarse si es con Player aumenta la puntuacion en base a la Tier de la gema
    // La gema se destruye
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundController.Instance.PlaySound(pickGemSound, 0.8f);
            scoreManager.IncreaseScore(gemTier);
            Destroy(gameObject);
        }
    }
}