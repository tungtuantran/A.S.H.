using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ShortClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private bool pointerDown;
    private float pointerDownTimer;

    public float invalidHoldTime = 1;

    public UnityEvent onShortClick;

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointerDown = false;
    }

    private void Update()
    {
        if (pointerDown)
        {
            pointerDownTimer += Time.deltaTime;
        }

        if (!pointerDown)
        {
            if (pointerDownTimer > 0 && pointerDownTimer < invalidHoldTime)
            {
                if (onShortClick != null)
                {
                    onShortClick.Invoke();
                }
            }
            Reset();
        }

    }

    private void Reset()
    {
        pointerDown = false;
        pointerDownTimer = 0;
    }
}