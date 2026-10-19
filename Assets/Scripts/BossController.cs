using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public Transform player;

    public float maxHealth = 100f;
    private float currentHealth;

    public float speed = 2f;

    private bool furyMode = false;

    public Slider bossHealthBar;

    public GameObject panelVictoriaUI;

    private SpriteRenderer sr;

    [Header("Deteccion")]
    public float detectionDistance = 8f;

    [Header("Retroceso")]
    public float knockbackForce = 1.5f;

    private Rigidbody2D rb;

    private void Start()
    {
        currentHealth = maxHealth;

        bossHealthBar.maxValue = maxHealth;
        bossHealthBar.value = currentHealth;

        sr = GetComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();

        panelVictoriaUI.SetActive(false);
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // Solo persigue al jugador cuando esta cerca
        if (distance <= detectionDistance)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
            );
        }

        if (!furyMode && currentHealth <= maxHealth * 0.5f)
        {
            EnterFuryMode();
        }
    }

    public void TakeDamage(float damage, Transform attacker)
    {
        currentHealth -= damage;

        bossHealthBar.value = currentHealth;

        // Retroceso al recibir golpe
        if (rb != null)
        {
            float directionX = Mathf.Sign(transform.position.x - attacker.position.x);

            Vector2 direction = new Vector2(directionX, 0f);

            rb.linearVelocity = Vector2.zero;

            rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void EnterFuryMode()
    {
        furyMode = true;
        speed *= 1.8f;
        sr.color = Color.red;
    }

    private void Die()
    {
        panelVictoriaUI.SetActive(true);
        Time.timeScale = 0f;
        Destroy(gameObject);
    }
}