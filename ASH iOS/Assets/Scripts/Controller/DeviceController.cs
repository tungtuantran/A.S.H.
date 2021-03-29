using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class DeviceController : MonoBehaviour
{
    // Model
    protected Device device;

    // View
    [SerializeField]
    protected DeviceView view;

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

    protected virtual void Awake()
    {
        int deviceId = ImageTracking.deviceId;
        string deviceName = ImageTracking.deviceName;

        device = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(deviceId);

        if(device == null)
        {
            SetDevice(deviceName, deviceId);
        }

        view.TrackedDevice = device;
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
        view.OnDeviceRemoved();
    }

    public void EditNameOfDevice()
    {
        string name = view.editNameInputField.text;

        if (!string.IsNullOrWhiteSpace(name))
        {
            device.Name = name;
        }
        // else keep old name

        view.OnEditDeviceName();
    }

    /*
    public abstract void AddDevice(string name);
    */

    public abstract void AddDevice();

    public abstract void StopUpdating();

    protected abstract void SetDevice(string deviceName, int deviceId);

}
