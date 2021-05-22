using System;

/*
 * Data of Device Model
 */
[Serializable]
public class DeviceData : IDeviceData
{
    public string DeviceName;
    public int Id;
    public string Name;
    public bool IsOn;

    public DeviceData(Device device)
    {
        DeviceName = device.DeviceName;
        Id = device.Id;
        Name = device.Name;
        IsOn = device.IsOn;
    }
}
