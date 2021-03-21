using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[Serializable]
public class ColorEvent : UnityEvent<Color>
{
}

public class ColorPicker : MonoBehaviour
{
    public KeepDistanceInfront keepDistanceInfront;
    public bool worldSpaceMode;

    public ColorPreview colorPreview;

    public Color selectedColor { get; set; } = Color.white;
    private RectTransform Rect;
    private Texture2D ColorTexture;
    private Camera aRCamera;
    private bool pointerDown;
    private bool active;                                

    public bool Active
    {
        get { return active; }

        set
        {
            if (keepDistanceInfront != null)
            {
                keepDistanceInfront.SetLine();
            }

            Image colorImage = GetComponent<Image>();

            if (value)
            {
                colorImage.color = new Color(1f, 1f, 1f, 0.3f);
            }
            else
            {
                colorImage.color = new Color(1f, 1f, 1f, 0.0f);
            }
            active = value;
        }
    }

    void Start()
    {
        Rect = GetComponent<RectTransform>();
        ColorTexture = GetComponent<Image>().mainTexture as Texture2D;

        if (worldSpaceMode)
        {
            aRCamera = Camera.main;
        }
    }

    void Update()
    {
        /*
        if (colorPreview != null)
        {
            colorPreview.SetActive(active);
        }
        */

        if (Input.GetMouseButtonDown(0))
        {
            pointerDown = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            pointerDown = false;
        }

        if (pointerDown && active)
        {
            SelectColor();
        }
    }

    private void SelectColor()                                  // get color by mousePosition in Rect
    {
        //delta is the distance from the Rect center
        Vector2 delta;

        //width and height of Rect
        float width, height;

        // Unity's Library function that grabs a point in the screen and converts it into a point in the Rect
        //in this case we convert the mpuse position
        RectTransformUtility.ScreenPointToLocalPointInRectangle(Rect, Input.mousePosition, aRCamera, out delta);

        width = Rect.rect.width;
        height = Rect.rect.height;
        delta += new Vector2(width * .5f, height * .5f);

        //if mouse is in image 
        float x = Mathf.Clamp(delta.x / width, 0f, 1f);
        float y = Mathf.Clamp(delta.y / height, 0f, 1f);

        //convert rect x,y values into texture x,y values
        int texX = Mathf.RoundToInt(x * ColorTexture.width);
        int texY = Mathf.RoundToInt(y * ColorTexture.height);

        // get color from textures pixel in position x,y
        Color color = ColorTexture.GetPixel(texX, texY);

        // makes color not transparent anymore
        color.a = 1;

        // set color
        selectedColor = color;

        //s how preview
        /*
        if (colorPreview != null)
        {
            colorPreview.SetColor(color);
        }
        */
    }
}
