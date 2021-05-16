using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 * saveDeviceCollection() if device gets added, removed or updated
 * loadDeviceCollection() if app gets (re)started
 */
public class DeviceCollection
{
    private static readonly DeviceCollection deviceCollectionInstance = new DeviceCollection();     // Singleton

    public List<IDevice> RegisteredDevices { get; set; } = new List<IDevice>();
    public bool AllDevicesOff { get; set; }

    static DeviceCollection()
    {
    }

    private DeviceCollection()
    {
        // loads device collection data when device collection singlton object gets initialized
        LoadDeviceCollection();
    }

    public static DeviceCollection DeviceCollectionInstance{
        get
        {
            return deviceCollectionInstance;
        }
    }

    public IDevice GetRegisteredDeviceByDeviceId(int deviceId)
    {
        foreach(IDevice device in RegisteredDevices){
            if(device.Id == deviceId)
            {
                return device;
            }
        }
        return null;
    }

    public void AddRegisteredDevice(IDevice device)
    {
        if (device != null)
        {
            RegisteredDevices.Add(device);
            SaveDeviceCollection();
        }
        else
        {
            throw new NoDeviceException();
        }
    }

    public void RemoveRegisteredDevice(IDevice device)
    {
        if (device != null)
        {
            RegisteredDevices.Remove(device);
            SaveDeviceCollection();
        }
        else
        {
            throw new NoDeviceException();
        }
    }

    public void SaveDeviceCollection()                 // has to be called if sth. needs to be updated
    {
        SaveSystem.SaveDeviceCollection(this);
    }

    public void LoadDeviceCollection()                                                 
    {
        RegisteredDevices.Clear();                      // just for safety but it's not even neccesary, because LoadDeviceCollection() gets only called when the app starts or when the registeredDevice array is already empty

        DeviceCollectionData deviceCollectionData = SaveSystem.LoadDeviceCollection();
        if (deviceCollectionData != null)
        {
            if (deviceCollectionData.DeviceDataList != null)
            {
                for (int i = 0; i < deviceCollectionData.DeviceDataList.Length; i++)
                {

                    DeviceData deviceData = (DeviceData)deviceCollectionData.DeviceDataList[i];
                    IDevice device = null;

                    Debug.Log("device type name: " + deviceData.GetType().Name);

                    switch (deviceData.GetType().Name)
                    {
                        case "LampData":
                            device = new Lamp(deviceData.DeviceName, deviceData.Id, deviceData.Name);
                            break;
                        default:
                            Debug.LogError("Unknown Device Data Type");
                            break;
                    }

                    if (device != null)
                    {
                        device.LoadDevice(deviceData);
                        RegisteredDevices.Add(device);
                    }
                }
            }

            AllDevicesOff = deviceCollectionData.AllDevicesOff;
        } 
    }
}

[Serializable]
public class NoDeviceException : Exception
{
    public NoDeviceException()
    {
    }

    public NoDeviceException(string message) : base(message)
    {
    }
}