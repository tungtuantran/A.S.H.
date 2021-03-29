using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IDeviceView
{

    InputField addNameInputField
    {
        get;
        set;
    }

    InputField editNameInputField
    {
        get;
        set;
    }

    Device trackedDevice
    {
        get;
        set;
    }


    void OnDeviceAdded(string deviceName);
    void OnEditDeviceName();
    void OnDeviceRemoved();
}
