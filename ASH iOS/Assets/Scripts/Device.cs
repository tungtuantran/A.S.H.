using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Device: MonoBehaviour
{
    public string _name { get; set; }
    public int id { get; set; }
    public bool isOn { get; set; } = false;

    public Device(string name, int id)
    {
        this._name = name;
        this.id = id;
    }

    public void SaveDevice()
    {
        SaveSystem.SaveDevice(this);
    }

    public void LoadDevice()
    {
        IDeviceData deviceData = SaveSystem.LoadDevice(this.GetType().Name);
        SetLoadedDeviceData(deviceData);
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