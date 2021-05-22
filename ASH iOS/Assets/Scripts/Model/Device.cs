/*
 * Abstract Class for Device Models
 */
public abstract class Device : IDevice
{
    private DeviceCollection deviceCollection = DeviceCollection.DeviceCollectionInstance;

    public string DeviceName { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }    // name chosen by user
    public bool IsOn { get; set; }

    public Device(string deviceName, int id, string name)
    {
        DeviceName = deviceName;
        Id = id;
        Name = name;
    }

    public Device(string deviceName, int id)
    {
        DeviceName = deviceName;
        Id = id;
    }

    public abstract void LoadDevice(DeviceData device);

    public abstract string DeviceValuesToString();

    public virtual void SetDefaultValues()
    {
        Name = "";
        IsOn = false;
    }

    public override string ToString()
    {
        return "DeviceName: " + DeviceName + ", ID: " + Id + " , Name: " + Name + ", isOn: " + IsOn; 
    }
}

public enum DeviceName
{
    standing_lamp1,
    standing_lamp2,
    table_lamp1,
    table_lamp4,
    wall_lamp4
}