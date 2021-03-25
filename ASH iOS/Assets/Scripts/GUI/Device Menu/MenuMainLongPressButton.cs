﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuMainLongPressButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float requiredHoldTime = 0.4f;
    public UnityEvent onHold;
    public UnityEvent onPointerUp;

    [SerializeField]
    private Image fillImage;

    private bool pointerDown;
    private bool currentlyActive;
    private float pointerDownTimer;

    void OnMouseUp()
    {
        Reset();

        if (onPointerUp != null)
        {
            onPointerUp.Invoke();
        }

        currentlyActive = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();

        if (onPointerUp != null)
        {
            onPointerUp.Invoke();
        }

        currentlyActive = false;
    }

    private void Update()
    {
        if (!currentlyActive)
        {
            if (pointerDown)
            {
                pointerDownTimer += Time.deltaTime;
                if (pointerDownTimer >= requiredHoldTime)
                {
                    if (onHold != null)
                    {
                        currentlyActive = true;
                        Handheld.Vibrate();
                        onHold.Invoke();
                    }
                    Reset();
                }
                fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
            }
        }
    }

    private void Reset()
    {
        pointerDown = false;
        pointerDownTimer = 0;
        fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
    }

}