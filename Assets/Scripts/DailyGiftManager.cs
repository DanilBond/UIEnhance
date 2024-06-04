using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DailyGiftManager : MonoBehaviour
{
    public static DailyGiftManager instance;

    public UIPanel DailyGift;

    public TextMeshProUGUI coinsText;
    
    private const string LastCheckKey = "LastCheckDate";
    
    private int[] randomCoins = new[]
    {
        500,
        777,
        1000,
        1200,
        2000,
        5000,
    };

    private void Awake()
    {
        instance = this;
    }

    private IEnumerator Start()
    {
        if (PlayerPrefs.GetInt("TutorialPassed", 0) == 1)
        {
            yield return new WaitForSeconds(1);
            CheckDay();
        }
        else
        {
            yield return new WaitForSeconds(.2f);
            TutorialManager.insntance.OnTutorialPassed.AddListener(CheckDay);
        }
    }
    
    private void CheckDay()
    {
        string savedDate = PlayerPrefs.GetString(LastCheckKey, string.Empty);
        
        if (!string.IsNullOrEmpty(savedDate))
        {
            DateTime lastCheckDate = DateTime.Parse(savedDate);
            DateTime currentDate = DateTime.Now;

            if ((currentDate - lastCheckDate).Days >= 1)
            {
                GetGift();

                PlayerPrefs.SetString(LastCheckKey, currentDate.ToString());
            }
        }
        else
        {
            DateTime currentDate = DateTime.Now;
            PlayerPrefs.SetString(LastCheckKey, currentDate.ToString());
            
            GetGift();
        }
    }

    
    [ContextMenu("GetGift")]
    public void GetGift()
    {
        UIManager.instance.OpenPanel(DailyGift.panelName);
    }

    public void SelectGift()
    {
        int randomCoin = randomCoins[Random.Range(0, randomCoins.Length)];
        coinsText.text = randomCoin.ToString();
        MoneyManager.instance.SetMoneyCount(randomCoin);
    }
}
