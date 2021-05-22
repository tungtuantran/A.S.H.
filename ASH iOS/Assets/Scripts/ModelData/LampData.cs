using System;

/*
 * Data of Lamp as an inheritance of DeviceData
 */
[Serializable]
public class LampData : DeviceData
{
    public float[] LightColor;
    public float LightBrightness;
    public float[] LightTemperature;

    public LampData(Lamp lamp) : base(lamp)
    {
        LightColor = new float[4];
        LightColor[0] = lamp.LightColor.r;
        LightColor[1] = lamp.LightColor.g;
        LightColor[2] = lamp.LightColor.b;
        LightColor[3] = lamp.LightColor.a;

        LightBrightness = lamp.LightBrightness;

        LightTemperature = new float[4];
        LightTemperature[0] = lamp.LightTemperature.r;
        LightTemperature[1] = lamp.LightTemperature.g;
        LightTemperature[2] = lamp.LightTemperature.b;
        LightTemperature[3] = lamp.LightTemperature.a;
    }
}
