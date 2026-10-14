using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [Header("Referencias")]
    public PlayerHeat playerHealth; // Referencia al script de salud del jugador

    [Header("Arreglo de Corazones")]
    public GameObject[] hearts; // Contiene las 3 imagenes de los corazones en el Canvas

    void Update()
    {
        // Seguridad para evitar errores si la referencia no esta asignada
        if (playerHealth == null) return;

        // Consultamos la vida actual del jugador en tiempo real
        int currentLives = playerHealth.GetCurrentLives();

        // Recorremos el arreglo para activar o desactivar la visualizacion de cada corazon
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentLives)
            {
                hearts[i].SetActive(true);  // Muestra el corazon si aun se dispone de esa vida
            }
            else
            {
                hearts[i].SetActive(false); // Oculta el corazon si esa vida ya se perdio
            }
        }
    }
}