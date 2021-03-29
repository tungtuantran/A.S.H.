using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LampController : DeviceController
{
    private const float lockOnDelta = 0.0005f;
    private const float minBrightness = 0.15f;
    private const float maxBrightness = 1f;
    
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

    // cache light values for canceling when updating started
    private bool updatingStarted;

    // cached values for canceling
    private float lightBrightnessCancelCache;         
    private Color lightColorCancelCache;
    private Color lightTemperatureCancelCache;

    // cache light values for lockng when locking is selected
    private bool lockingSelected;
    private bool isLocked;

    public bool IsLocked {
        get
        {
            return isLocked;
        }

        set
        {
            isLocked = value;

            if (isLocked)
            {
                if (view != null)
                {
                    ((LampView)view).UpdateLightPreview((Lamp)device, updateLightBrightness, updateLightColor, updateLightTemperature);
                }
                Handheld.Vibrate();
            }
        }
    }

    private Vector3 lockedPosition;

    // cached values for locking
    private float lightBrightnessLockCache;    
    private Color lightColorLockCache;

    void Update()
    {
        float brightness = 0f;
        Color color = Color.white;
        Color temperatureColor = Color.white;

        // canceling is selected
        if((updateLightBrightness || updateLightColor || updateLightTemperature) && !updatingStarted)
        {
            CacheLightValuesForCanceling();
            updatingStarted = true;
        }

        // cancelins is not selected
        if (!updateLightBrightness && !updateLightColor && !updateLightTemperature && updatingStarted)
        {
            updatingStarted = false;
        }

        // locking is selected
        if (lockingSelected)
        {
            if (!IsLocked)
            {
                // light color value is locked
                if (Mathf.Abs(lockedPosition.z - brightnessCalculator.forwardDistance) > Mathf.Abs(lockedPosition.x - colorCalculator.sidewardDistance) + lockOnDelta && Mathf.Abs(lockedPosition.z - brightnessCalculator.forwardDistance) > Mathf.Abs(lockedPosition.y - colorCalculator.upwardDistance) + lockOnDelta)
                {
                    color = lightColorLockCache;
                    SetLightColor(color);
                    IsLocked = true;
                    updateLightColor = false;
                }

                //  light brightness value is locked
                else if (Mathf.Abs(lockedPosition.x - colorCalculator.sidewardDistance) > Mathf.Abs(lockedPosition.z + brightnessCalculator.forwardDistance) + lockOnDelta || Mathf.Abs(lockedPosition.y - colorCalculator.upwardDistance) > Mathf.Abs(lockedPosition.z - brightnessCalculator.forwardDistance) + lockOnDelta)
                {
                    brightness = lightBrightnessLockCache;
                    SetLightBrightness(brightness);
                    IsLocked = true;
                    updateLightBrightness = false;
                }
            }
        }

        if (updateLightBrightness)
        {
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


        ((LampView)view).UpdateLightPreview((Lamp)device, updateLightBrightness, updateLightColor, updateLightTemperature);
        ((LampView)view).UpdateAxis(updateLightBrightness, updateLightColor, updateLightTemperature);
    }

    protected override void SetDevice(string deviceName, int deviceId)
    {
        device = new Lamp(deviceName, deviceId);
    }

    public void InsertCachedLightValuesForCanceling()
    {
        SetLightBrightness(lightBrightnessCancelCache);
        SetLightColor(lightColorCancelCache);
        SetLightTemperature(lightTemperatureCancelCache);
    }

    // locking for brightness and color only
    public void SetLockingSelected(bool lockingSelected)
    {
        if (lockingSelected)
        {
            CacheLightValuesForLocking();
            lockedPosition = new Vector3(colorCalculator.sidewardDistance, colorCalculator.upwardDistance, brightnessCalculator.forwardDistance);
        }
        else
        {
            // reset
            updateLightColor = true;
            updateLightBrightness = true;
            IsLocked = false;
            Handheld.Vibrate();
        }

        this.lockingSelected = lockingSelected;
    }

    public override void StopUpdating()
    {
        updateLightColor = false;
        updateLightBrightness = false;
        updateLightTemperature = false;

        brightnessCalculator.Active = false;
        colorCalculator.Active = false;
        temperatureCalculator.Active = false;
    }

    public void PauseUpdatingLightBrightness(bool pause)
    {
        updateLightBrightness = !pause;
    }

    public void PauseUpdatingLightColor(bool pause)
    {
        updateLightColor = !pause;
    }

    public void PauseUpdatingLightTemperature(bool pause)
    {
        updateLightTemperature = !pause;
    }

    public void UpdateLightColor()
    {
        updateLightColor = true;
        colorCalculator.Active = true;

        ((LampView)view).ShowLightImagePreview();
    }

    public void UpdateLightBrightness()
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
        UpdateLightBrightness();
    }

    private void CacheLightValuesForCanceling()
    {
        lightBrightnessCancelCache = ((Lamp)device).LightBrightness;
        lightColorCancelCache = ((Lamp)device).LightColor;
        lightTemperatureCancelCache = ((Lamp)device).LightTemperature;
    }

    private void CacheLightValuesForLocking()
    {
        lightBrightnessLockCache = ((Lamp)device).LightBrightness;
        lightColorLockCache = ((Lamp)device).LightColor;
    }

    private float ConvertDistanceToBrightnessValue(float distanceForBrightness)
    {
        float brightnessDelta = distanceForBrightness * 100;                                                    // example: 0.0035 -> 0.35 (für farbsättigung wo 0-100%: *10000)
        float brightness = maxBrightness - (maxBrightness - lightBrightnessCancelCache) - brightnessDelta;      // brightness = 1 - (1 - lightBrightnessCancelCache) - brightnessDelta;

        if (brightness < minBrightness)
        {
            brightness = minBrightness;
        }

        if (brightness > maxBrightness)
        {
            brightness = maxBrightness;
        }

        return brightness;
    }

    private Color ConvertDistanceToColorValue(float distanceForHue, float distanceForSaturation)
    {
        float hDelta = distanceForHue * 100;
        float sDelta = distanceForSaturation * 2 * 100;                                                         // distance multiplied by 2 for a smaller max distance

        // get hue and saturation from cached color
        float hCache, sCache, vCache;
        Color.RGBToHSV(lightColorCancelCache, out hCache, out sCache, out vCache);

        // hue h and saturation s from hsv
        // Mathf.Abs() to keep hue value positive
        float h = Mathf.Abs(hCache + hDelta) % 1;                                                               // mod 1 to keep it smaller than 1f because h turns black if value is over 1f
        float s = 1 - (1 - sCache) - sDelta;

        if (s < 0f)
        {
            s = 0f;
        }

        // set color
        return Color.HSVToRGB(h, s, 1f);
    }

    private Color ConvertDistanceToTemperatureColorValue(float distanceForTemperature)
    {
        int texYDelta = Mathf.RoundToInt(distanceForTemperature * 100000);

        // get texY from temperatureCache
        int texYCache = GetYOfPixelByColor(temperatureTexture, lightTemperatureCancelCache);

        int texY = texYCache + texYDelta;

        if (texY > temperatureTexture.height)
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

    private int GetYOfPixelByColor(Texture2D texture, Color color)
    {
        for (int y = 0; y <= texture.height; y++) {
            if(color == texture.GetPixel(0,y))
            {
                return y;
            }
        }

        // pixel with given color not found
        return 0;
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