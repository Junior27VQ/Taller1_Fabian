using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Salud del Enemigo")]
    public int MaxHealth = 3; // Salud maxima del enemigo
    public int currentHealth; // Salud actual del enemigo

    [Header("Empujon (Knockback)")]
    public float knockbackForceX = 4f;
    public float knockbackForceY = 2f;
    private Rigidbody2D enemyRb;
    private bool isKnockedBack = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = MaxHealth;
        enemyRb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damageAmout, Transform attackTransform){
        currentHealth -= damageAmout;

        // Aplicamos empujon 
        if(enemyRb != null){
            float knockbackDir = (transform.position.x < attackTransform.position.x) ? -1f : 1f;
            enemyRb.linearVelocity = new Vector2(knockbackDir, knockbackForceY);
        }
        if(currentHealth <= 0){
            Die();
        }
    }

    public void Die(){
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
