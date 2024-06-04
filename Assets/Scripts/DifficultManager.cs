using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultManager : MonoBehaviour
{
    public static DifficultManager instance;

    public GoalItem[] goals;
    
    private void Awake()
    {
        instance = this;
    }
    
    private void Start()
    {
        CheckGoal();
    }

    public void CheckGoal()
    {
        foreach (GoalItem goal in goals)
        {
            goal.goal.SetActive(goal.id == GameManager.instance.selectedDifficultId);
        }
    }
}

[System.Serializable]
public class GoalItem
{
    public string id;
    public GameObject goal;
}