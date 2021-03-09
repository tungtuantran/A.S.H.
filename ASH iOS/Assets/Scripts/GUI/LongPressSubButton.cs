﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LongPressSubButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool pointerEnter;
    private float pointerEnterTimer;
    private bool pointerExit;

    private bool _currentlyActive { get; set; }
    public bool currentlyActive
    {
        get { return _currentlyActive; }
        set
        {
            _currentlyActive = value;
            Reset();
        }
    }

    public float requiredHoldTime = 0.4f;

    public UnityEvent onPointerEnter;

    [SerializeField]
    private Image fillImage;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Reset();
        pointerEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Reset();
    }

    private void Update()
    {
        if(pointerExit)
        {
            Reset();
        }

        if (pointerEnter && !currentlyActive)
        {
            pointerEnterTimer += Time.deltaTime;
            if (pointerEnterTimer >= requiredHoldTime)
            {
                if (onPointerEnter != null)
                {
                    //DeviceDisplay.currentActiveSubButton = this;
                    currentlyActive = true;
                    Handheld.Vibrate();
                    onPointerEnter.Invoke();
                }

                Reset();
            }
            fillImage.fillAmount = pointerEnterTimer / requiredHoldTime;
        }
    }

    private void Reset()
    {
        pointerExit = false;
        pointerEnter = false;
        pointerEnterTimer = 0;
        fillImage.fillAmount = pointerEnterTimer / requiredHoldTime;
    }

}