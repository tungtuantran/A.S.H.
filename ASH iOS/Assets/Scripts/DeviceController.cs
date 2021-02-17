using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DeviceController : MonoBehaviour
{
    public Device device { get; set; }
    //public Button OnOffButton { get; set; }

    public void SetOnOff()
    {
        if (device.isOn)
        {
            device.isOn = false;
        }
        else
        {
            device.isOn = true;
        }
    }
}
