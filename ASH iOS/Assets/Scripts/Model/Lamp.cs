using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : Device
{
    public Color lightColor { get; set; } = Color.white;
    public float lightBrightness { get; set; } = 1.0f;              // 0.15 - 1.00
    public Color lightTemperature { get; set; } = Color.white;      //public float lightTemperature { get; set; } = 4000.0f;          // 2700k - 6500k -> TODO ????

    public Lamp(string deviceName, int id, string name): base(deviceName, id, name)
    {
    }

    public Lamp(string deviceName, int id) : base(deviceName, id)
    {
    }

    public override void LoadDevice(IDeviceData deviceData)
    {
        LampData lampData = (LampData) deviceData;
        deviceName = lampData.deviceName;
        id = lampData.id;
        _name = lampData._name;
        isOn = lampData.isOn;

        lightColor = new Color(lampData.lightColor[0], lampData.lightColor[1], lampData.lightColor[2], lampData.lightColor[3]);
        lightBrightness = lampData.lightBrightness;
        lightTemperature = new Color(lampData.lightTemperature[0], lampData.lightTemperature[1], lampData.lightTemperature[2], lampData.lightTemperature[3]);
    }

    public override string ToString()
    {
        return base.ToString()
            + ", Light Color: " + lightColor.ToString()
            + ", Light Brightness: " + lightBrightness.ToString()
            + ", Light Temperature: " + lightTemperature.ToString();
    }

    public override string DeviceValuesToString()
    {
        string mode;

        if (isOn)
        {
            mode = "ON";
        }
        else
        {
            mode = "OFF";
        }

        return "Mode: " + mode
            + "\nLight Color: " + lightColor.ToString()
            + "\n Brightness: " + lightBrightness.ToString()
            + "\n Temperature: " + lightTemperature.ToString();
    }

}