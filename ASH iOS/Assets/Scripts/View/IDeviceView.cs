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

    void OnDeviceAdded(string deviceName);
    void OnDeviceRemoved();
    void OnUpdateIsOn(bool isOn);
    void OnUpdateName(string name);
    void OnRegisteredDevice(bool registered);
}
