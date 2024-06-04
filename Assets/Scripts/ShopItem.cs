using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public Image Image;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI CostText;

    public Button BuyButton;
    public Button SelectButton;

    public GameObject BuyedPanel;
    public GameObject SelectedPanel;
    public GameObject NotBuyedPanel;

    public ShopItemData data;

    public void SetState(string id)
    {
        switch (id)
        {
            case "Buyed":
                BuyedPanel.SetActive(true);
                SelectedPanel.SetActive(false);
                NotBuyedPanel.SetActive(false);
                break;
            case "Selected":
                BuyedPanel.SetActive(false);
                SelectedPanel.SetActive(true);
                NotBuyedPanel.SetActive(false);
                break;
            case "NotBuyed":
                BuyedPanel.SetActive(false);
                SelectedPanel.SetActive(false);
                NotBuyedPanel.SetActive(true);
                break;
        }
    }

    public void Init(ShopItemData data)
    {
        this.data = data;
        
        Image.sprite = data.sprite;
        NameText.text = data.name;
        CostText.text = data.cost.ToString();

        string itemId = data.name;
        
        BuyButton.onClick.AddListener(() =>
        {
            ShopManager.instance.Buy(itemId);
        });
        
        
        SelectButton.onClick.AddListener(() =>
        {
            ShopManager.instance.Select(itemId);
        });
    }
}
