using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LongPressSubButton : MonoBehaviour, IPointerEnterHandler
{
    private bool pointerEnter;
    private float pointerEnterTimer;

    public float requiredHoldTime = 0.4f;

    public UnityEvent onPointerEnter;

    [SerializeField]
    private Image fillImage;

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerEnter = true;
    }

    private void Update()
    {
        
        if (pointerEnter)
        {
            pointerEnterTimer += Time.deltaTime;
            if (pointerEnterTimer >= requiredHoldTime)
            {
                if (onPointerEnter != null)
                {
                    DeviceDisplay.currentActiveSubButton = this;

                    onPointerEnter.Invoke();
                    Handheld.Vibrate();
                }

                Reset();
            }
            fillImage.fillAmount = pointerEnterTimer / requiredHoldTime;
        }
    }

    private void Reset()
    {
        pointerEnter = false;
        pointerEnterTimer = 0;
        fillImage.fillAmount = pointerEnterTimer / requiredHoldTime;
    }

}