using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rd;
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    public bool isGrouded;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        rd.linearVelocity = new Vector2(move * moveSpeed, rd.linearVelocity.y);

        if(Input.GetKeyDown(KeyCode.Space) && isGrouded){
            rd.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrouded = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision2D){
        if(collision2D.gameObject.CompareTag("Ground")){
            isGrouded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision2D){
        if(collision2D.gameObject.CompareTag("Ground")){
            isGrouded = false;
        }
    }
}
