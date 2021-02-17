using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Device: MonoBehaviour
{
    public string deviceName { get; set; }
    public int id { get; set; }
    public string _name { get; set; }                       //name chosen by user
    public bool isOn { get; set; } = false;

    public Device(string deviceName, int id, string name)
    {
        this.deviceName = deviceName;
        this.id = id;
        this._name = name;

        //add device to deviceCollection
    }

    
    public void SaveDevice()            //TODO: save into deviceCollection class instead
    {
        //SaveSystem.SaveDevice(this);
    }

    public void LoadDevice()            //TODO: load from deviceCollection class instead
    {
        //IDeviceData deviceData = SaveSystem.LoadDevice(this.GetType().Name);
        //SetLoadedDeviceData(deviceData);
    }

    public abstract void SetLoadedDeviceData(IDeviceData deviceData);
}

public enum DeviceName
{
    standing_lamp1,
    standing_lamp2,
    table_lamp1,
    table_lamp4,
    wall_lamp4
}