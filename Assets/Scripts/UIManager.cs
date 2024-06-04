using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public UIPanel mainPanel;
    public UIPanel tutorialPanel;
    
    private List<UIPanel> panels = new List<UIPanel>();
    private UIPanel currentPanel;

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    private void Awake()
    {
        instance = this;

        panels = FindObjectsOfType<UIPanel>(true).ToList();
        
        foreach (UIPanel panel in panels)
        {
            panel.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("TutorialPassed", 0) == 1)
        {
            mainPanel.gameObject.SetActive(true);
        }
        else
        {
            tutorialPanel.gameObject.SetActive(true);
        }
    }

    public void OpenPanelOverlay(string panelName)
    {
        UIPanel panel = panels.Find(p => p.panelName == panelName);
        if (panel != null)
        {
            panel.gameObject.SetActive(true);
            panel.Open(null);
            currentPanel = panel;
        }
    }

    public void OpenPanel(string panelName)
    {
        panels.ForEach((uiPanel =>
        {
            if (uiPanel.name != panelName)
            {
                uiPanel.Close(() => { uiPanel.gameObject.SetActive(false); });
            }
        }));
        
        UIPanel panel = panels.Find(p => p.panelName == panelName);
        if (panel != null)
        {
            panel.gameObject.SetActive(true);
            panel.Open(null);
            currentPanel = panel;
        }
    }

    public void ClosePanel(string panelName)
    {
        UIPanel panel = panels.Find(p => p.panelName == panelName);
        if (panel != null)
        {
            panel.Close(null);
        }
    }
}
