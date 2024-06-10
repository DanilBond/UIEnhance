using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;
    
    [SerializeField]
    private int Money;

    private List<MoneyContainer> moneyContainers = new List<MoneyContainer>();

    private void Awake()
    {
        instance = this;
        
        moneyContainers = FindObjectsOfType<MoneyContainer>(true).ToList();

        if (!PlayerPrefs.HasKey("Money"))
        {
            SetMoneyCount(1000);
        }
        
        Money = PlayerPrefs.GetInt("Money", 0);
        
        UpdateContainers();
    }

    private void Start()
    {
        
    }

    public int GetMoneyCount()
    {
        Money = PlayerPrefs.GetInt("Money", 0);
        return Money;
    }
    public void SetMoneyCount(int value)
    {
        Money = PlayerPrefs.GetInt("Money", 0);
        Money += value;
        PlayerPrefs.SetInt("Money", Money);
        PlayerPrefs.SetInt("AllMoney", PlayerPrefs.GetInt("AllMoney") + value);
        UpdateContainers();
    }

    [ContextMenu("AddDevMoney")]
    public void AddDevMoney()
    {
        SetMoneyCount(50000);
    }

    public void UpdateContainers()
    {
        moneyContainers.ForEach(container => container.UpdateMoney());
    }
}
