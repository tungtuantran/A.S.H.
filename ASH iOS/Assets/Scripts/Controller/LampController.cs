﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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
            SetLightBrightness(1 - brightnessCalculator.distance * 100);        // example: 0.0035 -> 0.35 (für farbsättigung wo 0-100%: *10000)
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

    public void UpdateLightColor()
    {
        updateLightColor = true;
        colorPicker.Active = true;
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
        if(brightness < 0.15)
        {
            brightness = 0.15f;
        }
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