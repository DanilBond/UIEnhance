using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class ButtonAnimationManager
{
    [MenuItem("Tools/FlexTools/AddAnimatorToAllButtons")]
    private static void AddAnimatorToAllButtons()
    {
        foreach (Button button in GameObject.FindObjectsOfType<Button>(true))
        {
            if (button.GetComponent<ButtonAnimator>())
                continue;

            Undo.AddComponent<ButtonAnimator>(button.gameObject);
        }
    }
    
    [MenuItem("Tools/FlexTools/AddAnimatorToButtonsWithoutTransitions")]
    private static void AddAnimatorToButtonsWithoutTransitions()
    {
        foreach (Button button in GameObject.FindObjectsOfType<Button>(true))
        {
            if (button.GetComponent<ButtonAnimator>())
                continue;
            if (button.transition != Selectable.Transition.None)
                continue;
            
            Undo.AddComponent<ButtonAnimator>(button.gameObject);
        }
    }
}
