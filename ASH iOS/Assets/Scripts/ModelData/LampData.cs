using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LampData : IDeviceData
{
    public string deviceName { get; set; }
    public int id { get; set; }
    public string _name { get; set; }
    public bool isOn { get; set; } = false;

    public float[] lightColor { get; set; }
    public float lightBrightness { get; set; }               //10 out of 10
    public float[] lightTemperature { get; set; }
    //public float lightTemperature { get; set; }              //5 out of 10

    /*
    public bool isTimerSet { get; set; }
    public string timerStart { get; set; }                 //example: 09:01
    public string timerStop { get; set; }
    public bool[] timerDaysOfWeek { get; set; }
    */

    public LampData(Lamp lamp)
    {
        deviceName = lamp.deviceName;
        id = lamp.id;
        _name = lamp._name;
        isOn = lamp.isOn;

        lightColor = new float[4];
        lightColor[0] = lamp.lightColor.r;
        lightColor[1] = lamp.lightColor.g;
        lightColor[2] = lamp.lightColor.b;
        lightColor[3] = lamp.lightColor.a;

        lightBrightness = lamp.lightBrightness;

        lightTemperature = new float[4];
        lightTemperature[0] = lamp.lightTemperature.r;
        lightTemperature[1] = lamp.lightTemperature.g;
        lightTemperature[2] = lamp.lightTemperature.b;
        lightTemperature[3] = lamp.lightTemperature.a;
        //lightTemperature = lamp.lightTemperature;

        /*
        isTimerSet = lamp.isTimerSet;
        timerStart = lamp.timerStart;
        timerStop = lamp.timerStop;
        timerDaysOfWeek = lamp.timerDaysOfWeek;
        */
    }
}
