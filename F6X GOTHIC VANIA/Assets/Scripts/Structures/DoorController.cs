using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script que controla el comportamiento de la puerta
public class DoorController : MonoBehaviour
{
    public GameObject boss; // Referencia al jefe que hay tras la puerta
    public AudioClip openSound; // Sonido que reproduce la puerta al abrirse
    public AudioClip lockedSound; // Sonido que reproduce la puerta si no se pudo abrir
    private Animator anim; // Referencia al Animator
    private Collider2D col; // Referencia al Collider2D
    private KeyIconController keyIcon; // Referencia

    // Obtener las referencias a los componentes
    private void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        keyIcon = FindAnyObjectByType<KeyIconController>();
    }

    // Al chocar comprueba si es con el Player y de serlo se abre si posee una llave
    // De lo contrario sonara que sigue cerrada
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerController>().GetHasKey())
                Unlock();
            else
                SoundController.Instance.PlaySound(openSound);
        }
    }

    // Funcion que abre la puerta sonando ejecutando la animacion e invocando a desactivar el colider
    private void Unlock()
    {
        SoundController.Instance.PlaySound(openSound);
        keyIcon.HideKeyIcon();
        Invoke("DisableCollider", 1f);
        anim.SetTrigger("open");
    }

    // Desactiva el collider
    private void DisableCollider()
    {
        col.enabled = false;
        boss.SetActive(true);
    }
}