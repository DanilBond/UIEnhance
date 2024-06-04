using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager insntance;
    
    public GameObject[] pages;
    public UIPanel mainMenu;

    private int currentPage;

    public UnityEvent OnTutorialPassed;

    private void Awake()
    {
        insntance = this;
    }

    public void Next()
    {
        currentPage++;

        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
        }

        if (currentPage <= pages.Length - 1)
        {
            pages[currentPage].SetActive(true);
        }
        else
        {
            Skip();
        }
    }

    public void Skip()
    {
        PlayerPrefs.SetInt("TutorialPassed", 1);
        //UIManager.instance.OpenPanel(mainMenu.panelName);
        
        OnTutorialPassed?.Invoke();
    }
}
