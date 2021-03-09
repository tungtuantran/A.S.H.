using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyPasteSystem : MonoBehaviour
{
    private float copyPasteTextDisplayTimer = 4f;
    public Text copyPasteText;


    public static Device copiedDevice;
    public static bool swipeToCopyPaste = true;

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    private void Update()
    {
        Swipe();
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
            Debug.Log("swipeToCopyPaste is " + swipeToCopyPaste);

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
                swipeToCopyPaste = true;
                Debug.Log("up swipe");
            }

            //swipe down
            if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                Paste();
                swipeToCopyPaste = true;
                Debug.Log("down swipe");
            }

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
        }
    }

    public void Copy()
    {
        if(swipeToCopyPaste)
        {
            if (DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(ImageTracking.deviceId) != null)
            {
                copiedDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(ImageTracking.deviceId);
                Handheld.Vibrate();

                Debug.Log("copied device: " + copiedDevice.ToString());
                copyPasteText.text = "Copied values from " + copiedDevice._name;
            }
        }
        Invoke("ClearCopyPasteText", copyPasteTextDisplayTimer);

    }

    public void Paste()
    {
        if (swipeToCopyPaste)
        {
            if (DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(ImageTracking.deviceId) != null)
            {
                Device trackedDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(ImageTracking.deviceId);
                if (copiedDevice.GetType().Name.Equals(trackedDevice.GetType().Name))
                {
                    Debug.Log("same type name: " + copiedDevice.GetType().Name);
                    switch (copiedDevice.GetType().Name)
                    {
                        case "Lamp":
                            ((Lamp)trackedDevice).lightBrightness = ((Lamp)copiedDevice).lightBrightness;
                            ((Lamp)trackedDevice).lightColor = ((Lamp)copiedDevice).lightColor;
                            ((Lamp)trackedDevice).lightTemperature = ((Lamp)copiedDevice).lightTemperature;
                            Handheld.Vibrate();

                            Debug.Log("is lamp");
                            copyPasteText.text = "Pasted values from " + copiedDevice._name + " to " + ((Lamp)trackedDevice)._name;
                            break;
                        default:
                            Debug.Log("unknown");
                            break;
                    }
                }
            }
        }
        Invoke("ClearCopyPasteText", copyPasteTextDisplayTimer);
    }

    void ClearCopyPasteText()
    {
        copyPasteText.text = "";
    }
}
