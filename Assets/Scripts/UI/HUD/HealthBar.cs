using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script que controla el comportamiento de la barra de vida en el HUD
public class HealthBar : MonoBehaviour
{
    [SerializeField] GameObject health; // Referencia a la vida en la barra de vida

    // Recalcula la escala de la barra de vida en funcion a la vida actual que recibe
    public void SetHealth(float currentHealth)
    {
        health.transform.localScale = new Vector3(currentHealth, 1f);
    }
}