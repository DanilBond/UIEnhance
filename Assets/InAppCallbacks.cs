using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class InAppCallbacks : MonoBehaviour
{
    public GameObject loader;
    public TextMeshProUGUI Message;

    private void Start()
    {
        CheckPurchases();
    }

    private void CheckPurchases()
    {
        
        //premiumBtn.GetComponent<Button>().interactable = PlayerPrefs.GetInt("Premium", 0) == 0;
    }

    public void ShowURL(string u) => Application.OpenURL(u);

    public void OpenLoader()
    {
        Message.text = "Loading";
        loader.SetActive(true);
    }

    public void CloseLoaderFail()
    {
        Message.text = "Failed!";
        Invoke(nameof(CloseDelay), 1.5f);
    }
    public void CloseLoaderSuccess()
    {
        Message.text = "Success!";
        Invoke(nameof(CloseDelay), 1.5f);
    }

    void CloseDelay()
    {
        loader.SetActive(false);
    }
    
    public void Restore()
    {
        var apple = CodelessIAPStoreListener.Instance.GetStoreExtensions<IAppleExtensions>();
        apple.RestoreTransactions((result) => {
            Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
        });
    }

    public void OnPurchaseCompleted(Product product)
    {
        if (product.definition.id == "coins_10k")
        {
            MoneyManager.instance.SetMoneyCount(10000);
        }
        if (product.definition.id == "coins_25k")
        {
            MoneyManager.instance.SetMoneyCount(25000);
        }
        if (product.definition.id == "coins_50k")
        {
            MoneyManager.instance.SetMoneyCount(50000);
        }
        
        if (product.definition.id == "new_year_ball")
        {
            ShopManager.instance.Buy("NEW YEAR BALL");
        }
        if (product.definition.id == "star_ball")
        {
            ShopManager.instance.Buy("STAR BALL");
        }
        
        CheckPurchases();
    }
}