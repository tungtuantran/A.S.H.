using UnityEngine.EventSystems;

/*
 * Sub Menu as Long Press Button for Device Menu.
 * (Selecting Long Press Button by just entering.)
 */
public class MenuSubLongPressButton : LongPressButton, IPointerEnterHandler, IPointerExitHandler
{
    public bool CurrentlyActive
    {
        get {
            return currentlyActive;
        }
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
        hold = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Reset();
    }

    protected override void Update()
    {
        if(release)
        {
            OnRelease();
        }

        base.Update();
    }
}