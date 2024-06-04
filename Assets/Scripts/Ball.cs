using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private bool isContacted = false;

    public int ballBid;

    private void Start()
    {
        SetSkin();
        SetSpeed();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Goal goal))
        {
            if(isContacted)
                return;
            
            isContacted = true;
            float coins = ballBid;
            coins *= goal.multiplier;

            MoneyManager.instance.SetMoneyCount(Mathf.RoundToInt(coins));

            GameNotification.instance.Notify(
                new GameNotification.GameNotificationData(Mathf.RoundToInt(coins), goal.type));

            GameManager.instance.balls.Remove(gameObject);
            
            Destroy(gameObject);
        }
    }

    private void SetSkin()
    {
        GetComponent<SpriteRenderer>().sprite = BallSkinManager.instance.GetSkin();
    }

    private void SetSpeed()
    {
        GetComponent<Rigidbody2D>().gravityScale = GameManager.instance.speedActivated ? 2 : 1;
    }
}
