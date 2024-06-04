using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFieldManager : MonoBehaviour
{
    public static GameFieldManager instance;

    public FieldItem[] fields;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CheckField();
    }

    public void CheckField()
    {
        foreach (FieldItem field in fields)
        {
            field.field.SetActive(field.id == GameManager.instance.selectedFieldId);
        }
    }
}

[System.Serializable]
public class FieldItem
{
    public string id;
    public GameObject field;
}