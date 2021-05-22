using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/**
 * Short Press Button
 * Selecting by a quick click/tap.
 */
public class ShortPressButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public float invalidHoldTime = 1.0f;
    public UnityEvent onPointerDown;
    public UnityEvent onShortClick;

    private bool pointerDown;
    private float pointerDownTimer;

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