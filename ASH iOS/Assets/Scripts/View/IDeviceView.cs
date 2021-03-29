using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDeviceView
{

    void OnDeviceAdded(string deviceName);
    void OnEditDeviceName();
    void OnDeviceRemoved();
}
