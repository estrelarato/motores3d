using UnityEngine;
using TMPro; // Use UnityEngine.UI caso use o Text padrão da Unity

public class CoinUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

    private void OnEnable()
    {
        // Inscreve no evento quando o objeto/cena é ativado
        PlayerObserverManager.OnCoinCountChanged += UpdateCoinDisplay;
    }

    private void OnDisable()
    {
        // Desinscreve do evento para evitar Memory Leaks
        PlayerObserverManager.OnCoinCountChanged -= UpdateCoinDisplay;
    }

    private void Start()
    {
        // Inicializa a UI com 0 moedas
        UpdateCoinDisplay(0);
    }

    private void UpdateCoinDisplay(int newCount)
    {
        if (coinText != null)
        {
            coinText.text = $"Moedas: {newCount}";
        }
    }
}