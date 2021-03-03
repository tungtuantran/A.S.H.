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

    public abstract void AddCurrentlyTrackedDevice(string name);

    public void RemoveSelectedDevice()
    {
        DeviceCollection.DeviceCollectionInstance.RemoveRegisteredDevice(selectedDevice);
    }

    public void SelectDeviceByCurrentlyTrackedDevice()
    {
        selectedDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(ImageTracking.deviceId);
    }

    public abstract void StopUpdating();

    public void CopyDeviceValues()
    {
        CopyPasteSystem.copiedDevice = selectedDevice;
        CopyPasteSystem.copying = true;
    }

    public void PasteDeviceValues()
    {
        if (CopyPasteSystem.copying)
        {
            InsertValuesOfDevice(CopyPasteSystem.copiedDevice);
            Debug.Log("inserted");
        }
        Debug.Log("insertion failed");
    }

    protected abstract void InsertValuesOfDevice(Device device);
}
