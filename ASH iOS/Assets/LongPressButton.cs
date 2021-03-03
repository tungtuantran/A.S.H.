using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LongPressButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    private bool pointerEnter;
    private bool pointerDown;
    private bool currentlyActive;
    private float pointerDownTimer;

    public float requiredHoldTime = 0.4f;

    public UnityEvent onPointerDown;
    public UnityEvent onPointerUp;

    [SerializeField]
    private Image fillImage;

    /*
    public void OnMouseOver()
    {
        pointerDown = true;
        Debug.Log("Mouse over");
    }
    */

    void OnMouseUp()
    {
        Reset();

        if (onPointerUp != null)
        {
            onPointerUp.Invoke();
        }

        currentlyActive = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerEnter = true;
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
            CopyPasteSystem.swipeToCopyPaste = true;       // activates swipe function to copy/paste device values

            if (pointerDown || pointerEnter)
            {
                pointerDownTimer += Time.deltaTime;
                if (pointerDownTimer >= requiredHoldTime)
                {
                    if (onPointerDown != null)
                    {

                        onPointerDown.Invoke();
                        Handheld.Vibrate();
                        currentlyActive = true;
                    }

                    Reset();
                }
                fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
            }
        }
        else
        {
            CopyPasteSystem.swipeToCopyPaste = false;       // deactivates swipe function to copy/paste device values
        }
    }

    private void Reset()
    {
        pointerEnter = false;
        pointerDown = false;
        pointerDownTimer = 0;
        fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
    }

}