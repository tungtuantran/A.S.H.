using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LongPressedButton2 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    public float requiredHoldTime = 0.4f;
    public Color backgroundColorCurrentlyActive;

    public UnityEvent onHold;
    public UnityEvent onPointerExit;

    [SerializeField]
    private Image backgroundImage;

    [SerializeField]
    private Image fillImage;

    private Color backgroundColorOnDefault;
    private bool pointerExit;
    private bool pointerEnter;
    private bool currentlyActive;
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
    private float pointerEnterTimer;

    void Awake()
    {
        backgroundColorOnDefault = backgroundImage.color;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointerExit = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerExit = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerEnter = true;
    }

    private void Update()
    {
        if (pointerExit)
        {
            Debug.Log("pointer exit");

            Reset();

            if (onPointerExit != null)
            {
                onPointerExit.Invoke();
            }

            currentlyActive = false;
        }

        if (!currentlyActive)
        {
            if (pointerEnter)
            {
                pointerEnterTimer += Time.deltaTime;
                if (pointerEnterTimer >= requiredHoldTime)
                {
                    if (onHold != null)
                    {
                        currentlyActive = true;
                        Handheld.Vibrate();
                        onHold.Invoke();
                        Debug.Log("invoke sth");
                    }
                    Reset();
                }
                fillImage.fillAmount = pointerEnterTimer / requiredHoldTime;
            }
        }
        UpdateBackgroundColor();
    }

    private void Reset()
    {
        pointerExit = false;
        pointerEnter = false;
        pointerEnterTimer = 0;
        fillImage.fillAmount = pointerEnterTimer / requiredHoldTime;
    }

    private void UpdateBackgroundColor()
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