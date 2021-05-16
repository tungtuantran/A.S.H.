using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyPasteSystem : MonoBehaviour
{
    public static bool active = true;

    [SerializeField]
    private FollowMousePosition swipeNotification;

    [SerializeField]
    private Text copiedPastedSwipeText;

    private IDevice copiedDevice;

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
            if (SelectDevice.DevicePresenterOfSelectedDevice != null)
            {
                copiedDevice = SelectDevice.DevicePresenterOfSelectedDevice.Device;

                copiedPastedSwipeText.text = "Copied " + copiedDevice.Name + "!";
            }
        }

        active = true;
    }

    public void Paste()
    {
        if (active)
        {
            if (SelectDevice.DevicePresenterOfSelectedDevice != null)
            {
                IDevice deviceToPasteIn = SelectDevice.DevicePresenterOfSelectedDevice.Device;

                // same type name
                if (copiedDevice.GetType().Name.Equals(deviceToPasteIn.GetType().Name))
                {
                    SelectDevice.DevicePresenterOfSelectedDevice.InsertCopiedValuesToDevice(copiedDevice);
                    SelectDevice.DevicePresenterOfSelectedDevice.ShowView();

                    copiedPastedSwipeText.text = "Pasted in " + copiedDevice.Name + "!";
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
        if (SelectDevice.DevicePresenterOfSelectedDevice != null)
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
