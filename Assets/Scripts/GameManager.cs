using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    //public int defaultBid;
    public int maxBallsPerGame;

    [Space] 
    public TextMeshProUGUI currentBidText;

    public int bidDefaultValue;
    public int currentBid;

    [Space]
    public Transform gameParent;
    public GameObject ballPrefab;
    public Transform startPosition;
    public Vector2 startPositionMinMax;

    [Space] 
    public Button dropButton;

    [Space] 
    public Image speedButton;

    public Sprite enableSpeed;
    public Sprite disableSpeed;
    public bool speedActivated;

    [HideInInspector]
    public List<GameObject> balls = new List<GameObject>();

    private bool isAutoPlay;

    public string selectedFieldId;
    public string selectedDifficultId;
    
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(Tick());
        
        CloseGame();
    }

    public void StartGame()
    {
        if (string.IsNullOrEmpty(selectedFieldId))
        {
            selectedFieldId = PreGameSelection.instance.GetRandomField();
        }
        if (string.IsNullOrEmpty(selectedDifficultId))
        {
            selectedDifficultId = PreGameSelection.instance.GetRandomDifficult();
        }
        
        GameNotification.instance.Init();

        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
        balls = new List<GameObject>();
        isAutoPlay = false;

        dropButton.interactable = true;
        
        GameFieldManager.instance.CheckField();
        DifficultManager.instance.CheckGoal();
        
        CheckSpeed();
        
        CheckDynamicPlatform();
        
        OpenGame();
    }

    public void StopGame()
    {
        selectedDifficultId = "";
        selectedFieldId = "";
        PreGameSelection.instance.CheckSelected();
        
        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
        CloseGame();
    }
    

    public void AutoPlay()
    {
        isAutoPlay = !isAutoPlay;
        dropButton.interactable = !isAutoPlay;
    }
    public void Play()
    {
        if(!CanPlay())
            return;

        if (currentBid == MoneyManager.instance.GetMoneyCount())
        {
            PlayerPrefs.SetInt("AllIn", 1);
        }
        MoneyManager.instance.SetMoneyCount(-currentBid);

        Vector2 pos = startPosition.position;
        pos.x += Random.Range(startPositionMinMax.x, startPositionMinMax.y);
        
        GameObject ball = Instantiate(ballPrefab, pos, Quaternion.identity);
        ball.GetComponent<Ball>().ballBid = currentBid;
        balls.Add(ball);
    }

    private void CheckDynamicPlatform()
    {
        foreach (Goal goal in FindObjectsOfType<Goal>(true))
        {
            if (goal.isDynamic)
            {
                goal.gameObject.SetActive(PlayerPrefs.GetInt("DYNAMIC PLATFORM", 0) == 1);
                return;
            }
        }
    }

    public bool CanPlay()
    {
        if(MoneyManager.instance.GetMoneyCount() < currentBid)
            return false;
        if(balls.Count >= maxBallsPerGame)
            return false;
        if (currentBid == 0)
            return false;

        return true;
    }

    public void UpdateBid()
    {
        currentBid = Mathf.Clamp(currentBid, 0, MoneyManager.instance.GetMoneyCount());
        currentBidText.text = currentBid.ToString();
    }

    public void CheckSpeed()
    {
        speedButton.sprite = PlayerPrefs.GetInt("SPEED", 0) == 0 ? disableSpeed : enableSpeed;
    }

    public void SwitchSpeed()
    {
        if (PlayerPrefs.GetInt("SPEED", 0) == 1)
        {
            speedActivated = !speedActivated;
        }
        else
        {
            speedActivated = false;
        }
    }

    public void PlusBid()
    {
        currentBid += bidDefaultValue;
        UpdateBid();
        
    }
    public void MinusBid()
    {
        currentBid -= bidDefaultValue;
        UpdateBid();
    }

    public void MaxBid()
    {
        currentBid = MoneyManager.instance.GetMoneyCount();
        UpdateBid();
    }

    public void MinBid()
    {
        currentBid = 0;
        UpdateBid();
    }

    IEnumerator Tick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            if (isAutoPlay)
            {
                for (int i = 0; i < maxBallsPerGame; i++)
                {
                    Play();
                }
            }
        }
    }
    
    
    public void OpenGame()
    {
        gameParent.gameObject.SetActive(true);
        gameParent.transform.localScale = Vector3.zero;
        gameParent.transform.DOScale(Vector3.one, .2f);
    }
    public void CloseGame()
    {
        gameParent.transform.localScale = Vector3.zero;
        gameParent.gameObject.SetActive(false);
    }
}
