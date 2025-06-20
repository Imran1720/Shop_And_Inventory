using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUIManager : MonoBehaviour, ICoinManager
{
    [Header("Coin Data")]
    [SerializeField] private TextMeshProUGUI coinText;

    private int playerCoins = 0;
    public void DecrementCoins(int amount)
    {
        playerCoins -= amount;
        if (playerCoins <= 0)
        {
            playerCoins = 0;
        }
        SetCoinUI();
    }

    public void IncrementCoins(int amount)
    {
        playerCoins += amount;
        SetCoinUI();
    }

    public int GetTotalCoins() => playerCoins;

    public void SetCoinUI() => coinText.text = playerCoins.ToString();
}
