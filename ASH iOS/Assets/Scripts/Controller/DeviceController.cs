using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class DeviceController : MonoBehaviour
{
    // Model
    protected Device device;

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

    // View
    protected IDeviceView view;

    public IDeviceView View
    {
        get
        {
            return view;
        }

        set
        {
            view = value;

            if(view != null)
            {
                view.trackedDevice = device;
            }
        }
    }

    [SerializeField]
    private DeviceView deviceView;

    protected virtual void Awake()
    {
        int deviceId = ImageTracking.deviceId;
        string deviceName = ImageTracking.deviceName;

        // set device model
        device = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(deviceId);

        if(device == null)
        {
            SetDevice(deviceName, deviceId);
        }

        // set device view
        View = deviceView;
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

    public void AddDevice()
    {
        string name = view.addNameInputField.text;

        if (!string.IsNullOrWhiteSpace(name))
        {
            device.Name = name;
            DeviceCollection.DeviceCollectionInstance.AddRegisteredDevice(device);

            view.OnDeviceAdded(name);
        }
        else
        {
            throw new NoInputException();
        }
    }

    public abstract void StopUpdating();

    protected abstract void SetDevice(string deviceName, int deviceId);

}
