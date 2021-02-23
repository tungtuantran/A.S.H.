using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : Device
{
    public Color lightColor { get; set; } = Color.white;
    public float lightBrightness { get; set; } = 1.0f;           //1 out of 1
    public float lightTemperature { get; set; } = 0.5f;           //min 2700k - max 6500k -> TODO ????

    public bool isTimerSet { get; set; } = false;
    public string timerStart { get; set; } = "";              //example: 09:01
    public string timerStop { get; set; } = "";
    public bool[] timerDaysOfWeek { get; set; } = new bool[7];  //default: all false; [0]= Monday, [1] = Tuesday, ...

    public Lamp(string deviceName, int id, string name): base(deviceName, id, name)
    {
    }

    public override void LoadDevice(IDeviceData deviceData)
    {
        LampData lampData = (LampData) deviceData;
        this.deviceName = lampData.deviceName;
        this.id = lampData.id;
        this._name = lampData._name;
        this.isOn = lampData.isOn;

        this.lightColor = new Color(lampData.lightColor[0], lampData.lightColor[1], lampData.lightColor[2], lampData.lightColor[3]);
        this.lightBrightness = lampData.lightBrightness;
        this.lightTemperature = lampData.lightTemperature;

        this.isTimerSet = lampData.isTimerSet;
        this.timerStart = lampData.timerStart;
        this.timerStop = lampData.timerStop;
        this.timerDaysOfWeek = lampData.timerDaysOfWeek;
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