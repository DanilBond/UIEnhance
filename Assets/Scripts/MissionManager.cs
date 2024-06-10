using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


[System.Serializable]
public class MissionInfo
{
    public int id;
    public string desc;
    public int reward;
}

public class MissionManager : MonoBehaviour
{
    public MissionInfo[] infos;
    public MissionItem itemPrefab;
    public Transform missionsParent;

    private void Start()
    {
        UpdateMissions();
    }

    public void UpdateMissions()
    {
        foreach (Transform t in missionsParent)
        {
            Destroy(t.gameObject);
        }

        foreach (MissionInfo info in infos)
        {
            MissionItem item = Instantiate(itemPrefab, missionsParent);
            bool isCompleted = false;
            if (info.id == 0)
            {
                if (PlayerPrefs.GetInt("AllMoney") >= 5000)
                {
                    isCompleted = true;
                }
            }
            if (info.id == 1)
            {
                if (PlayerPrefs.GetInt("LoseAll") == 1)
                {
                    isCompleted = true;
                }
            }
            if (info.id == 2)
            {
                if (PlayerPrefs.GetInt("AllMoney") >= 10000)
                {
                    isCompleted = true;
                }
            }
            if (info.id == 3)
            {
                if (PlayerPrefs.GetInt("AllIn") == 1)
                {
                    isCompleted = true;
                }
            }
            
            item.Init(info, isCompleted);
        }
    }
}
