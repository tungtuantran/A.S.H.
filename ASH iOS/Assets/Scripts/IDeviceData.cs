using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDeviceData
{
    string _name { get; set; }
    int id { get; set; }
    bool isOn { get; set; }
}
