using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LampDisplay : DeviceDisplay
{
    [SerializeField]
    private Light _light;

    protected override void DisplayPropertiesOfTrackedAndRegisteredDevice()
    {
        _light.gameObject.SetActive(trackedAndRegisteredDevice.isOn);

        Color lightColor = ((Lamp)trackedAndRegisteredDevice).lightColor;
        float lightBrightness = ((Lamp)trackedAndRegisteredDevice).lightBrightness;
        Color lightTemperature = ((Lamp)trackedAndRegisteredDevice).lightTemperature;
        //float lightTemperature = ((Lamp)trackedAndRegisteredDevice).lightTemperature;

        _light.color = MixColorWithTemperature(lightColor, lightTemperature);
        _light.intensity = lightBrightness;
        
    }

    protected override void DisplayOffState()
    {
        _light.gameObject.SetActive(false);
    }

    private Color MixColorWithTemperature(Color color, Color temperatureColor)
    {
        float h, s, v;
        Color.RGBToHSV(temperatureColor, out h, out s, out v);

        float temperatureColorPortion = s;                                          //the whiter the temperatureColor is, the smaller its portion
        float colorPortion = 1 - temperatureColorPortion;
        
        float r, g, b;
        r = color.r * colorPortion + temperatureColor.r * temperatureColorPortion;
        g = color.g * colorPortion + temperatureColor.g * temperatureColorPortion;
        b = color.b * colorPortion + temperatureColor.b * temperatureColorPortion;

        return new Color(r, g, b);
    }
}
