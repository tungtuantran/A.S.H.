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
        _light.gameObject.SetActive(registeredDevice.IsOn);

        Color lightColor = ((Lamp)registeredDevice).LightColor;
        float lightBrightness = ((Lamp)registeredDevice).LightBrightness;
        Color lightTemperature = ((Lamp)registeredDevice).LightTemperature;

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

        //the whiter the temperatureColor, the smaller its portion
        float temperatureColorPortion = s;                  
        float colorPortion = 1 - temperatureColorPortion;
        
        float r, g, b;
        r = color.r * colorPortion + temperatureColor.r * temperatureColorPortion;
        g = color.g * colorPortion + temperatureColor.g * temperatureColorPortion;
        b = color.b * colorPortion + temperatureColor.b * temperatureColorPortion;

        return new Color(r, g, b);
    }
}
