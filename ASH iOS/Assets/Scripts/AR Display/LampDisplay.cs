using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LampDisplay : DeviceDisplay
{
    [SerializeField]
    private Light pointLight;

    [SerializeField]
    private Light spotLight;

    protected override void DisplayPropertiesOfTrackedAndRegisteredDevice()
    {
        pointLight.gameObject.SetActive(registeredDevice.IsOn);
        spotLight.gameObject.SetActive(registeredDevice.IsOn);

        Color lightColor = ((Lamp)registeredDevice).LightColor;
        float lightBrightness = ((Lamp)registeredDevice).LightBrightness;
        Color lightTemperature = ((Lamp)registeredDevice).LightTemperature;

        pointLight.color = MixColorWithTemperature(lightColor, lightTemperature);
        pointLight.intensity = ConvertBrightnessToIntesity(lightBrightness);
        spotLight.color = MixColorWithTemperature(lightColor, lightTemperature);
        spotLight.intensity = ConvertBrightnessToIntesity(lightBrightness);
    }

    protected override void DisplayOffState()
    {
        pointLight.gameObject.SetActive(false);
        spotLight.gameObject.SetActive(false);
    }

    private float ConvertBrightnessToIntesity(float brightness)
    {
        return brightness * 2;
    }

    private Color MixColorWithTemperature(Color color, Color temperatureColor)
    {
        float h, s, v;
        Color.RGBToHSV(temperatureColor, out h, out s, out v);

        //the whiter the temperatureColor, the smaller its portion
        float temperatureColorPortion = s;

        // prevents the temperature portion from dominating
        if(temperatureColorPortion > 0.5f)
        {
            temperatureColorPortion = 0.5f;
        }

        float colorPortion = 1 - temperatureColorPortion;
        
        float r, g, b;
        r = color.r * colorPortion + temperatureColor.r * temperatureColorPortion;
        g = color.g * colorPortion + temperatureColor.g * temperatureColorPortion;
        b = color.b * colorPortion + temperatureColor.b * temperatureColorPortion;

        return new Color(r, g, b);
    }
}
