using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script que controla el comportamiento del jefe final
public class BossController : MonoBehaviour
{
    public float maxHealth; // Salud maxima
    public float damage; // Daño que hace al jugador
    public float speed; // Velocidad de movimiento
    public float waitTime; // Tiempo de espera entre embestidas
    public int killValue; // Puntuacion que otorga al ser derrotado
    public float enrageMoveSpeedMultiplier; // Multiplicador de velocidad de movimiento cuando esta enfurecido
    public float enrageDamageMultiplier; // Multiplicador de dano cuando esta enfurecido
    public Color enreagedColor; // Color que toma cuando esta enfurecido
    public AudioClip menaceSound; // Sonido que emite el jefe cuando se activa el combate
    public AudioClip attackSound; // Sonido que emite el jefe antes de embestir al jugador
    public AudioClip deathSound; // Sonido que emite el jefe cuando muere
    public Transform startPos; // Posicion inicial
    public Transform endPos; // Posicion final
    public ParticleSystem deathParticles; // Particulas que se emiten cuando muere
    public GameManager gameManager; // Referencia al GameManager
    private float currentHealth; // Salud actual
    private bool enraged; // Indica si esta enfurecido
    private bool isWaiting; // Indica si esta esperando antes de embestir de nuevo
    private Animator anim; // Referencia al Animator
    private Rigidbody2D rb; // Referencia al Rigidbody2D
    private SpriteRenderer spr; // Referencia al SpriteRenderer
    private PlayerController player; // Referencia al jugador
    private ScoreManager scoreManager; // Referencia al ScoreManager
    private Color currentColor; // Color actual
    private Vector3 startPosVec; // Posicion inicial en forma de Vector3
    private Vector3 endPosVec; // Posicion final en forma de Vector3
    private Vector3 nextPosVec; // Siguiente posicion a la que se movera

    // Obtener las referencias a los componentes
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Inicializar variables
    private void Start()
    {
        SoundController.Instance.PlaySound(menaceSound, 0.3f);
        player = FindAnyObjectByType<PlayerController>();
        scoreManager = FindAnyObjectByType<ScoreManager>();
        enraged = false;
        isWaiting = false;
        startPosVec = startPos.position;
        endPosVec = endPos.position;
        nextPosVec = endPosVec;
        currentHealth = maxHealth;
        currentColor = spr.color;
    }

    private void Update()
    {
        // Moverse hacia la siguiente posicion
        if (!isWaiting)
        {
            Vector3 direction = (nextPosVec - transform.position).normalized;
            rb.velocity = direction * speed;
        }

        // Comprobar si ha llegado a la siguiente posicion
        if (Vector3.Distance(transform.position, nextPosVec) <= 0.2f)
        {
            rb.velocity = Vector3.zero;
            anim.SetBool("isRunning", false);
            StartCoroutine(SetNextPosition());
        }
        else if (!isWaiting)
            anim.SetBool("isRunning", true);

        // Si aun no esta enfurecido, lo hara al llegar a la mitad de la vida maxima
        // Enfurecido: Cambia de color y aumenta sus estadisticas
        if (!enraged && currentHealth <= maxHealth / 2)
        {
            currentColor = enreagedColor;
            spr.color = currentColor;
            speed *= enrageMoveSpeedMultiplier;
            damage *= enrageDamageMultiplier;
            enraged = true;
        }
    }

    // Establece la proxima posicion del jefe
    // Reproduce el sonido de ataque mientras espera waitTime
    private IEnumerator SetNextPosition()
    {
        if (nextPosVec == endPosVec)
        {
            nextPosVec = startPosVec;
        }
        else
        {
            nextPosVec = endPosVec;
        }
        isWaiting = true;
        SoundController.Instance.PlaySound(attackSound, 0.2f);
        yield return new WaitForSeconds(waitTime);
        if (spr.flipX)
            spr.flipX = false;
        else
            spr.flipX = true;
        isWaiting = false;
    }

    // Al chocar comprueba si es con el Player y de serlo le causa dano
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.TakeDamage(damage);
        }
    }

    // Al activarse si es con Sword el jefe sufre dano
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sword"))
        {
            TakeDamage();
        }
    }

    // Inicia la Coroutine de parpadear y reduce la salud restante, en caso de caer a 0 morira
    public void TakeDamage()
    {
        StartCoroutine(FlashSprite());
        currentHealth--;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Suma su puntuacion de haber sido derrotado, reproduce el sonido de muerte
    // Instancia las particulas e indica al GameManager que la partida ha terminado
    // El jefe se destruye
    private void Die()
    {
        scoreManager.IncreaseScore(killValue);
        SoundController.Instance.PlaySound(deathSound, 0.4f);
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        gameManager.GameOver();
        Destroy(gameObject);
    }

    // El sprite parpadea en negro
    private IEnumerator FlashSprite()
    {
        spr.color = Color.black;
        yield return new WaitForSeconds(0.2f);
        spr.color = currentColor;
    }
}