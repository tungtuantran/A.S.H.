using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class DeviceController : MonoBehaviour
{
    public Device SelectedDevice { get; set; }

    public void SetSelectedDeviceOnOff()
    {
        if (SelectedDevice.isOn)
        {
            SelectedDevice.isOn = false;
        }
        else
        {
            SelectedDevice.isOn = true;
        }
    }

    public void RemoveSelectedDevice()
    {
        DeviceCollection.DeviceCollectionInstance.RemoveRegisteredDevice(SelectedDevice);
    }

    public void SelectDeviceByCurrentlyTrackedDevice()
    {
        SelectedDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(ImageTracking.deviceId);
    }

    public void EditNameOfSelectedDevice(string name)
    {
        SelectedDevice._name = name;
    }

    public abstract void AddCurrentlyTrackedDevice(string name);

    public abstract void StopUpdating();

}
