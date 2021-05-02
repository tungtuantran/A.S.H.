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
    private static readonly DeviceCollection deviceCollectionInstance = new DeviceCollection();         //Singleton pattern

    public List<Device> RegisteredDevices { get; set; } = new List<Device>();
    public bool AllDevicesOff { get; set; }

    static DeviceCollection()
    {
    }

    private DeviceCollection()
    {
        //Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");                        //neccesary for serialization on iOS devices
        LoadDeviceCollection();
    }

    public static DeviceCollection DeviceCollectionInstance{
        get
        {
            return deviceCollectionInstance;
        }
    }

    public Device GetRegisteredDeviceByDeviceId(int deviceId)
    {
        foreach(Device device in RegisteredDevices){
            if(device.Id == deviceId)
            {
                return device;
            }
        }
        return null;
    }

    public void AddRegisteredDevice(Device device)
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

    public void RemoveRegisteredDevice(Device device)
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

    public void SaveDeviceCollection()                 //has to be called if sth needs to be updated
    {
        SaveSystem.SaveDeviceCollection(this);
    }

    public void LoadDeviceCollection()                                                 
    {
        RegisteredDevices.Clear();                      //TODO: even neccesary if loadDeviceCollection gets only called when app starts?

        DeviceCollectionData deviceCollectionData = SaveSystem.LoadDeviceCollection();
        if (deviceCollectionData != null)
        {
            for (int i = 0; i < deviceCollectionData.DeviceDataList.Length; i++)
            {

                IDeviceData deviceData = deviceCollectionData.DeviceDataList[i];
                Device device = null;

                switch (deviceData.GetType().Name)
                {
                    case "LampData":
                        device = new Lamp(deviceData.DeviceName, deviceData.Id, deviceData.Name);
                        break;
                    default:
                        Debug.LogError("Uknown Data Type");
                        break;
                }

                if (device != null)
                {
                    device.LoadDevice(deviceData);
                    RegisteredDevices.Add(device);
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