using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDeviceData
{
    string DeviceName { get; set; }
    int Id { get; set; }
    string Name { get; set; }
    bool IsOn { get; set; }
}
