using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPasteSystem : MonoBehaviour
{

    public static Device copiedDevice;
    public static bool copying;

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    private void Update()
    {
        Swipe();
        /*
        if (Input.GetMouseButtonUp(0))
        {
            copying = false;
        }
        Debug.Log("copying: " + copying + "; copiedDevice: " + copiedDevice.ToString());
        */
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
                if(DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(ImageTracking.deviceId) != null)
                {
                    copiedDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(ImageTracking.deviceId);
                    Debug.Log("copied device: " + copiedDevice.ToString());
                }
                Debug.Log("up swipe");
            }

            //swipe down
            if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
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
                                Debug.Log("is lamp");
                                break;
                            default:
                                Debug.Log("unknown");
                                break;
                        }
                    }
                }    
                Debug.Log("down swipe");
            }
            //swipe left
            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                Debug.Log("left swipe");
            }
            //swipe right
            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                Debug.Log("right swipe");
            }
        }
    }

    
    /*
    public void Swipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }

            if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                //swipe upwards
                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    if (DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(ImageTracking.deviceId) != null)
                    {
                        copiedDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(ImageTracking.deviceId);
                        Debug.Log("copied device: " + copiedDevice.ToString());
                    }

                    Debug.Log("up swipe");
                }
                //swipe down
                if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
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
                                    Debug.Log("is lamp");
                                    break;
                                default:
                                    Debug.Log("unknown");
                                    break;
                            }
                        }
                    }

                    Debug.Log("down swipe");
                }
                //swipe left
                if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    Debug.Log("left swipe");
                }
                //swipe right
                if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    Debug.Log("right swipe");
                }
            }
        }
    }
    */
}
