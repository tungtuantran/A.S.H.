using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[Serializable]
public class ColorEvent2 : UnityEvent<Color>
{
}

public class ColorPickerTest : MonoBehaviour
{

    public KeepDistanceInfront keepDistanceInfront;         //no serializeField -> no force

    [SerializeField]
    public Image colorPreview;

    public bool worldSpaceMode;

    private RectTransform Rect;
    private Texture2D ColorTexture;
    private Camera aRCamera;
    private bool pointerDown;

    public Color selectedColor { get; set; } = Color.white;


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
        //colorPreview.gameObject.SetActive(active);

        if (RectTransformUtility.RectangleContainsScreenPoint(Rect, Input.mousePosition))
        {
            if (Input.GetMouseButtonDown(0))
            {
                pointerDown = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                pointerDown = false;
            }

            if (pointerDown)
            {
                SelectColor();
            }
        }
    }

    private void SelectColor()
    {
        Vector2 delta;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(Rect, Input.mousePosition, aRCamera, out delta);

        float width = Rect.rect.width;
        float height = Rect.rect.height;
        delta += new Vector2(width * .5f, height * .5f);

        float x = Mathf.Clamp(delta.x / width, 0f, 1f);
        float y = Mathf.Clamp(delta.y / height, 0f, 1f);

        int texX = Mathf.RoundToInt(x * ColorTexture.width);
        int texY = Mathf.RoundToInt(y * ColorTexture.height);


        Color color = ColorTexture.GetPixel(texX, texY);

        if(color.a == 0)
        {
            return;
        }

        color.a = 1;                                            //so color is not transparent anymore
        selectedColor = color;

        colorPreview.color = color;
    }
}
