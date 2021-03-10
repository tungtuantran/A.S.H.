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

    private bool updateLightColor;
    private bool updateLightBrightness;
    private bool updateLightTemperature;

    void Update()
    {
        if (updateLightColor)
        {
            SetLightColor(colorPicker.selectedColor);
        }
        if(updateLightBrightness){
            SetLightBrightness(1 - brightnessCalculator.Distance * 100);        // example: 0.0035 -> 0.35 (für farbsättigung wo 0-100%: *10000)
        }
        if (updateLightTemperature)
        {
            SetLightTemperature(temperaturePicker.selectedColor);

        }
    }

    public override void StopUpdating()
    {
        updateLightColor = false;
        updateLightBrightness = false;
        updateLightTemperature = false;

        brightnessCalculator.Active = false;
        colorPicker.Active = false;
        temperaturePicker.Active = false;
    }

    public void UpdateLigthBrightness()
    {
        updateLightBrightness = true;
        brightnessCalculator.Active = true;
    }

    public void UpdateLightTemperature()
    {
        updateLightTemperature = true;
        temperaturePicker.Active = true;
    }

    public void UpdateLightColorAndBrightness()
    {
        updateLightColor = true;
        colorPicker.Active = true;

        UpdateLigthBrightness();
    }

    private void SetLightColor(Color color)
    {
        ((Lamp) SelectedDevice).lightColor = color;
    }

    private void SetLightTemperature(Color temperatureColor)
    {
        ((Lamp) SelectedDevice).lightTemperature = temperatureColor;
    }

    private void SetLightBrightness(float brightness)
    {
        if(brightness < 0.15)
        {
            brightness = 0.15f;
        }
        ((Lamp) SelectedDevice).lightBrightness = brightness;
    }

    public override void AddCurrentlyTrackedDevice(string name)
    {
        Device deviceToAdd = new Lamp(ImageTracking.deviceName, ImageTracking.deviceId, name);
        DeviceCollection.DeviceCollectionInstance.AddRegisteredDevice(deviceToAdd);
    }
}