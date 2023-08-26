using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script que controla el comportamiento de las plataformas movibles
public class MovingPlatform : MonoBehaviour
{
    public float speed; // Velocidad de movimiento
    public Transform startPos; // Posicion inicial
    public Transform endPos; // Posicion final
    private Vector3 startPosVec, endPosVec, nextPosVec; // Vectores3 para las posiciones inicial, final y siguiente

    // Inicializar variables
    private void Start()
    {
        startPosVec = startPos.position;
        endPosVec = endPos.position;
        nextPosVec = startPosVec;
    }

    // Constantemente se desplaza hacia la siguiente posicion
    // Al llegar a la posicion final busca la siguiente posicion
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosVec, speed * Time.deltaTime);
        if ((transform.position == nextPosVec))
        {
            if (nextPosVec == startPosVec)
            {
                nextPosVec = endPosVec;
            }
            else
                nextPosVec = startPosVec;
        }
    }

    // Al chocar comprueba si es con el Player y de serlo lo establece como hijo de la plataforma
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = transform;
        }
    }

    // Al chocar comprueba si es con el Player y de serlo establece parent a null
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }
}