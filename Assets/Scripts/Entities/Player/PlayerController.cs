using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script que controla las acciones del jugador
public class PlayerController : MonoBehaviour
{
    public float maxHealth; // Salud maxima
    public float speed; // Velocidad de movimiento
    public float defense; // Defensa
    public float jump; // Fuerza de salto
    public AudioClip attackSound; // Sonido que emite el jugador al atacar
    public AudioClip hurtSound; // Sonido que emite el jugador cuando recibe daño
    public GameManager gameManager; // Referencia al GameManager
    private float movementX; // Movimiento sobre el eje X
    private float currentHealth; // Salud actual
    private float speedMultiplier; // Multiplicador de velocidad
    private float defenseMultiplier; // Multiplicador de defensa
    private float jumpMultiplier; // Multiplicador de fuerza de salto
    private bool canMove; // Indica si se puede mover
    private bool canAttack; // Indica si puede atacar
    private bool hasKey; // Indica si posee una llave
    private Rigidbody2D rb; // Referencia al Rigidbody 2D
    private SpriteRenderer spr; // Referencia al spriteRenderer
    private Animator anim; // Referencia al Animator
    private Transform swordTrs; // Referencia a la espada
    private GameObject view; // Referencia a la camara y comlementos
    private Color ogColor; // Color original
    [SerializeField] private HealthBar healthBar; // Referencia a la barra de vida en el HUD

    private void Start()
    {
        // Obtener las referencias a los componentes
        // Inicializar variables
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        view = GameObject.Find("View");
        swordTrs = transform.Find("Sword");
        ogColor = spr.color;
        currentHealth = maxHealth;
        speedMultiplier = 1f;
        defenseMultiplier = 1f;
        jumpMultiplier = 1f;
        hasKey = false;
        canMove = true;
        canAttack = true;
    }

    private void Update()
    {
        // Si la partida aun no ha terminado comprobar el comportamiento del jugador
        if (!gameManager.GetGameOver())
        {
            // Medinate GetAxis una funcion propia de Unity podemos registrar facilmente la direccion a la que trata de ir el jugador
            movementX = Input.GetAxis("Horizontal");
            Rotate();
            Run();
            Jump();
            Attack();
            Crouch();
        }
    }

    // Usamos la variable movementX para girar el sprite del jugador en la direccion oportuna
    private void Rotate()
    {
        if (movementX > 0)
        {
            spr.flipX = false;
            swordTrs.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (movementX < 0)
        {
            spr.flipX = true;
            swordTrs.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    // Aqui le damos impulso al jugador para que se desplaze horizontalmente
    private void Run()
    {
        if (movementX == 0 && !canMove)
            anim.SetBool("isRunning", false);
        else
            anim.SetBool("isRunning", true);
        Vector2 movement = new Vector2(movementX * speed * speedMultiplier, rb.velocity.y);
        if (anim.GetBool("isCrouch"))
            movement.x = 0;
        rb.velocity = movement;
    }

    // Comprobamos mediante el script CheckGround que el jugador esta sobre el suelo y pulsa espacio para hacer saltar al jugador
    private void Jump()
    {
        if (CheckGround.isGrounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)))
        {
            rb.AddForce(Vector2.up * jump * jumpMultiplier, ForceMode2D.Impulse);
        }
    }

    // Comprobamos que el jugador esta en el suelo y puede atacar para hacerlo
    private void Attack()
    {
        if (CheckGround.isGrounded && Input.GetKeyDown(KeyCode.J) && canAttack)
        {
            StartCoroutine(AttackCoroutine());
            anim.SetTrigger("attack");
            SoundController.Instance.PlaySound(attackSound, 0.1f);
        }
    }

    // Limitamos que el jugador vuelva a atacar antes de acabar la animacion
    private IEnumerator AttackCoroutine()
    {
        canAttack = false;
        anim.SetTrigger("attack");
        SoundController.Instance.PlaySound(attackSound, 0.1f);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        canAttack = true;
    }

    // El jugador puede agacharse con LeftControl, momento en el que se reduce su hitBox y no se podra mover
    private void Crouch()
    {
        if (CheckGround.isGrounded && Input.GetKey(KeyCode.LeftControl))
        {
            canMove = true;
            anim.SetBool("isCrouch", true);
        }
        else
        {
            canMove = false;
            anim.SetBool("isCrouch", false);
        }
    }

    // El jugador puede recibir curacion de una cantidad determinada
    public void Heal(float healing)
    {
        currentHealth += healing;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth / maxHealth);
    }

    // El jugador puede recibir dano de una cantidad determinada y de ser demasiado morira
    public void TakeDamage(float damage)
    {
        StartCoroutine(FlashSprite());
        SoundController.Instance.PlaySound(hurtSound, 0.4f);
        damage = Mathf.Round(damage / (defense * defenseMultiplier));
        anim.SetTrigger("hurt");
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        healthBar.SetHealth(currentHealth / maxHealth);
    }

    // Setter del multiplicador de velocidad
    public void SetSpeedMultiplier(float speedBuff)
    {
        speedMultiplier = speedBuff;
    }

    // Setter del multiplicador de defensa
    public void SetDefenseMultiplier(float defenseBuff)
    {
        defenseMultiplier = defenseBuff;
    }

    // Setter del multiplicador de fuerza de salto
    public void SetJumpMultiplier(float jumpBuff)
    {
        jumpMultiplier = jumpBuff;
    }

    // El jugador consigue una llave
    public void PickKey()
    {
        hasKey = true;
    }

    // El jugador muere
    private void Die()
    {
        view.transform.parent = null;
        gameManager.GameOver();
        Destroy(gameObject);
    }

    // El sprite parpadea en rojo
    private IEnumerator FlashSprite()
    {
        spr.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spr.color = ogColor;
    }

    // Getter de la salud actual
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    // Getter de si tiene llave
    public bool GetHasKey()
    {
        return hasKey;
    }
}