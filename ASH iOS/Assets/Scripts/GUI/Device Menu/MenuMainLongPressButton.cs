using UnityEngine.EventSystems;

/**
 * Main Menu as Long Press Button for Device Menu. 
 * (Selects Long Press Button just if pointer is down.)
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