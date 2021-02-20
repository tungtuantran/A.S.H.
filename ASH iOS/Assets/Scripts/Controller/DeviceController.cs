using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DeviceController : MonoBehaviour
{
    public Device selectedDevice { get; set; }          //device that's selected by menu
    public Device trackedDevice { get; set; }           //deevice that is (selected) by tracking

    //public Button OnOffButton { get; set; }

    public void SetSelectedDeviceOnOff()
    {
        if (selectedDevice.isOn)
        {
            selectedDevice.isOn = false;
        }
        else
        {
            selectedDevice.isOn = true;
        }
    }
}
