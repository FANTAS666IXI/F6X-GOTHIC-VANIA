using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script que controla los sonidos que se reproduzcan de forma ocasional
public class SoundController : MonoBehaviour
{
    public static SoundController Instance; // Vuelve el script visible desde cualquier otro
    private AudioSource audioSource; // Referencia al AudioSource

    // Si no existe ninguna instancia del SoundController la crea, en caso contrario no realizara nada mas
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
    }

    // Funcion que recibe una pista de audio y la reproduce
    public void PlaySound(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }

    // Sobrecarga de la funcion anterior que ademas puede recibir un float para modificar el volumen de la pista a reproducir
    public void PlaySound(AudioClip sound, float volume)
    {
        audioSource.PlayOneShot(sound, volume);
    }
}