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
        colorPicker.SetNewColor(((Lamp) selectedDevice).lightColor);
        temperatureSlider.value = ((Lamp) selectedDevice).lightTemperature;
        brightnessSlider.value = ((Lamp)selectedDevice).lightBrightness;

    }
    void Update()
    {
        SetLightColor(colorPicker.TheColor);
        SetLightTemperature(temperatureSlider.value);
        SetLightBrightness(brightnessSlider.value);

        colorPicker.SetNewColor(((Lamp) selectedDevice).lightColor);
        temperatureSlider.value = ((Lamp) selectedDevice).lightTemperature;
        brightnessSlider.value = ((Lamp)selectedDevice).lightBrightness;
    }

    private void SetLightColor(Color color)
    {
        ((Lamp) selectedDevice).lightColor = color;
    }

    private void SetLightTemperature(float temperatureValue)
    {
        ((Lamp) selectedDevice).lightTemperature = temperatureValue;
    }

    private void SetLightBrightness(float brightnessValue)
    {
        ((Lamp)selectedDevice).lightBrightness = brightnessValue;
    }
}