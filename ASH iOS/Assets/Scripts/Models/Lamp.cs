using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : Device
{
    public Color lightColor { get; set; } = Color.white;
    public float lightBrightness { get; set; } = 1.0f;              // 0.15 - 1.00
    public float lightTemperature { get; set; } = 4000.0f;          // 2700k - 6500k -> TODO ????

    private bool _isTimerSet = false;
    private string _timerStart = "18:00";                           // example: 09:01
    private string _timerStop = "00:00";
    public bool[] timerDaysOfWeek { get; set; } = new bool[7];      // default: all false; [0]= Monday, [1] = Tuesday, ...


    private DateTime timerStartTime;
    private DateTime timerStopTime;

   
    public bool isTimerSet
    {
        get
        {
            return _isTimerSet;
        }

        set
        {
            _isTimerSet = value;

            while (isTimerSet)
            {
                DateTime currentTime = DateTime.Now;

                if (CheckTimerIfIsSetForDay((int)currentTime.DayOfWeek))
                {
                    if (currentTime.Equals(timerStartTime))
                    {
                        isOn = true;
                    }

                    if (currentTime.Equals(timerStopTime))
                    {
                        isOn = false;
                    }
                }
            }
        }
    }

    public string timerStart
    {
        get {
            return _timerStart;
        }

        set
        {
            _timerStart = value;
            timerStartTime = DateTime.Parse(timerStart + ":00", System.Globalization.CultureInfo.CurrentCulture);
        }
    }

    public string timerStop
    {
        get
        {
            return _timerStop;
        }

        set
        {
            _timerStop = value;
            timerStopTime = DateTime.Parse(timerStart + ":00", System.Globalization.CultureInfo.CurrentCulture);
        }
    }
   

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

    private bool CheckTimerIfIsSetForDay(int day)
    {
        if (timerDaysOfWeek[day - 1])
        {
            return true;
        }

        return false;
    }

}