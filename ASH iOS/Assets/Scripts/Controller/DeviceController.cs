using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class DeviceController : MonoBehaviour
{
    protected Device selectedDevice;
    public Device SelectedDevice
    {
        get
        {
            return selectedDevice;
        }

        set
        {
            selectedDevice = value;
        }
    }

    protected int deviceId;
    public int DeviceId {
        get
        {
            return deviceId;
        }

        set
        {
            deviceId = value;
        }
    }

    protected string deviceName;
    public string DeviceName
    {
        get
        {
            return deviceName;
        }

        set
        {
            deviceName = value;
        }
    }

    private void Awake()
    {
        deviceId = ImageTracking.deviceId;
        deviceName = ImageTracking.deviceName;

        selectedDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(deviceId);

        if(selectedDevice == null)
        {
            SetSelectedDevice();
        }
    }

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

    public void EditNameOfSelectedDevice(string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            selectedDevice._name = name;
        }

        // else keep old name
    }

    public abstract void AddDevice(string name);

    public abstract void StopUpdating();

    protected abstract void SetSelectedDevice();

}
