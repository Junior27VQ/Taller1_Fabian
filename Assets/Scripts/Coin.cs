using UnityEngine;

public class Coin : MonoBehaviour
{
    // Variable estatica para llevar la cuenta global de las monedas recolectadas
    public static int coinCount = 0;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Detecta si el objeto que colisiona es el jugador ("Player")
        if (collision.CompareTag("Player"))
        {
            coinCount++; // Aumenta en 1 el contador de monedas
            Destroy(gameObject); // Destruye la moneda actual de la escena
        }
    }
}