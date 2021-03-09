using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LampController : DeviceController
{

    [SerializeField]
    private DistanceCalculator brightnessCalculator;

    [SerializeField]
    private ColorPicker colorPicker;

    [SerializeField]
    private ColorPicker temperaturePicker;

    private bool updateColor;
    private bool updateBrightness;
    private bool updateTemperature;

    void Start()
    {
    }

    void Update()
    {
        if (updateColor)
        {
            SetLightColor(colorPicker.selectedColor);
        }
        if(updateBrightness){
            SetLightBrightness(1 - brightnessCalculator.distance * 100);        // example: 0.0035 -> 0.35 (für farbsättigung wo 0-100%: *10000)
        }
        if (updateTemperature)
        {
            SetLightTemperature(temperaturePicker.selectedColor);

        }
    }

    public override void StopUpdating()
    {
        updateColor = false;
        updateBrightness = false;
        updateTemperature = false;

        brightnessCalculator.active = false;
        colorPicker.active = false;
        temperaturePicker.active = false;
    }

    public void UpdateBrightness()
    {
        updateBrightness = true;

        brightnessCalculator.active = true;
    }

    public void UpdateTemperature()
    {
        updateTemperature = true;

        temperaturePicker.active = true;
    }

    public void UpdateColorAndIntensity()
    {
        updateColor = true;
        updateBrightness = true;

        colorPicker.active = true;
        brightnessCalculator.active = true;
    }

    private void SetLightColor(Color color)
    {
        ((Lamp) selectedDevice).lightColor = color;
    }

    private void SetLightTemperature(Color temperatureColor)
    {
        ((Lamp) selectedDevice).lightTemperature = temperatureColor;
    }

    private void SetLightBrightness(float brightness)
    {
        if(brightness < 0.15)
        {
            brightness = 0.15f;
        }
        ((Lamp)selectedDevice).lightBrightness = brightness;
    }

    public override void AddCurrentlyTrackedDevice(string name)
    {
        Device deviceToAdd = new Lamp(ImageTracking.deviceName, ImageTracking.deviceId, name);
        DeviceCollection.DeviceCollectionInstance.AddRegisteredDevice(deviceToAdd);
    }
}