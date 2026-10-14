using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHeat : MonoBehaviour
{
    [Header("Sistema de Vidas")]
    public int maxLives = 3;
    private int currentLives;

    [Header("Invulnerabilidad y Empujón (Knockback)")]
    public float invulnerabilityDuration = 1f;
    private bool isInvulnerable = false;
    public float knockbackForceX = 7f;
    public float knockbackForceY = 4f;

    private Rigidbody2D rd;
    private Animator animator;
    private PlayerController playerController;

    void Start()
    {
        currentLives = maxLives;
        rd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>(); // Obtenemos referencia al controlador de movimiento
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        // Al colisionar con un enemigo y no estar en periodo de invulnerabilidad, recibe daño
        if (collision2D.gameObject.CompareTag("Enemy") && !isInvulnerable)
        {
            TakeDamage(collision2D.transform);
        }
    }

    void TakeDamage(Transform enemyTransform)
    {
        currentLives--;
        animator.SetTrigger("Hurt");

        // Calcula la dirección del empujón basándose en la posición del enemigo respecto al jugador
        float knockbackDir = (transform.position.x < enemyTransform.position.x) ? -1f : 1f;

        // Aplica la fuerza física de retroceso al Rigidbody2D
        rd.linearVelocity = new Vector2(knockbackForceX * knockbackDir, knockbackForceY);

        // Inicia la corrutina para gestionar la invulnerabilidad temporal
        StartCoroutine(InvulnerabilityRoutine());
    }

    IEnumerator InvulnerabilityRoutine()
    {
        isInvulnerable = true;

        // Desactivamos el movimiento en el PlayerController para que el empujón actúe libremente
        if (playerController != null)
        {
            playerController.canMove = false;
        }

        // Espera el tiempo configurado de invulnerabilidad (1 segundo)
        yield return new WaitForSeconds(invulnerabilityDuration);

        // Reactivamos el movimiento del jugador
        if (playerController != null)
        {
            playerController.canMove = true;
        }

        isInvulnerable = false;

        // Si las vidas llegan a cero, reinicia la escena actual
        if (currentLives <= 0)
        {
            RestartLevel();
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Método público que permite a otros scripts (como HealthUI) consultar las vidas restantes
    public int GetCurrentLives()
    {
        return currentLives;
    }
}