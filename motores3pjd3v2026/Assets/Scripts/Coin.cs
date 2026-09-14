using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;
    [SerializeField] private float rotationSpeed = 100f;

    private void Update()
    {
        // Efeito visual de rotação simples
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que colidiu é o jogador
        if (other.CompareTag("Player"))
        {
            // Tenta obter o componente do jogador e adcionar a moeda
            PlayerCoinCollector collector = other.GetComponent<PlayerCoinCollector>();
            if (collector != null)
            {
                collector.AddCoins(coinValue);
                Destroy(gameObject); // Destrói a moeda coletada
            }
        }
    }
}