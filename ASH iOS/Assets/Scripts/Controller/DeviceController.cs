using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

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
        if (ImageTracking.deviceId != 0)
        {
            selectedDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(ImageTracking.deviceId);
        }
        else
        {
            throw new NoDeviceException();
        }
    }

    public void EditNameOfSelectedDevice(string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            selectedDevice._name = name;
        }

        // else keep old name
    }

    public abstract void AddCurrentlyTrackedDevice(string name);

    public abstract void StopUpdating();

}
