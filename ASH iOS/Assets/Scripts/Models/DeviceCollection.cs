using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 * saveDeviceCollection() if device gets added, removed or updated
 * loadDeviceCollection() if app gets (re)started
 * 
 *
 */
public class DeviceCollection
{
    private static readonly DeviceCollection deviceCollectionInstance = new DeviceCollection();         //Singleton pattern

    public List<Device> registeredDevices { get; set; } = new List<Device>();
    public bool allDevicesOff { get; set; }

    static DeviceCollection()
    {
    }

    private DeviceCollection()
    {
        Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");                        //neccesary for serialization on iOS devices
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
        foreach(Device device in registeredDevices){
            if(device.id == deviceId)
            {
                return device;
            }
        }
        return null;
    }

    public void AddRegisteredDevice(Device device)
    {
        this.registeredDevices.Add(device);
        this.SaveDeviceCollection();
    }

    public void RemoveRegisteredDevice(Device device)
    {
        this.registeredDevices.Remove(device);
        this.SaveDeviceCollection();
    }

    public void SaveDeviceCollection()                 //has to be called if sth needs to be updated
    {
        SaveSystem.SaveDeviceCollection(this);
    }

    public void LoadDeviceCollection()                                                 
    {
        registeredDevices.Clear();                      //TODO: even neccesary if loadDeviceCollection gets only called when app starts?

        DeviceCollectionData deviceCollectionData = SaveSystem.LoadDeviceCollection();
        if (deviceCollectionData != null)
        {
            for (int i = 0; i < deviceCollectionData.deviceDatas.Length; i++)
            {

                IDeviceData deviceData = deviceCollectionData.deviceDatas[i];
                Device device = null;

                switch (deviceData.GetType().Name)
                {
                    case "LampData":
                        device = new Lamp(deviceData.deviceName, deviceData.id, deviceData._name);
                        break;
                    default:
                        Debug.LogError("Uknown Data Type");
                        break;
                }

                if (device != null)
                {
                    device.LoadDevice(deviceData);
                    registeredDevices.Add(device);
                }
            }

            allDevicesOff = deviceCollectionData.allDevicesOff;
        }
    }
}
