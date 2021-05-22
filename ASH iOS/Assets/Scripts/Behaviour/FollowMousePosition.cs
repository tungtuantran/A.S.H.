using UnityEngine;

/*
 * Places a RectTransform depending on the mouse position
 */
public class FollowMousePosition : MonoBehaviour
{

    public RectTransform childRect;

    // width and height of childRect childRect
    private float width;
    private float height;

    // x and y axis value 
    float x, y;

    void Start()
    {
        //default pivot
        SetPivotOfChildRectToUp();                                                

        width = ((RectTransform)transform).rect.width;
        height = ((RectTransform)transform).rect.height;
    }

    public void SetPositionByMousePosition() {

        //set x axis value
        if (Input.mousePosition.x + width / 2 > Screen.width)       // if collids right border
        {
            x = Screen.width - width / 2;
            SetPivotOfChildRectToLeft();
        }
        else if(Input.mousePosition.x - width / 2 < 0)              // if collids left border
        {
            x =  width / 2;
            SetPivotOfChildRectToRight();
        }
        else
        {
            x = Input.mousePosition.x;
        }

        //set y axis value
        if (Input.mousePosition.y + height / 2 > Screen.height)      // if collids top border
        {
            y = Screen.height - height / 2;
            SetPivotOfChildRectToDown();
        }
        else if (Input.mousePosition.y - height / 2 < 0)             // if collids bottom border
        {
            y = height / 2;
            SetPivotOfChildRectToUp();
        }
        else
        {
            y = Input.mousePosition.y;
            SetPivotOfChildRectToUp();                              // back to default pivot
        }

        //set child rect position by x and y axis value
        transform.position = new Vector3(x, y, 0);

    }

    private void SetPivotOfChildRectToUp()
    {
        if (childRect != null)
        {
            childRect.anchorMin = new Vector2(0.5f, 1f);
            childRect.anchorMax = new Vector2(0.5f, 1f);
            childRect.pivot = new Vector2(0.5f, 0.5f);
            childRect.anchoredPosition = new Vector3(0, -(childRect.rect.height/2));
        }
    }

    private void SetPivotOfChildRectToDown()
    {
        if (childRect != null)
        {
            childRect.anchorMin = new Vector2(0.5f, 0);
            childRect.anchorMax = new Vector2(0.5f, 0);
            childRect.pivot = new Vector2(0.5f, 0.5f);
            childRect.anchoredPosition = new Vector3(0, childRect.rect.height / 2);
        }
    }

    private void SetPivotOfChildRectToLeft()
    {
        if (childRect != null)
        {
            childRect.anchorMin = new Vector2(0, 0.5f);
            childRect.anchorMax = new Vector2(0, 0.5f);
            childRect.pivot = new Vector2(0.5f, 0.5f);
            childRect.anchoredPosition = new Vector3(childRect.rect.width / 2, 0);
        }
    }

    private void SetPivotOfChildRectToRight()
    {
        if (childRect != null)
        {
            childRect.anchorMin = new Vector2(1f, 0.5f);
            childRect.anchorMax = new Vector2(1f, 0.5f);
            childRect.pivot = new Vector2(0.5f, 0.5f);
            childRect.anchoredPosition = new Vector3(-(childRect.rect.width/2), 0);
        }
    }

}