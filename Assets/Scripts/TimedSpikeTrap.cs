using UnityEngine;
using System.Collections;

public class TimedSpikeTrap : MonoBehaviour
{
    [Header("Ajustes del Tiempo")]
    public float waitTime = 2f; // Tiempo en reposo antes de activarse
    public float activeTime = 1f; // Tiempo que permanece activa/peligrosa

    [Header("Daño Causado")]
    public int damageAmount = 1; // Cantidad de vidas que se le quitan al personaje

    [Header("Estado Trampa")]
    public bool isDangerous = false;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(TrapRoutine());
    }

    // Se ejecuta al entrar a la trampa
    private void OnTriggerEnter2D(Collider2D other)
    {
        ProcesarDanio(other);
    }

    // CORREGIDO: Se cambió 'OnTriggerStary2D' por 'OnTriggerStay2D'
    private void OnTriggerStay2D(Collider2D other)
    {
        ProcesarDanio(other);
    }

    IEnumerator TrapRoutine()
    {
        while (true)
        {
            // Estado inactivo / seguro
            isDangerous = false;
            yield return new WaitForSeconds(waitTime);

            // Activar animacion de picos saliendo
            if (animator != null)
            {
                animator.SetTrigger("Active");
            }

            // Estado activo / peligroso
            isDangerous = true;
            yield return new WaitForSeconds(activeTime);
        }
    }

    void ProcesarDanio(Collider2D other)
    {
        // Si la trampa esta en su fase peligrosa
        if (isDangerous)
        {
            PlayerHeat playerHealth = other.GetComponent<PlayerHeat>();

            if (playerHealth != null)
            {
                // Se pasa 'transform' para indicar la posicion de la trampa
                playerHealth.TakeDamage(transform);
            }
        }
    }
}