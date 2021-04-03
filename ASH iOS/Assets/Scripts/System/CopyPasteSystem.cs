using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyPasteSystem : MonoBehaviour
{
    public static Device copiedDevice;
    public static bool active = true;

    [SerializeField]
    private FollowMousePosition swipeNotification;

    [SerializeField]
    private Text copiedPastedSwipeText;

    void Awake()
    {
        swipeNotification.gameObject.SetActive(false);
    }

    void Update()
    {
        SetSwipeNotificationActive();
    }

    public void Copy()
    {
        if(active)
        {
            if (SelectDevice.SelectedDevice != null)
            {
                copiedDevice = SelectDevice.SelectedDevice;

                copiedPastedSwipeText.text = "Copied " + copiedDevice.Name + "!";
                //Handheld.Vibrate();
            }
        }

        active = true;
    }

    public void Paste()
    {
        if (active)
        {
            if (SelectDevice.SelectedDevice != null)
            {
                Device deviceToPasteIn = SelectDevice.SelectedDevice;

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
                            //Handheld.Vibrate();

                            copiedPastedSwipeText.text = "Pasted in " + copiedDevice.Name + "!";
                            break;
                        //is unknown
                        default:
                            break;
                    }
                }
                else
                {
                    copiedPastedSwipeText.text = "Failed to paste. Copied " + copiedDevice.DeviceName + "values are not convertable into" + deviceToPasteIn.DeviceName + " values.";
                }
            }
        }

        active = true;
    }

    public void SetSwipeNotificationActive()
    {
        if (SelectDevice.SelectedDevice != null)
        {
            // if its not already active
            if (!swipeNotification.gameObject.activeSelf)
            {
                swipeNotification.SetPositionByMousePosition();
                swipeNotification.gameObject.SetActive(true);
            }
        }
        else
        {
            swipeNotification.gameObject.SetActive(false);
            copiedPastedSwipeText.text = "";
        }
    }
}
