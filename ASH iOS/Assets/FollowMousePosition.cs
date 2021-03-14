using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMousePosition : MonoBehaviour
{

    public RectTransform child;

    //width and height of object
    private float width;
    private float height;

    float x, y;

    void Start()
    {
        //default pivot
        SetPivotOfChildUp();                                                

        width = ((RectTransform)transform).rect.width;
        height = ((RectTransform)transform).rect.height;
    }

    void Update() {

        //set x position
        if ((float) Input.mousePosition.x + width / 2 > Screen.width)       // if collids right border
        {
            x = Screen.width - width / 2;
            SetPivotOfChildLeft();
        }
        else if((float) Input.mousePosition.x - width / 2 < 0)             // if collids left border
        {
            x =  width / 2;
            SetPivotOfChildRight();
        }
        else
        {
            x = Input.mousePosition.x;
        }

        //set y position
        if ((float)Input.mousePosition.y + height / 2 > Screen.height)      // if collids top border
        {
            y = Screen.height - height / 2;
            SetPivotOfChildDown();
        }
        else if ((float)Input.mousePosition.y - height / 2 < 0)             // if collids bottom border
        {
            y = height / 2;
            SetPivotOfChildUp();
        }
        else
        {
            y = Input.mousePosition.y;
            SetPivotOfChildUp();                                            // back to default pivot
        }

        //set position
        this.transform.position = new Vector3(x, y, 0);

    }

    private void SetPivotOfChildUp()
    {
        if (child != null)
        {
            child.anchorMin = new Vector2(0.5f, 1f);
            child.anchorMax = new Vector2(0.5f, 1f);
            child.pivot = new Vector2(0.5f, 0.5f);
            child.anchoredPosition = new Vector3(0, -(child.rect.height/2));
        }
    }

    private void SetPivotOfChildDown()
    {
        if (child != null)
        {
            child.anchorMin = new Vector2(0.5f, 0);
            child.anchorMax = new Vector2(0.5f, 0);
            child.pivot = new Vector2(0.5f, 0.5f);
            child.anchoredPosition = new Vector3(0, child.rect.height / 2);
        }
    }

    private void SetPivotOfChildLeft()
    {
        if (child != null)
        {
            child.anchorMin = new Vector2(0, 0.5f);
            child.anchorMax = new Vector2(0, 0.5f);
            child.pivot = new Vector2(0.5f, 0.5f);
            child.anchoredPosition = new Vector3(child.rect.width / 2, 0);
        }
    }

    private void SetPivotOfChildRight()
    {
        if (child != null)
        {
            child.anchorMin = new Vector2(1f, 0.5f);
            child.anchorMax = new Vector2(1f, 0.5f);
            child.pivot = new Vector2(0.5f, 0.5f);
            child.anchoredPosition = new Vector3(-(child.rect.width/2), 0);
        }
    }

}