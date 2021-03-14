using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMousePosition : MonoBehaviour
{

    public RectTransform childRect;

    //width and height of object
    private float width;
    private float height;

    float x, y;

    void Start()
    {
        //default pivot
        SetPivotOfChildRectToUp();                                                

        width = ((RectTransform)transform).rect.width;
        height = ((RectTransform)transform).rect.height;
    }

    void Update() {

        //set x position
        if ((float) Input.mousePosition.x + width / 2 > Screen.width)       // if collids right border
        {
            x = Screen.width - width / 2;
            SetPivotOfChildRectToLeft();
        }
        else if((float) Input.mousePosition.x - width / 2 < 0)             // if collids left border
        {
            x =  width / 2;
            SetPivotOfChildRectToRight();
        }
        else
        {
            x = Input.mousePosition.x;
        }

        //set y position
        if ((float)Input.mousePosition.y + height / 2 > Screen.height)      // if collids top border
        {
            y = Screen.height - height / 2;
            SetPivotOfChildRectToDown();
        }
        else if ((float)Input.mousePosition.y - height / 2 < 0)             // if collids bottom border
        {
            y = height / 2;
            SetPivotOfChildRectToUp();
        }
        else
        {
            y = Input.mousePosition.y;
            SetPivotOfChildRectToUp();                                            // back to default pivot
        }

        //set position
        this.transform.position = new Vector3(x, y, 0);

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