using UnityEngine;
using TMPro; // Libreria necesaria para usar TextMeshPro

public class CoinUI : MonoBehaviour
{
    [Header("Referencias de UI")]
    public TextMeshProUGUI coinText; // Arrastraras aqui tu texto del Canvas

    void Update()
    {
        // Actualiza el texto en pantalla mostrando cuantas monedas llevamos recolectadas
        if (coinText != null)
        {
            coinText.text = "Monedas: " + Coin.coinCount;
        }
    }
}