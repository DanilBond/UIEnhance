using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSkinManager : MonoBehaviour
{
    public static BallSkinManager instance;
    
    public BallSkin[] skins;

    private void Awake()
    {
        instance = this;
    }

    public Sprite GetSkin()
    {
        foreach (BallSkin ballSkin in skins)
        {
            if (ballSkin.id == PlayerPrefs.GetString(ShopManager.SELECTED_BALL))
            {
                return ballSkin.sprite;
            }
        }

        return skins[0].sprite;
    }
}

[System.Serializable]
public class BallSkin
{
    public string id;
    public Sprite sprite;
}