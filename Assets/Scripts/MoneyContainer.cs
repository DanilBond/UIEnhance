using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyContainer : MonoBehaviour
{
    public void UpdateMoney()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = MoneyManager.instance.GetMoneyCount().ToString();
    }
}
