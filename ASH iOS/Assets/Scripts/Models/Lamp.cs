using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : Device
{
    public Color lightColor { get; set; } = Color.white;
    public float lightBrightness { get; set; } = 1.0f;              // 1 out of 1
    public float lightTemperature { get; set; } = 4000.0f;          // min 2700k - max 6500k -> TODO ????

    public bool isTimerSet { get; set; } = false;
    public string timerStart { get; set; } = "";                    // example: 09:01
    public string timerStop { get; set; } = "";
    public bool[] timerDaysOfWeek { get; set; } = new bool[7];      // default: all false; [0]= Monday, [1] = Tuesday, ...

    public Lamp(string deviceName, int id, string name): base(deviceName, id, name)
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
        lightTemperature = lampData.lightTemperature;

        isTimerSet = lampData.isTimerSet;
        timerStart = lampData.timerStart;
        timerStop = lampData.timerStop;
        timerDaysOfWeek = lampData.timerDaysOfWeek;
    }

    public override string ToString()
    {
        return base.ToString()
            + ", Color: " + lightColor.ToString()
            + ", Light Brightness: " + lightBrightness.ToString()
            + ", Light Temperature: " + lightTemperature.ToString()
            + ", is Timer Set: " + isTimerSet.ToString()
            + ", Timer Start: " + timerStart.ToString()
            + ", Timer Stop: " + timerStop.ToString()
            + ", Timer Days: " + timerDaysOfWeek.ToString();
    }
}