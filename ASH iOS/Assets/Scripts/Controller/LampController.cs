using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LampController : DeviceController
{
    public Text lightTextPreview;
    public Image lightImagePreview;
    
    [SerializeField]
    private DistanceCalculator brightnessCalculator;

    [SerializeField]
    private DistanceCalculator colorCalculator;

    [SerializeField]
    private DistanceCalculator temperatureCalculator;

    [SerializeField]
    private Texture2D temperatureTexture;

    private bool updateLightBrightness;
    private bool updateLightColor;
    private bool updateLightTemperature;

    protected override void Awake()
    {
        base.Awake();

        // preview hidden on default
        HideLightPreview();
    }

    void Update()
    {
        float brightness = 0f;
        Color color = Color.white;
        Color temperatureColor = Color.white;

        if(updateLightBrightness){
            brightness = ConvertDistanceToBrightnessValue(brightnessCalculator.forwardDistance);
            SetLightBrightness(brightness);
        }

        if (updateLightColor)
        {
            color = ConvertDistanceToColorValue(colorCalculator.sidewardDistance, colorCalculator.upwardDistance);
            SetLightColor(color);
        }

        if (updateLightTemperature)
        {
            temperatureColor = ConvertDistanceToTemperatureColorValue(temperatureCalculator.upwardDistance);
            SetLightTemperature(temperatureColor);
        }

        ShowLightPreview(brightness, color, temperatureColor);
    }

    private float ConvertDistanceToBrightnessValue(float distanceForBrightness)
    {
        float brightness = 1 - distanceForBrightness * 100;                         // example: 0.0035 -> 0.35 (für farbsättigung wo 0-100%: *10000)

        if (brightness < 0.15f)
        {
            brightness = 0.15f;
        }

        if(brightness > 1f)
        {
            brightness = 1f;
        }

        return brightness;
    }

    private Color ConvertDistanceToColorValue(float distanceForHue, float distanceForSaturation)
    {
        // hue h and saturation s from hsv
        // Mathf.Abs() to keep hue value positive
        float h = Mathf.Abs(distanceForHue * 100);

        // distance multiplied by 2 for a smaller max distance
        float s = 1 - distanceForSaturation * 2 * 100;

        if(h > 1f)
        {
            h = 1f;
        }

        if (s < 0f)
        {
            s = 0f;
        }

        // set color
        return Color.HSVToRGB(h, s, 1f);
    }

    private Color ConvertDistanceToTemperatureColorValue(float distanceForTemperature)
    {
        int delta = Mathf.RoundToInt(distanceForTemperature * 100000);

        // starts from the middle height of the Texture
        int texY = temperatureTexture.height/2 + delta;

        if(texY > temperatureTexture.height)
        {
            texY = temperatureTexture.height;
        }

        if(texY < 0)
        {
            texY = 0;
        }

        // get color from textures pixel
        return temperatureTexture.GetPixel(0, texY);


    }

    private void ShowLightPreview(float brightness, Color color, Color temperatureColor)
    {

        if (updateLightBrightness && updateLightColor)
        {
            lightTextPreview.text = Convert.ToInt32(brightness * 100).ToString() + "%";
            lightImagePreview.color = new Color(color.r, color.g, color.b, brightness);

            lightTextPreview.gameObject.SetActive(true);
            lightImagePreview.gameObject.SetActive(true);
        }
        else
        {
            if (updateLightBrightness)
            {
                lightTextPreview.text = Convert.ToInt32(brightness * 100).ToString() + "%";
                lightImagePreview.color = new Color(1f, 1f, 1f, brightness);

                lightTextPreview.gameObject.SetActive(true);
                lightImagePreview.gameObject.SetActive(true);
            }
            if (updateLightColor)
            {
                lightImagePreview.color = new Color(color.r, color.g, color.b, 1f);

                lightImagePreview.gameObject.SetActive(true);
            }

            if (updateLightTemperature)
            {
                lightImagePreview.color = new Color(temperatureColor.r, temperatureColor.g, temperatureColor.b, 1f);

                lightImagePreview.gameObject.SetActive(true);
            }
        }
    }

    private void HideLightPreview()
    {
        lightTextPreview.gameObject.SetActive(false);
        lightImagePreview.gameObject.SetActive(false);
    }

    public override void StopUpdating()
    {
        updateLightColor = false;
        updateLightBrightness = false;
        updateLightTemperature = false;

        brightnessCalculator.Active = false;
        colorCalculator.Active = false;
        temperatureCalculator.Active = false;

        HideLightPreview();
    }

    public void UpdateLightColor()
    {
        updateLightColor = true;
        colorCalculator.Active = true;

        lightImagePreview.gameObject.SetActive(true);
    }

    public void UpdateLigthBrightness()
    {
        updateLightBrightness = true;
        brightnessCalculator.Active = true;
    }

    public void UpdateLightTemperature()
    {
        updateLightTemperature = true;
        temperatureCalculator.Active = true;
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