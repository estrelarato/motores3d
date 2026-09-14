using System;

public static class PlayerObserverManager
{
    // Evento disparado quando a quantidade de moedas muda.
    // O int repassa o total de moedas atualizado.
    public static event Action<int> OnCoinCountChanged;

    /// <summary>
    /// Dispara o evento de mudança na contagem de moedas.
    /// </summary>
    public static void NotifyCoinCountChanged(int currentCoins)
    {
        OnCoinCountChanged?.Invoke(currentCoins);
    }
}