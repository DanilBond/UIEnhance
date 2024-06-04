using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GiftManager : MonoBehaviour
{
    public static GiftManager instance;

    public TextMeshProUGUI coinsText;

    private int[] randomCoins = new[]
    {
        500,
        777,
        1000,
        1200,
        2000,
        4000,
        10000,
        15000,
    };
    
    private void Awake()
    {
        instance = this;
    }

    public void OpenGift()
    {
        UIManager.instance.OpenPanel("Gift");
        int randomCoin = randomCoins[Random.Range(0, randomCoins.Length)];
        coinsText.text = randomCoin.ToString();
        MoneyManager.instance.SetMoneyCount(randomCoin);
    }
}
