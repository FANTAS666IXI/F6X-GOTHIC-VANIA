using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script que comprueba que si el jugador esta en el suelo
public class CheckGround : MonoBehaviour
{
    public static bool isGrounded; // Indica si el jugador esta en el suelo
    private Animator animPlayer; // Referencia al Animator del jugador

    // Obtiene las referencias al Animator del jugador
    private void Start()
    {
        animPlayer = transform.parent.GetComponent<Animator>();
    }

    // Al entrar en contacto con un collider con el tag Ground se vuelve verdadero isGrounded
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = true;
            animPlayer.SetBool("isJumping", false);
        }
    }

    // Al dejar de estar en contacto con el collider con el tag Ground se vuelve falso isGrounded
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = false;
            animPlayer.SetBool("isJumping", true);
        }
    }
}