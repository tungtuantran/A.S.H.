using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
 * selects long press button just if if pointer is down
 */
public class MenuMainLongPressButton : LongPressButton, IPointerDownHandler, IPointerUpHandler
{

    void OnMouseUp()
    {
        Reset();

        if (onRelease != null)
        {
            onRelease.Invoke();
        }

        currentlyActive = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        hold = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        release = true;
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