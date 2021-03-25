using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuSubLongPressButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Image fillImage;

    [SerializeField]
    private Image backgroundImage;

    public float requiredHoldTime = 0.4f;
    public Color backgroundColorCurrentlyActive;

    public UnityEvent onHold;
    public UnityEvent onExit;

    private Color backgroundColorOnDefault;
    private bool pointerEnter;
    private float pointerEnterTimer;
    private bool pointerExit;
    private bool currentlyActive;

    public bool CurrentlyActive
    {
        get { return currentlyActive; }
        set
        {
            currentlyActive = value;
            Reset();
        }
    }

    void Awake()
    {
        backgroundColorOnDefault = backgroundImage.color;
    }

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
            if(onExit != null)
            {
                Debug.Log("on exit executed");
                onExit.Invoke();
            }
        }

        if (pointerEnter && ! CurrentlyActive)
        {
            pointerEnterTimer += Time.deltaTime;
            if (pointerEnterTimer >= requiredHoldTime)
            {
                if (onHold != null)
                {
                    CurrentlyActive = true;
                    Handheld.Vibrate();
                    onHold.Invoke();
                }

                Reset();
            }
            fillImage.fillAmount = pointerEnterTimer / requiredHoldTime;
        }

        SetBackgroundColor();
    }

    private void Reset()
    {
        pointerExit = false;
        pointerEnter = false;
        pointerEnterTimer = 0;
        fillImage.fillAmount = pointerEnterTimer / requiredHoldTime;
    }

    private void SetBackgroundColor()
    {
        if (currentlyActive)
        {
            backgroundImage.color = backgroundColorCurrentlyActive;
        }
        else
        {
            backgroundImage.color = backgroundColorOnDefault;
        }
    }

}