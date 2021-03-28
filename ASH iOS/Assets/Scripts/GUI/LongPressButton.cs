using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class LongPressButton : MonoBehaviour
{

    public float requiredHoldTime = 0.4f;
    public Color backgroundColorCurrentlyActive;

    public UnityEvent onHold;
    public UnityEvent onRelease;

    [SerializeField]
    protected Image fillImage;

    [SerializeField]
    protected Image backgroundImage;

    protected Color backgroundColorOnDefault;
    protected bool hold;
    protected bool release;
    protected bool currentlyActive;
    protected float holdTimer;

    void Awake()
    {
        backgroundColorOnDefault = backgroundImage.color;
    }

    protected void OnRelease()
    {
        Reset();

        if (onRelease != null)
        {
            onRelease.Invoke();
        }
    }

    protected virtual void Update()
    {
        if (hold && !currentlyActive)
        {
            holdTimer += Time.deltaTime;
            if (holdTimer >= requiredHoldTime)
            {
                if (onHold != null)
                {
                    currentlyActive = true;
                    onHold.Invoke();
                    Handheld.Vibrate();
                }
                Reset();
            }
            fillImage.fillAmount = holdTimer / requiredHoldTime;
        }

        UpdateBackgroundColor();
    }

    protected virtual void Reset()
    {
        release = false;
        hold = false;
        holdTimer = 0;
        fillImage.fillAmount = holdTimer / requiredHoldTime;
    }

    protected void UpdateBackgroundColor()
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
