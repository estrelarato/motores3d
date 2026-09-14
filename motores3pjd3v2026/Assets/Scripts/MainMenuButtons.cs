using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    // Função para o botão Iniciar Jogo
    public void StartGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RequestSceneChange("GetStarted_Scene");
        }
        else
        {
            Debug.LogError("GameManager não encontrado! Certifique-se de iniciar a partida pela cena _Boot.");
        }
    }

    // Função para o botão Sair
    public void QuitGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.QuitGame();
        }
    }
}