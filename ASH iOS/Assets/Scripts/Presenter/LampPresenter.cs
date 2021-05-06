using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LampPresenter : DevicePresenter
{
    private const float lockOnDelta = 0.0005f;
    private const float minBrightness = 0.15f;
    private const float maxBrightness = 1f;

    public IDistanceCalculator BrightnessCalculator { get; set; }
    public IDistanceCalculator ColorCalculator { get; set; }
    public IDistanceCalculator TemperatureCalculator { get; set; }

    [SerializeField]
    private DistanceCalculator brightnessCalculator;

    [SerializeField]
    private DistanceCalculator colorCalculator;

    [SerializeField]
    private DistanceCalculator temperatureCalculator;

    [SerializeField]
    public Texture2D temperatureTexture;

    private bool updateLightBrightness;
    private bool updateLightColor;
    private bool updateLightTemperature;

    // cache light values for when updating started and for canceling
    private bool updatingStarted;

    // cached start values
    private float lightBrightnessStartCache;         
    private Color lightColorStartCache;
    private Color lightTemperatureStartCache;

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
                    ((LampView)view).OnUpdateLightPreview((Lamp)device, updateLightBrightness, updateLightColor, updateLightTemperature);
                }
                //Handheld.Vibrate();
            }
        }
    }

    private Vector3 lockedPosition;

    // cached values for locking
    private float lightBrightnessLockCache;    
    private Color lightColorLockCache;

    protected override void Awake()
    {
        base.Awake();

        // set calculator
        BrightnessCalculator = brightnessCalculator;
        ColorCalculator = colorCalculator;
        TemperatureCalculator = temperatureCalculator;
    }

    void Update()
    {
        float brightness = 0f;
        Color color = Color.white;
        Color temperatureColor = Color.white;

        if((updateLightBrightness || updateLightColor || updateLightTemperature) && !updatingStarted)
        {
            // updating started
            CacheLightValuesOnStart();
            updatingStarted = true;
        }

        if (!updateLightBrightness && !updateLightColor && !updateLightTemperature && updatingStarted)
        {
            // updating stopped
            updatingStarted = false;
        }

        // locking is selected
        if (lockingSelected)
        {
            if (!IsLocked)
            {
                // light color value is locked
                if (Mathf.Abs(lockedPosition.z - BrightnessCalculator.forwardDistance) > Mathf.Abs(lockedPosition.x - ColorCalculator.sidewardDistance) + lockOnDelta && Mathf.Abs(lockedPosition.z - BrightnessCalculator.forwardDistance) > Mathf.Abs(lockedPosition.y - ColorCalculator.upwardDistance) + lockOnDelta)
                {
                    color = lightColorLockCache;
                    SetLightColor(color);
                    IsLocked = true;
                    updateLightColor = false;
                }

                //  light brightness value is locked
                else if (Mathf.Abs(lockedPosition.x - ColorCalculator.sidewardDistance) > Mathf.Abs(lockedPosition.z + BrightnessCalculator.forwardDistance) + lockOnDelta || Mathf.Abs(lockedPosition.y - ColorCalculator.upwardDistance) > Mathf.Abs(lockedPosition.z - BrightnessCalculator.forwardDistance) + lockOnDelta)
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
            brightness = ConvertDistanceToBrightnessValue(BrightnessCalculator.forwardDistance);
            SetLightBrightness(brightness);
            ((LampView)view).OnUpdateLightBrightness(((Lamp)device).LightBrightness);
        }

        if (updateLightColor)
        {
            color = ConvertDistanceToColorValue(ColorCalculator.sidewardDistance, ColorCalculator.upwardDistance);
            SetLightColor(color);
            ((LampView)view).OnUpdateLightColor(((Lamp)device).LightColor);
        }

        if (updateLightTemperature)
        {
            temperatureColor = ConvertDistanceToTemperatureColorValue(TemperatureCalculator.upwardDistance);
            SetLightTemperature(temperatureColor);
            ((LampView)view).OnUpdateLightTemperature(((Lamp)device).LightTemperature);
        }


        ((LampView)view).OnUpdateLightPreview((Lamp)device, updateLightBrightness, updateLightColor, updateLightTemperature);
        ((LampView)view).OnUpdateAxis(updateLightBrightness, updateLightColor, updateLightTemperature);
    }

    protected override void SetDevice(string deviceName, int deviceId)
    {
        device = new Lamp(deviceName, deviceId);
    }

    protected override void InsertDefaultValuesToDevice()
    {
        ((Lamp)device).SetDefaultValues();
    }

    public override void InsertCopiedValuesToDevice(IDevice copiedDevice)
    {
        Lamp copiedLamp = (Lamp)copiedDevice;

        SetLightBrightness(copiedLamp.LightBrightness);
        SetLightColor(copiedLamp.LightColor);
        SetLightTemperature(copiedLamp.LightTemperature);
    }

    public void InsertCachedLightValuesOnStart()
    {
        SetLightBrightness(lightBrightnessStartCache);
        SetLightColor(lightColorStartCache);
        SetLightTemperature(lightTemperatureStartCache);
    }

    // locking for brightness and color only
    public void SetLockingSelected(bool lockingSelected)
    {
        if (lockingSelected)
        {
            CacheLightValuesOnLocking();
            lockedPosition = new Vector3(ColorCalculator.sidewardDistance, ColorCalculator.upwardDistance, BrightnessCalculator.forwardDistance);
        }
        else
        {
            // reset
            updateLightColor = true;
            updateLightBrightness = true;
            IsLocked = false;
            //Handheld.Vibrate();
        }

        this.lockingSelected = lockingSelected;
    }

    public override void ShowView()
    {
        base.ShowView();

        // if tracked device is registered in DeviceCollection
        if (DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(device.Id) != null)
        {
            ((LampView)view).OnUpdateLightBrightness(((Lamp)device).LightBrightness);
            ((LampView)view).OnUpdateLightTemperature(((Lamp)device).LightTemperature);
            ((LampView)view).OnUpdateLightColor(((Lamp)device).LightColor);
        }
    }

    public override void StopUpdating()
    {
        updateLightColor = false;
        updateLightBrightness = false;
        updateLightTemperature = false;

        BrightnessCalculator.Active = false;
        ColorCalculator.Active = false;
        TemperatureCalculator.Active = false;
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
        ColorCalculator.Active = true;

        ((LampView)view).OnUpdateLightColor(((Lamp)device).LightColor);
    }

    public void UpdateLightBrightness()
    {
        updateLightBrightness = true;
        BrightnessCalculator.Active = true;

        ((LampView)view).OnUpdateLightBrightness(((Lamp)device).LightBrightness);
    }

    public void UpdateLightTemperature()
    {
        updateLightTemperature = true;
        TemperatureCalculator.Active = true;

        ((LampView)view).OnUpdateLightTemperature(((Lamp)device).LightTemperature);
    }

    public void UpdateLightColorAndBrightness()
    {
        UpdateLightColor();
        UpdateLightBrightness();
    }

    private void CacheLightValuesOnStart()
    {
        lightBrightnessStartCache = ((Lamp)device).LightBrightness;
        lightColorStartCache = ((Lamp)device).LightColor;
        lightTemperatureStartCache = ((Lamp)device).LightTemperature;
    }

    private void CacheLightValuesOnLocking()
    {
        lightBrightnessLockCache = ((Lamp)device).LightBrightness;
        lightColorLockCache = ((Lamp)device).LightColor;
    }

    private float ConvertDistanceToBrightnessValue(float distanceForBrightness)
    {
        float brightnessDelta = distanceForBrightness * 100;                                                    // example: 0.0035 -> 0.35 (für farbsättigung wo 0-100%: *10000)
        float brightness = maxBrightness - (maxBrightness - lightBrightnessStartCache) - brightnessDelta;       // brightness = 1 - (1 - lightBrightnessStartCache) - brightnessDelta;

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
        Color.RGBToHSV(lightColorStartCache, out hCache, out sCache, out vCache);

        // hue h and saturation s from hsv
        // Mathf.Abs() to keep hue value positive
        float h = Mathf.Abs(hCache + hDelta) % 1;                                                               // mod 1 to keep it smaller than 1f because h turns black if value is over 1f
        float s = 1 - (1 - sCache) - sDelta;

        if (s < 0f)
        {
            s = 0f;
        }

        if(s > 1f)
        {
            s = 1f;
        }

        // set color
        return Color.HSVToRGB(h, s, 1f);
    }

    private Color ConvertDistanceToTemperatureColorValue(float distanceForTemperature)
    {
        int texYDelta = Mathf.RoundToInt(distanceForTemperature * 100000);

        // get texY from temperatureCache
        int texYCache = GetYOfPixelByColor(temperatureTexture, lightTemperatureStartCache);

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

    // ---------------- test methods ----------------

    public float TestConvertDistanceToBrightnessValue(float distanceForBrightness)
    {
        CacheLightValuesOnStart();
        return ConvertDistanceToBrightnessValue(distanceForBrightness);
    }

    public Color TestConvertDistanceToColorValue(float distanceForHue, float distanceForSaturation)
    {
        CacheLightValuesOnStart();
        return ConvertDistanceToColorValue(distanceForHue, distanceForSaturation);
    }

    public Color TestConvertDistanceToTemperatureColorValue(float distanceForTemperature)
    {
        CacheLightValuesOnStart();
        return ConvertDistanceToTemperatureColorValue(distanceForTemperature);
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