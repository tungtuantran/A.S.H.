using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDeviceData
{
    string deviceName { get; set; }
    int id { get; set; }
    string _name { get; set; }
    bool isOn { get; set; }
}
