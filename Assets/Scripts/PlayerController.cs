using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rd;
    private Animator animator; //Control de animaciones
    private bool facingRight = true;

    [Header("Configuracion de Movimiento")]
    public float moveSpeed = 5f; // velocidad de movimiento
    public float jumpForce = 7f; //fuerza de salto
    public bool isGrounded;  //vericar si esta en el suelo

    [Header("Efectos visuales")]
    public GameObject polvoSaltoPrefabs;
    
    // Bandera publica que permite bloquear el movimiento temporalmente desde el script de salud (durante el empujon)
    [HideInInspector] public bool canMove = true;

    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. Leemos el input horizontal una sola vez aqui de forma limpia
        float move = Input.GetAxis("Horizontal");

        // 2. Gestionamos el giro del sprite segun la direccion del input
        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }

        // 3. Si canMove es falso (por recibir dano), detenemos la actualizacion de velocidad de movimiento
        if (!canMove) return;

        // 4. Actualizamos el parametro de velocidad en el Animator
        animator.SetFloat("Speed", Mathf.Abs(move));

        // 5. Aplicamos la velocidad horizontal manteniendo la fisica vertical actual
        rd.linearVelocity = new Vector2(move * moveSpeed, rd.linearVelocity.y);

        // 6. Sistema de salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rd.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            //animator.SetBool("isJump", true);

            if(polvoSaltoPrefabs != null){
                Instantiate(polvoSaltoPrefabs, transform.position, Quaternion.identity);
            }

            if(AudioManager.Instance != null){
                AudioManager.Instance.ReproducirSfx(AudioManager.Instance.sonidoSalto);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        // Detecta cuando el jugador toca el suelo
        if (collision2D.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isGrounded", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision2D)
    {
        // Detecta cuando el jugador deja el suelo
        if (collision2D.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // Metodo para voltear horizontalmente al personaje invirtiendo su escala X
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}