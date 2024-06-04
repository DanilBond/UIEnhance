using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class UIPanel : MonoBehaviour
{
    public string panelName;
    
    public float fadeDuration;

    
    private void OnEnable()
    {
        StartCoroutine(UpdateRect());
    }

    public virtual void Open(Action onOpenCallback)
    {
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().DOFade(1f, fadeDuration).OnComplete(() =>
        {
            onOpenCallback?.Invoke();
        });
    }

    public virtual void Close(Action onCloseCallback)
    {
        GetComponent<CanvasGroup>().alpha = 1f;
        GetComponent<CanvasGroup>().DOFade(0f, fadeDuration).OnComplete(() =>
        {
            onCloseCallback?.Invoke();
            gameObject.SetActive(false);
        });
    }

    private IEnumerator UpdateRect()
    {
        yield return new WaitForEndOfFrame();
        foreach (HorizontalOrVerticalLayoutGroup gr in FindObjectsOfType<HorizontalOrVerticalLayoutGroup>())
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(gr.transform as RectTransform);
        }
    }
}
