using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [Header("Referencias")]
    public PlayerHeat playerHealth; // Referencia al script de salud del jugador

    [Header("Arreglo de Corazones")]
    public GameObject[] hearts; // Contiene las 3 imágenes de los corazones en el Canvas

    void Update()
    {
        // Seguridad para evitar errores si la referencia no está asignada
        if (playerHealth == null) return;

        // Consultamos la vida actual del jugador en tiempo real
        int currentLives = playerHealth.GetCurrentLives();

        // Recorremos el arreglo para activar o desactivar la visualización de cada corazón
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentLives)
            {
                hearts[i].SetActive(true);  // Muestra el corazón si aún se dispone de esa vida
            }
            else
            {
                hearts[i].SetActive(false); // Oculta el corazón si esa vida ya se perdió
            }
        }
    }
}