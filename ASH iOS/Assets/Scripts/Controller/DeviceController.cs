using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class DeviceController : MonoBehaviour
{
    public Device selectedDevice { get; set; }

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

    public void RemoveSelectedDevice()
    {
        DeviceCollection.DeviceCollectionInstance.RemoveRegisteredDevice(selectedDevice);
    }

    public void SelectDeviceByCurrentlyTrackedDevice()
    {
        selectedDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(ImageTracking.deviceId);
    }

    public abstract void AddCurrentlyTrackedDevice(string name);

    public abstract void StopUpdating();

}
