using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDevice
{

    string DeviceName
    {
        get;
        set;
    }

    int Id
    {
        get;
        set;
    }

    string Name
    {
        get;
        set;
    }

    bool IsOn
    {
        get;
        set;
    }

    void LoadDevice(IDeviceData device);
    string DeviceValuesToString();
    void SetDefaultValues();

}
