using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class DeviceController : MonoBehaviour
{
    protected Device device;
    protected int deviceId;
    protected string deviceName;

    public Device Device
    {
        get
        {
            return device;
        }

        set
        {
            device = value;
        }
    }

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

        device = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(deviceId);

        if(device == null)
        {
            SetDevice();
        }
    }

    public void SetDeviceOnOff()
    {
        if (device.IsOn)
        {
            device.IsOn = false;
        }
        else
        {
            device.IsOn = true;
        }
    }

    public void RemoveDevice()
    {
        DeviceCollection.DeviceCollectionInstance.RemoveRegisteredDevice(device);
    }

    public void EditNameOfDevice(string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            device.Name = name;
        }

        // else keep old name
    }

    public abstract void AddDevice(string name);

    public abstract void StopUpdating();

    protected abstract void SetDevice();

}
