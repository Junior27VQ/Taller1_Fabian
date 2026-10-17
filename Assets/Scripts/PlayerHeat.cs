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
        // Al colisionar con un enemigo y no estar en periodo de invulnerabilidad, recibe dano
        if (collision2D.gameObject.CompareTag("Enemy") && !isInvulnerable)
        {
            TakeDamage(collision2D.transform);
        }
    }

    public void TakeDamage(Transform enemyTransform)
    {
        if(isInvulnerable) return;
        
        currentLives--;
        animator.SetTrigger("Hurt");

        // Calcula la direccion del empujon basandose en la posicion del enemigo respecto al jugador
        float knockbackDir = (transform.position.x < enemyTransform.position.x) ? -1f : 1f;

        // Aplica la fuerza fisica de retroceso al Rigidbody2D
        rd.linearVelocity = new Vector2(knockbackForceX * knockbackDir, knockbackForceY);

        // Inicia la corrutina para gestionar la invulnerabilidad temporal
        StartCoroutine(InvulnerabilityRoutine());

        // Movimiento de camara
        if(CameraShake.Instance != null){
            CameraShake.Instance.Sacudir(0.15f, 02f);
        }
    }

    IEnumerator InvulnerabilityRoutine()
    {
        isInvulnerable = true;

        // Desactivamos el movimiento en el PlayerController para que el empujon actue libremente
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

    // Metodo publico que permite a otros scripts (como HealthUI) consultar las vidas restantes
    public int GetCurrentLives()
    {
        return currentLives;
    }
}