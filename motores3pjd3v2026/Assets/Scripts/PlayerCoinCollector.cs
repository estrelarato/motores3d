using UnityEngine;

public class PlayerCoinCollector : MonoBehaviour
{
    private int currentCoins = 0;

    public void AddCoins(int amount)
    {
        currentCoins += amount;

        // Notifica todos os inscritos pelo canal de eventos estático
        PlayerObserverManager.NotifyCoinCountChanged(currentCoins);
    }
}