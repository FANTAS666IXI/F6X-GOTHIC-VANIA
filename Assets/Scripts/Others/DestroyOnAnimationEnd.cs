using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script que controla el comportamiento de un prefab con animacion de muerte de un enemigo
public class DestroyOnAnimationEnd : MonoBehaviour
{
    private Animator anim; // Referencia al Animator

    // Obtener la referencia al componente
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Comprueba que la animacion ha terminado y entonces se destruye el objeto
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            Destroy(gameObject);
    }
}