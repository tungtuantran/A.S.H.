using UnityEngine;

/*
 * Lamp Model as an inheritance of Device
 */
public class Lamp : Device
{
    public Color LightColor { get; set; } = Color.white;
    public float LightBrightness { get; set; } = 1.0f;              // 0.15 - 1.0
    public Color LightTemperature { get; set; } = Color.white;

    public Lamp(string deviceName, int id, string name): base(deviceName, id, name)
    {
    }

    public Lamp(string deviceName, int id) : base(deviceName, id)
    {
    }

    public override void LoadDevice(DeviceData deviceData)
    {
        LampData lampData = (LampData) deviceData;
        DeviceName = lampData.DeviceName;
        Id = lampData.Id;
        Name = lampData.Name;
        IsOn = lampData.IsOn;

        LightColor = new Color(lampData.LightColor[0], lampData.LightColor[1], lampData.LightColor[2], lampData.LightColor[3]);
        LightBrightness = lampData.LightBrightness;
        LightTemperature = new Color(lampData.LightTemperature[0], lampData.LightTemperature[1], lampData.LightTemperature[2], lampData.LightTemperature[3]);
    }

    public override string DeviceValuesToString()
    {
        string mode;

        if (IsOn)
        {
            mode = "ON";
        }
        else
        {
            mode = "OFF";
        }

        return "Mode: " + mode
            + "\nLight Color: " + LightColor.ToString()
            + "\n Brightness: " + LightBrightness.ToString()
            + "\n Temperature: " + LightTemperature.ToString();
    }

    public override void SetDefaultValues()
    {
        base.SetDefaultValues();

        LightColor = Color.white;
        LightBrightness = 1.0f;
        LightTemperature = Color.white;
    }

    public override string ToString()
    {
        return base.ToString()
            + ", Light Color: " + LightColor.ToString()
            + ", Light Brightness: " + LightBrightness.ToString()
            + ", Light Temperature: " + LightTemperature.ToString();
    }

}