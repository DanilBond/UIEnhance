using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    
    public ShopItemData[] shopItems;

    public ShopItem shopItemPrefab;
    public Transform itemsParent;

    public UIPanel onBuyPanel;
    public Image onBuyImage;
    public TextMeshProUGUI onBuyName;

    private List<ShopItem> _shopItems = new List<ShopItem>();

    public static string SELECTED_BALL = "SelectedBall";
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Init();
        BuyDefault();
    }

    private void Init()
    {
        foreach (ShopItemData data in shopItems)
        {
            ShopItem item = Instantiate(shopItemPrefab, itemsParent);
            
            item.Init(data);
            
            _shopItems.Add(item);
        }
        
        UpdateItems();
    }

    void BuyDefault()
    {
        foreach (ShopItemData shopItem in shopItems)
        {
            if (shopItem.cost == 0)
            {
                Buy(shopItem.name);
            }
        }
    }

    public void Select(string id)
    {
        ShopItemData item = null;

        foreach (ShopItemData shopItem in shopItems)
        {
            if (shopItem.name == id)
            {
                item = shopItem;
                break;
            }
        }
        
        if(item == null)
            return;
        
        if (!item.canSelect)
            return;

        
        PlayerPrefs.SetString(SELECTED_BALL, item.name);
        
        UpdateItems();
    }

    public void Buy(string id)
    {
        ShopItemData item = null;

        foreach (ShopItemData shopItem in shopItems)
        {
            if (shopItem.name == id)
            {
                item = shopItem;
                break;
            }
        }
        
        if(item == null)
            return;

        if (MoneyManager.instance.GetMoneyCount() < item.cost)
            return;

        
        if(PlayerPrefs.GetInt(item.name, 0) == 1)
            return;
        
        MoneyManager.instance.SetMoneyCount(-item.cost);
        PlayerPrefs.SetInt(item.name, 1);

        if (item.canSelect)
        {
            Select(id);
        }

        if (item.notifyPurchase)
        {
            UIManager.instance.OpenPanelOverlay(onBuyPanel.name);
            onBuyName.text = item.name;
            onBuyImage.sprite = item.sprite;
        }

        UpdateItems();
    }

    public void UpdateItems()
    {
        foreach (ShopItem item in _shopItems)
        {
            if (PlayerPrefs.GetInt(item.data.name, 0) == 0)
            {
                if (item.data.isNewYear)
                {
                    item.SetState("NewYear");
                }
                else if (item.data.isStarBall)
                {
                    item.SetState("StarBall");
                }
                else
                {
                    item.SetState("NotBuyed");
                }
            }
            else
            {
                item.SetState("Buyed");
                
                if (PlayerPrefs.GetString(SELECTED_BALL) == item.data.name)
                {
                    item.SetState("Selected");
                }
            }
        }
    }
}

[System.Serializable]
public class ShopItemData
{
    public string name;

    public int cost;

    public Sprite sprite;

    public bool canSelect;
    public bool notifyPurchase;
    
    public bool isNewYear;
    public bool isStarBall;
}