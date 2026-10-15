using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Ajustes de Ataque")]
    public Transform attackPoint;
    public float attackRange = 0.6f;
    public LayerMask enemyLayer;
    public int attackDamage = 1;

    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(0)){
            Attack();
        }
    }

    void Attack(){
        if(animator != null){
            // Activamos la animacion mediante el Trigger
            animator.SetTrigger("Attack");
        }

        // Detecta todos los enemigos dentro del circulo de impacto (Hitbox)
        Collider2D[] hitEnemis = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach(Collider2D enemy in hitEnemis){
            // Busca el script EnemyHealth en el enemigo golpeado
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

            if(enemyHealth != null){
                // Le pasamos el dano y la posicion del jugador actual para calcular el empujon
                enemyHealth.TakeDamage(attackDamage, transform);
            }
        }
    }

    // Dibujamos el radio del ataque definido por AttackPoint
    private void OnDrawGizmosSelected(){
        if(attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
