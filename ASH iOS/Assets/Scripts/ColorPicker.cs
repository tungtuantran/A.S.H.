﻿using System.Collections;
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

    public KeepDistanceInfront keepDistanceInfront;         //no serializeField -> no force

    [SerializeField]
    public Image colorPreview;

    public bool worldSpaceMode;

    private RectTransform Rect;
    private Texture2D ColorTexture;
    private Camera aRCamera;
    private bool pointerDown;

    public Color selectedColor { get; set; } = Color.white;
    private bool _active;                                   //TODO: can it replace pointerDown bool?

    public bool active
    {
        get { return _active; }

        set
        {
            if (value && keepDistanceInfront != null)
            {
                keepDistanceInfront.SetDirection();
            }
            _active = value;
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
        colorPreview.gameObject.SetActive(active);

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
        color.a = 1;                                            //so color is not transparent anymore
        selectedColor = color;

        colorPreview.color = color;
    }
}
