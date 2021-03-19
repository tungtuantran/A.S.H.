using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Device
{
    private DeviceCollection deviceCollection = DeviceCollection.DeviceCollectionInstance;

    public string DeviceName { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }                       // name chosen by user
    public bool IsOn { get; set; } = false;

    public Device(string deviceName, int id, string name)
    {
        DeviceName = deviceName;
        Id = id;
        Name = name;
    }

    public Device(string deviceName, int id)
    {
        DeviceName = deviceName;
        Id = id;
    }

    public void addDevice()                                 // add/save Device
    {
        deviceCollection.AddRegisteredDevice(this);
    }

    public void removeDevice()
    {
        deviceCollection.RemoveRegisteredDevice(this);      // save-function is called in removeRegisteredDevice()
    }

    public void UpdateDevice()                              // update/save new device settings
    {
        deviceCollection.SaveDeviceCollection();            // if device get updated -> actually deviceCollection gets updated
    }

    public abstract void LoadDevice(IDeviceData deviceData);

    public override string ToString()
    {
        return "DeviceName: " + DeviceName + ", ID: " + Id + " , Name: " + Name + ", isOn: " + IsOn; 
    }

    public abstract string DeviceValuesToString();
}

public enum DeviceName
{
    standing_lamp1,
    standing_lamp2,
    table_lamp1,
    table_lamp4,
    wall_lamp4
}