using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LampController : DeviceController
{

    /*
    [SerializeField]
    public ColorPickerTriangle colorPicker;

    [SerializeField]
    public Slider temperatureSlider;

    [SerializeField]
    public Slider brightnessSlider;
    */

    [SerializeField]
    public VectorDistanceCalculator brightnessCalculator;

    void Start()
    {
        LoadControllerValues();
    }

    void Update()
    {
        //SetLightColor(colorPicker.TheColor);
        //SetLightTemperature(temperatureSlider.value);

        SetLightBrightness(brightnessCalculator.distance * 100);        // example: 0.0035 -> 0.35 (für farbsättigung wo 0-100%: *10000)
        //SetLightBrightness(brightnessSlider.value);
    }

    protected override void LoadControllerValues()
    {
        //TODO: for ON/OFF switch toggle

        //colorPicker.SetNewColor(((Lamp)selectedDevice).lightColor);
        //temperatureSlider.value = ((Lamp)selectedDevice).lightTemperature;

        //brightnessSlider.value = ((Lamp)selectedDevice).lightBrightness;

        //brightnessCalculator.distance = ((Lamp)selectedDevice).lightBrightness;
        return;
    }

    /**
     * Stops all SubControllers
     */
    public override void StopController()
    {
        StopBrightnessSubController();
        //TODO: stop colorController, ...
    }

    public void StartBrightnessSubController()
    {
        brightnessCalculator.StartCalculatingDistance();
    }

    private void StopBrightnessSubController()
    {
        brightnessCalculator.StopCalculatingDistance();
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