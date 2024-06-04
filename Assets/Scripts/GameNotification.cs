using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameNotification : MonoBehaviour
{
    public static GameNotification instance;

    public Transform panel;
    public TextMeshProUGUI text;

    public float duration;
    public float delay;
    public Ease ease;

    public Color[] colors;
    
    public float outPositionX;
    public float inPositionX;
    
    private Queue<GameNotificationData> dataQueue = new Queue<GameNotificationData>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Init();
        
        StartCoroutine(Animate());
    }

    public void Init()
    {
        dataQueue = new Queue<GameNotificationData>();
        
        RectTransform rect = panel.gameObject.GetComponent<RectTransform>();
                    
        rect.anchoredPosition = new Vector2(outPositionX, rect.anchoredPosition.y);
    }

    public void Notify(GameNotificationData notification)
    {
        dataQueue.Enqueue(notification);
    }

    private IEnumerator Animate()
    {
        while (true)
        {
            if (dataQueue.TryDequeue(out GameNotificationData data))
            {
                RectTransform rect = panel.gameObject.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(outPositionX, rect.anchoredPosition.y);

                panel.gameObject.GetComponent<Image>().color = colors[(int)data.type];
                text.text = "+" + data.count;

                rect.DOAnchorPosX(inPositionX, duration).SetEase(ease);
                yield return new WaitForSeconds(duration);
                yield return new WaitForSeconds(delay);
                rect.DOAnchorPosX(outPositionX, duration).SetEase(ease);
                yield return new WaitForSeconds(duration);
            }
            else
            {
                yield return null;
            }
        }
    }

    public struct GameNotificationData
    {
        public int count;
        public GameNotificationType type;

        public GameNotificationData(int c, GameNotificationType t)
        {
            count = c;
            type = t;
        }
    }
    public enum GameNotificationType
    {
        Yellow = 0,
        Orange = 1,
        DarkOrange = 2,
        Red = 3
    }
}

