﻿public interface IDevice
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

    void LoadDevice(DeviceData device);
    string DeviceValuesToString();
    void SetDefaultValues();

}
