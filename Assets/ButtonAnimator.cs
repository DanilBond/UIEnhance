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
        transform.DOShakeScale(.15f, 1).OnComplete(() => { transform.DOScale(_defaultScale, .1f);});
    }
}
