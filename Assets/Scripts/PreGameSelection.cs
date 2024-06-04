using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PreGameSelection : MonoBehaviour
{
    public static PreGameSelection instance;

    public PGSItem[] fieldButtons;
    public PGSItem[] difficultButtons;
    
    private void Awake()
    {
        instance = this;
    }

    public void CheckSelected()
    {
        foreach (PGSItem pgsItem in fieldButtons)
        {
            pgsItem.button.color = pgsItem.id == GameManager.instance.selectedFieldId ? Color.yellow : Color.white;
        }
        
        foreach (PGSItem pgsItem in difficultButtons)
        {
            pgsItem.button.color = pgsItem.id == GameManager.instance.selectedDifficultId ? Color.yellow : Color.white;
        }
    }

    public void SelectField(int id)
    {
        GameManager.instance.selectedFieldId = fieldButtons[id].id;
        CheckSelected();
    }
    public void SelectDifficult(int id)
    {
        GameManager.instance.selectedDifficultId = difficultButtons[id].id;
        CheckSelected();
    }

    public string GetRandomField()
    {
        return fieldButtons[Random.Range(0, fieldButtons.Length)].id;
    }
    public string GetRandomDifficult()
    {
        return difficultButtons[Random.Range(0, difficultButtons.Length)].id;
    }
}

[System.Serializable]
public class PGSItem
{
    public string id;
    public Image button;
}