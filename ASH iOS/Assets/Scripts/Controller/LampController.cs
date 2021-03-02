using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LampController : DeviceController
{

    [SerializeField]
    public DistanceCalculator brightnessCalculator;

    [SerializeField]
    public ColorPicker colorPicker;

    void Start()
    {
    }

    void Update()
    {
        SetLightBrightness(1 - brightnessCalculator.distance * 100);        // example: 0.0035 -> 0.35 (für farbsättigung wo 0-100%: *10000)
        SetLightColor(colorPicker.selectedColor);
        //TODO: set temperature
    }

    /**
     * Stops all SubControllers
     */
    public override void StopController()
    {
        brightnessCalculator.active = false;
        colorPicker.active = false;
    }

    public void StartBrightnessSubController()
    {
        brightnessCalculator.active = true;
    }

    public void StartColorAndIntensitySubController()
    {
        colorPicker.active = true;
        brightnessCalculator.active = true;
    }

    public override void AddCurrentlyTrackedDevice(string name)
    {
        Device deviceToAdd = new Lamp(ImageTracking.deviceName, ImageTracking.deviceId, name);
        DeviceCollection.DeviceCollectionInstance.AddRegisteredDevice(deviceToAdd);
    }

    private void SetLightColor(Color color)
    {
        ((Lamp) selectedDevice).lightColor = color;
    }

    private void SetLightTemperature(float temperature)
    {
        ((Lamp) selectedDevice).lightTemperature = temperature;
    }

    private void SetLightBrightness(float brightness)
    {
        if(brightness < 0.15)
        {
            brightness = 0.15f;
        }
        ((Lamp)selectedDevice).lightBrightness = brightness;
    }
}