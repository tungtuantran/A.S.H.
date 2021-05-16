﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
 * selecting long press button by just entering and holding
 */
public class EnterLongPressButton : LongPressButton, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    public bool CurrentlyActive
    {
        get
        {
            return currentlyActive;
        }

        set
        {
            currentlyActive = value;
        }
    }

    void Awake()
    {
        backgroundColorOnDefault = backgroundImage.color;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        release = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        release = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hold = true;
    }

    protected override void Update()
    {
        if (release)
        {
            OnRelease();
            currentlyActive = false;
        }

        base.Update();
    }
}