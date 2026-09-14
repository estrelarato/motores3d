using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    // --- 1. Singleton Pattern ---
    public static GameManager Instance { get; private set; }

    // --- 2. Padrão State Simplificado (Enum) ---
    public enum GameState
    {
        Iniciando,
        MenuPrincipal,
        Gameplay
    }

    [Header("Estado Atual")]
    [SerializeField] private GameState currentState;
    public GameState CurrentState => currentState;

    private void Awake()
    {
        // Garantindo a instância única (Singleton) e sobrevivência entre cenas
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Inscreve no evento de carregamento de cena
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Define o estado inicial
        SetGameState(GameState.Iniciando);

        // Se iniciamos o jogo na cena _Boot, solicita a transição para a Splash_Scene
        if (SceneManager.GetActiveScene().name == "_Boot")
        {
            RequestSceneChange("Splash_Scene");
        }
    }

    private void OnDestroy()
    {
        // Boa prática: desinscrever de eventos ao destruir o objeto
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // --- 3. Gerenciamento de Cenas e Troca de Estados ---

    /// <summary>
    /// Altera o estado do jogo e exibe no Console.
    /// </summary>
    private void SetGameState(GameState newState)
    {
        currentState = newState;
        Debug.Log($"[GameManager] Estado alterado para: {currentState}");
    }

    /// <summary>
    /// Função pública para solicitar a troca de cena.
    /// Apenas o GameManager valida e autoriza o carregamento.
    /// </summary>
    public void RequestSceneChange(string sceneName)
    {
        // Regra de validação baseada no estado atual
        bool isAllowed = false;

        switch (currentState)
        {
            case GameState.Iniciando:
                // Do Boot/Splash só é permitido ir pro Menu Principal
                if (sceneName == "MenuPrincipal_Scene" || sceneName == "Splash_Scene")
                    isAllowed = true;
                break;

            case GameState.MenuPrincipal:
                // Do Menu só é permitido ir para a Gameplay
                if (sceneName == "GetStarted_Scene")
                    isAllowed = true;
                break;

            case GameState.Gameplay:
                // Da Gameplay pode voltar ao Menu
                if (sceneName == "MenuPrincipal_Scene")
                    isAllowed = true;
                break;
        }

        if (isAllowed)
        {
            Debug.Log($"[GameManager] Mudança de cena autorizada para: {sceneName}");
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning($"[GameManager] Mudança de cena NEGADA para '{sceneName}' no estado {currentState}.");
        }
    }

    /// <summary>
    /// Callback executado automaticamente após uma cena carregar para atualizar o estado.
    /// </summary>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "_Boot":
            case "Splash_Scene":
                SetGameState(GameState.Iniciando);
                break;

            case "MenuPrincipal_Scene":
                SetGameState(GameState.MenuPrincipal);
                break;

            case "GetStarted_Scene":
                SetGameState(GameState.Gameplay);
                AssignPlayerInput();

                // Carrega a cena GUI no modo Aditivo caso ela ainda não esteja carregada
                if (!SceneManager.GetSceneByName("GUI").isLoaded)
                {
                    Debug.Log("[GameManager] Carregando cena GUI de forma aditiva...");
                    SceneManager.LoadScene("GUI", LoadSceneMode.Additive);
                }
                break;
        }
    }

    // --- 4. Alocação de Input ---

    /// <summary>
    /// Aloca o input disponível ao PlayerInput do jogador em jogos Single Player.
    /// </summary>
    public void AssignPlayerInput()
    {
        PlayerInput playerInput = FindObjectOfType<PlayerInput>();

        if (playerInput != null)
        {
            // Ativa os controles para o jogador na cena de gameplay
            playerInput.ActivateInput();
            Debug.Log($"[GameManager] Input alocado com sucesso para o objeto: {playerInput.gameObject.name}");
        }
        else
        {
            Debug.LogWarning("[GameManager] Nenhum PlayerInput encontrado na cena atual.");
        }
    }

    /// <summary>
    /// Encerra a aplicação (botão de Sair).
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("[GameManager] Encerrando o jogo...");
        Application.Quit();
    }
}