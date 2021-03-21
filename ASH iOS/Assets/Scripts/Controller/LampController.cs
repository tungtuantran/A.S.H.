using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LampController : DeviceController
{

    
    [SerializeField]
    private DistanceCalculator brightnessCalculator;

    [SerializeField]
    private DistanceCalculator colorCalculator;

    [SerializeField]
    private ColorPicker temperaturePicker;

    private bool updateLightColor;
    private bool updateLightBrightness;
    private bool updateLightTemperature;

    void Update()
    {
        if (updateLightColor)
        {
            Color color = ConvertDistanceToColorValue(colorCalculator.sidewardDistance, colorCalculator.upwardDistance);
            SetLightColor(color);
        }
        if(updateLightBrightness){
            float brightness = ConvertDistanceToBrightnessValue(brightnessCalculator.forwardDistance);
            SetLightBrightness(brightness);
        }
        if (updateLightTemperature)
        {
            SetLightTemperature(temperaturePicker.selectedColor);

        }
    }

    private float ConvertDistanceToBrightnessValue(float distanceForBrightness)
    {
        float brightness = 1 - distanceForBrightness * 100;                         // example: 0.0035 -> 0.35 (für farbsättigung wo 0-100%: *10000)

        if (brightness < 0.15)
        {
            brightness = 0.15f;
        }

        return brightness;
    }

    private Color ConvertDistanceToColorValue(float distanceForHue, float distanceForSaturation)
    {
        // hue h and saturation s from hsv
        float h = distanceForHue * 100;
        float s = 1 - distanceForSaturation * 100;

        if (s < 0f)
        {
            s = 0f;
        }

        // set color
        return Color.HSVToRGB(h, s, 1f);
    }

    public override void StopUpdating()
    {
        updateLightColor = false;
        updateLightBrightness = false;
        updateLightTemperature = false;

        brightnessCalculator.Active = false;
        colorCalculator.Active = false;        
        temperaturePicker.Active = false;
    }

    public void UpdateLightColor()
    {
        updateLightColor = true;
        colorCalculator.Active = true;
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
        UpdateLightColor();
        UpdateLigthBrightness();
    }

    private void SetLightColor(Color color)
    {
        ((Lamp) device).LightColor = color;
    }

    private void SetLightTemperature(Color temperatureColor)
    {
        ((Lamp) device).LightTemperature = temperatureColor;
    }

    private void SetLightBrightness(float brightness)
    {
        ((Lamp) device).LightBrightness = brightness;
    }

    public override void AddDevice(string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            device.Name = name;
            DeviceCollection.DeviceCollectionInstance.AddRegisteredDevice(device);
        }
        else
        {
            throw new NoInputException();
        }
    }

    protected override void SetDevice()
    {
        device = new Lamp(deviceName, deviceId);
    }
}

[Serializable]
public class NoInputException : Exception
{
    public NoInputException()
    {
    }

    public NoInputException(string message) : base(message)
    {
    }
}