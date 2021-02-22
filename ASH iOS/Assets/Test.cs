using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Test : MonoBehaviour //, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    public ColorPickerTriangle colorPicker;

    //[SerializeField]
    //public Light _light;

    private Material mat;


    //private bool isPaint = false;

    private void Start()
    {
        Debug.Log("test script started");

        mat = GetComponent<MeshRenderer>().material;
        colorPicker.SetNewColor(mat.color);


    }

    private void Update()
    {
        Debug.Log("test script updated");

            //SetLightColor(colorPicker.TheColor);
            mat.color = colorPicker.TheColor;

            Debug.Log("set color");
            colorPicker.SetNewColor(mat.color);

    }

    /*
    private void SetLightColor(Color color)
    {
        _light.color = color;
        Debug.Log("light color set to: " + _light.color);
    }

    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isPaint)
        {
            StopPaint();
        }
        else
        {
            StartPaint();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //nothing
    }
    */

    /*
    void OnMouseDown()
    {
        Debug.Log("OnMouseDown called");
        if (isPaint)
        {
            StopPaint();
        }
        else
        {
            StartPaint();
        }
    }
    */
}
