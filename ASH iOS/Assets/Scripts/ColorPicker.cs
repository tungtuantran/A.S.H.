using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

[Serializable]
public class ColorEvent : UnityEvent<Color>
{
}

public class ColorPicker : MonoBehaviour
{
    [SerializeField]
    public Camera arCamera;

    [SerializeField]
    public GameObject PointerColor;

    [SerializeField]
    public TextMeshProUGUI colorText;

    public ColorEvent OnColorPreview;
    public ColorEvent OnColorSelect;

    RectTransform Rect;
    Texture2D ColorTexture;

    // Start is called before the first frame update
    void Start()
    {
        Rect = GetComponent<RectTransform>();
        ColorTexture = GetComponent<Image>().mainTexture as Texture2D;

    }

    // Update is called once per frame
    void Update()
    {
        //if (RectTransformUtility.RectangleContainsScreenPoint(Rect, Input.GetTouch(0).position))                       //nur wenn mouse innherhalb des rectamgöes ist
        //{
            Debug.Log("callled");
            Vector2 delta;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(Rect, Input.mousePosition, arCamera, out delta);

            Debug.Log("mousePosition: " + Input.mousePosition + "; Delta: " + delta);

            float width = Rect.rect.width;
            float height = Rect.rect.height;
            delta += new Vector2(width * .5f, height * .5f);

            //Debug.Log("offset delta:" + delta);

            float x = Mathf.Clamp(delta.x / width, 0f, 1f);
            float y = Mathf.Clamp(delta.y / height, 0f, 1f);
            Debug.Log("x=" + x + " y=" + y);

            int texX = Mathf.RoundToInt(x * ColorTexture.width);
            int texY = Mathf.RoundToInt(y * ColorTexture.height);

            Color color = ColorTexture.GetPixel(texX, texY);
            colorText.color = color;

            OnColorPreview?.Invoke(color);

            if (Input.GetMouseButtonDown(0))
            {
                OnColorSelect?.Invoke(color);
            }
        //}
    }
}
