using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Device
{
    private DeviceCollection deviceCollection = DeviceCollection.DeviceCollectionInstance;

    public string deviceName { get; set; }
    public int id { get; set; }
    public string _name { get; set; }                       // name chosen by user
    public bool isOn { get; set; } = false;

    public Device(string deviceName, int id, string name)
    {
        this.deviceName = deviceName;
        this.id = id;
        this._name = name;
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
        return "DeviceName: " + deviceName + ", ID: " + id + " , Name: " + _name + ", isOn: " + isOn; 
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