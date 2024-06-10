using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimator : MonoBehaviour, IPointerClickHandler
{
    private Vector2 _defaultScale;

    private void Awake()
    {
        _defaultScale = transform.localScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.DOShakeScale(UIManager.instance.buttonAnimationDuration, UIManager.instance.buttonAnimationStrength,
            UIManager.instance.buttonAnimationVibrato).SetEase(UIManager.instance.buttonAnimationEase).OnComplete(() =>
        {
            transform.DOScale(_defaultScale, UIManager.instance.buttonAnimationDuration / 2f)
                .SetEase(UIManager.instance.buttonAnimationEase);
        });
    }
}
