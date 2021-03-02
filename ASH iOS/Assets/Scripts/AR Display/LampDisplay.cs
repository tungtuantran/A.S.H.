using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LampDisplay : DeviceDisplay
{
    [SerializeField]
    public Light _light;

    protected override void DisplayPropertiesOfTrackedAndRegisteredDevice()
    {
        _light.gameObject.SetActive(trackedAndRegisteredDevice.isOn);

        Color lightColor = ((Lamp)trackedAndRegisteredDevice).lightColor;
        float lightBrightness = ((Lamp)trackedAndRegisteredDevice).lightBrightness;
        float lightTemperature = ((Lamp)trackedAndRegisteredDevice).lightTemperature;

        _light.color = lightColor;
        _light.intensity = lightBrightness;
        _light.colorTemperature = lightTemperature;
    }

}
