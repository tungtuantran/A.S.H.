using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyPasteSystem : MonoBehaviour
{
    public static Device copiedDevice;
    public static bool active = true;
    //public static Device selectedDevice;

    [SerializeField]
    private Text copiedDeviceInformationText;

    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;

    void Update()
    {
        Swipe();
        UpdateCopyText();
    }

    public void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();

            //swipe upwards
            if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                Copy();
                active = true;
                Debug.Log("up swipe");
            }

            //swipe down
            if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                Paste();
                active = true;
                Debug.Log("down swipe");
            }

            /*
            //swipe left
            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                swipeToCopyPaste = true;
                Debug.Log("left swipe");
            }

            //swipe right
            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                swipeToCopyPaste = true;
                Debug.Log("right swipe");
            }
            */
        }
    }

    public void Copy()
    {
        if(active)
        {
            if (SelectDevice.selectedDevice != null)
            {
                copiedDevice = SelectDevice.selectedDevice;
                Handheld.Vibrate();
            }
        }
    }

    public void Paste()
    {
        if (active)
        {
            if (SelectDevice.selectedDevice != null)
            {
                Device deviceToPasteIn = SelectDevice.selectedDevice;

                //same type name
                if (copiedDevice.GetType().Name.Equals(deviceToPasteIn.GetType().Name))
                {
                    switch (copiedDevice.GetType().Name)
                    {
                        //is lamp
                        case "Lamp":
                            ((Lamp)deviceToPasteIn).LightBrightness = ((Lamp)copiedDevice).LightBrightness;
                            ((Lamp)deviceToPasteIn).LightColor = ((Lamp)copiedDevice).LightColor;
                            ((Lamp)deviceToPasteIn).LightTemperature = ((Lamp)copiedDevice).LightTemperature;
                            Handheld.Vibrate();
                            break;
                        //is unknown
                        default:
                            break;
                    }
                }
            }
        }
    }

    private void UpdateCopyText()
    {
        if (copiedDevice != null)
        {
            copiedDeviceInformationText.text = copiedDevice.Name;
        }
        else
        {
            copiedDeviceInformationText.text = "Empty";
        }
    }
}
