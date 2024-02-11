using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script que controla el comportamiento de los enemigos comunes
public class EnemyController : MonoBehaviour
{
    public bool ghost; // Indica si este enemigo es de la clase fantasma
    public int maxHealth; // Salud maxima
    public int killValue; // Puntuacion que otorga al ser derrotado
    public float damage; // Dano que hace el enemigo al jugador
    public float speed; // Velocidad de movimiento
    public AudioClip ghostSound; // Sonido que emiten los enemigos fantasmas
    public AudioClip deathSound; // Sonido que emiten al morir
    public GameObject deathEffect; // Prefab que se generara al morir
    private int currentHealth; // Salud actual
    private float correctionDeathPos; // Pequeno ajuste en las distancias para generar correctamente el efecto de muerte 
    private bool player; // Indica si el jugador sigue presente en la escena
    private Rigidbody2D rb; // Referencia al Rigidbody2D
    private SpriteRenderer spr; // Referencia al SpriteRenderer
    private Animator anim; // Referencia al Animator
    private ScoreManager scoreManager;// Referencia al ScoreManager
    private Color ogColor; // Color original
    private Vector3 playerPos; // Posicion actual del jugador

    // Obtener las referencias a los componentes
    // Inicializar variables
    // Iniciar Coroutina para los sonidos de los fantasmas
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scoreManager = FindAnyObjectByType<ScoreManager>();
        currentHealth = maxHealth;
        ogColor = spr.color;
        correctionDeathPos = 0.15f;
        StartCoroutine(GhostSounds());
    }

    private void FixedUpdate()
    {
        // Detectar si el jugador sigue presente en la escena
        player = GameObject.Find("Player");
        if (player)
        {
            // Si no es un fantasma dirijirse directamente hacia el jugador
            if (!ghost)
                anim.SetBool("idle", false);
            playerPos = GameObject.FindGameObjectWithTag("Player").transform.position; transform.position = Vector3.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
            // Si el enemigo es un fantasma perseguir al jugador ademas en el eje Y
            if (ghost)
            {
                float verticalDistance = playerPos.y - transform.position.y;
                float verticalMovement = Mathf.Sign(verticalDistance) * speed * Time.deltaTime;
                transform.Translate(new Vector3(0, verticalMovement, 0));
            }
            // Rotar los sprites en la direccion del jugador
            if (transform.position.x < playerPos.x)
            {
                spr.flipX = true;
                if (ghost)
                    spr.flipX = false;
            }
            else if (transform.position.x > playerPos.x)
            {
                spr.flipX = false;
                if (ghost)
                    spr.flipX = true;
            }
        }
        else
        {
            // Cuando ya no exista el jugador si el enemigo es de tipo esqueleto pasar al estado Idle
            if (!ghost)
                anim.SetBool("idle", true);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    // Al activarse si es con Sword el enemigo sufre dano
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sword"))
        {
            takeDamage();
        }
    }

    // Inicia la Coroutine de parpadear y reduce la salud restante, en caso de caer a 0 morira
    private void takeDamage()
    {
        StartCoroutine(FlashSprite());
        currentHealth--;
        if (currentHealth <= 0)
            Die();
    }

    // Reduce el numero actual de enemigos atados al spawn de enemigos
    // Suma su puntuacion de haber sido derrotado, reproduce el sonido de muerte
    // Instancia el efecto de muerte y el enemigo se destruye
    // El enemigo se destruye
    private void Die()
    {
        transform.parent.GetComponent<EnemiesSpawnController>().ReduceCurrentEnemies();
        scoreManager.IncreaseScore(killValue);
        SoundController.Instance.PlaySound(deathSound);
        Vector3 deathPosition = transform.position;
        deathPosition.y -= correctionDeathPos;
        Instantiate(deathEffect, deathPosition, Quaternion.identity);
        Destroy(gameObject);
    }


    // Al chocar comprueba si es con el Player y de serlo le causa dano
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.TakeDamage(damage);
        }
    }

    // Este bucle permanece siempre activo reproduciendo en intervalos aleatorios el sonido de fantasma
    IEnumerator GhostSounds()
    {
        if (ghost)
        {
            while (true)
            {
                float delay = Random.Range(4f, 8f);
                yield return new WaitForSeconds(delay);
                SoundController.Instance.PlaySound(ghostSound, 0.4f);
            }
        }
    }

    // El sprite parpadea en rojo
    private IEnumerator FlashSprite()
    {
        spr.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spr.color = ogColor;
    }
}