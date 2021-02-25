using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LampController : DeviceController
{

    [SerializeField]
    public ColorPickerTriangle colorPicker;

    [SerializeField]
    public Slider temperatureSlider;


    [SerializeField]
    public Slider brightnessSlider;

    void Start()
    {
        LoadControllerValues();
    }

    void Update()
    {
        SetLightColor(colorPicker.TheColor);
        SetLightTemperature(temperatureSlider.value);
        SetLightBrightness(brightnessSlider.value);

        //LoadControllerValues();                   -> TODO: wieder entkommentieren: problem mit colorPicker muss gefixt werden -> colorpicker.SetNewColor() with immer aufgeraufen und positioniert ständig pointer neu: pointer entfernen?
    }

    protected override void LoadControllerValues()
    {
        //TODO: for ON/OFF switch toggle

        colorPicker.SetNewColor(((Lamp)selectedDevice).lightColor);
        temperatureSlider.value = ((Lamp)selectedDevice).lightTemperature;
        brightnessSlider.value = ((Lamp)selectedDevice).lightBrightness;
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
        ((Lamp)selectedDevice).lightBrightness = brightness;
    }

    public override void AddCurrentlyTrackedDevice(string name)
    {
        Device deviceToAdd = new Lamp(ImageTracking.deviceName, ImageTracking.deviceId, name);
        DeviceCollection.DeviceCollectionInstance.AddRegisteredDevice(deviceToAdd);
    }
}