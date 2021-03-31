using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyPasteSystem : MonoBehaviour
{
    public static Device copiedDevice;
    public static bool active = true;

    [SerializeField]
    private Text copiedDeviceInformationText;

    void Update()
    {
        UpdateCopyText();
    }

    public void Copy()
    {
        if(active)
        {
            if (SelectDevice.SelectedDevice != null)
            {
                copiedDevice = SelectDevice.SelectedDevice;
                Handheld.Vibrate();
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
                            Handheld.Vibrate();
                            break;
                        //is unknown
                        default:
                            break;
                    }
                }
            }
        }

        active = true;
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
