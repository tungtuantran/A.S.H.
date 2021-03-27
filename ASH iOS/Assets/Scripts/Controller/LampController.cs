using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LampController : DeviceController
{
    public Text lightTextPreview;
    public Image lightImagePreview;
    public AxesController axesController;
    
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

    private bool isUpdating;
    private float lightBrightnessCache;         // backup values for canceling
    private Color lightColorCache;
    private Color lightTemperatureCache;

    private bool isLocked;
    private bool switched;
    private float lightBrightnessLockCache;     // backup values for canceling     
    private Color lightColorLockCache;
    private Vector3 lockedPosition;

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

        if((updateLightBrightness || updateLightColor || updateLightTemperature) && !isUpdating)
        {
            CacheLightValues();

            isUpdating = true;
        }

        if ((!updateLightBrightness && !updateLightColor && !updateLightTemperature) && isUpdating)
        {
            isUpdating = false;
        }

        if (isLocked)
        {
            float switchOnDelta = 0.0010f;

            if (!switched)
            {
                /*
                Debug.Log("not switched");
                if (Mathf.Abs(brightnessCalculator.forwardDistance) > Mathf.Abs(colorCalculator.sidewardDistance) + switchOnDelta && Mathf.Abs(brightnessCalculator.forwardDistance) > Mathf.Abs(colorCalculator.upwardDistance) + switchOnDelta)
                {
                    updateLightColor = false;
                    color = lightColorLockCache;
                    switched = true;
                    Handheld.Vibrate();
                    axesController.xAxis.SetActive(false);
                    axesController.yAxis.SetActive(false);
                    axesController.zAxis.SetActive(true);
                    Debug.Log("switched to brightness");
                }else if (Mathf.Abs(colorCalculator.sidewardDistance) > Mathf.Abs(brightnessCalculator.forwardDistance) + switchOnDelta || Mathf.Abs(colorCalculator.upwardDistance) > Mathf.Abs(brightnessCalculator.forwardDistance) + switchOnDelta)
                {
                    updateLightBrightness = false;
                    brightness = lightBrightnessLockCache;
                    switched = true;
                    Handheld.Vibrate();
                    axesController.xAxis.SetActive(true);
                    axesController.yAxis.SetActive(true);
                    axesController.zAxis.SetActive(false);
                    Debug.Log("switched to color");
                }
                */

                Debug.Log("not switched");
                if (Mathf.Abs(lockedPosition.z - brightnessCalculator.forwardDistance) > Mathf.Abs(lockedPosition.x - colorCalculator.sidewardDistance) + switchOnDelta && Mathf.Abs(lockedPosition.z - brightnessCalculator.forwardDistance) > Mathf.Abs(lockedPosition.y - colorCalculator.upwardDistance) + switchOnDelta)
                {
                    color = lightColorLockCache;
                    SetLightColor(color);
                    ShowLightPreview();

                    updateLightColor = false;

                    switched = true;
                    Handheld.Vibrate();
                    axesController.xAxis.SetActive(false);
                    axesController.yAxis.SetActive(false);
                    axesController.zAxis.SetActive(true);
                    Debug.Log("switched to brightness");
                }
                else if (Mathf.Abs(lockedPosition.x - colorCalculator.sidewardDistance) > Mathf.Abs(lockedPosition.z + brightnessCalculator.forwardDistance) + switchOnDelta || Mathf.Abs(lockedPosition.y - colorCalculator.upwardDistance) > Mathf.Abs(lockedPosition.z - brightnessCalculator.forwardDistance) + switchOnDelta)
                {
                    brightness = lightBrightnessLockCache;
                    SetLightBrightness(brightness);
                    ShowLightPreview();

                    updateLightBrightness = false;

                    switched = true;
                    Handheld.Vibrate();
                    axesController.xAxis.SetActive(true);
                    axesController.yAxis.SetActive(true);
                    axesController.zAxis.SetActive(false);
                    Debug.Log("switched to color");
                }
            }

            Debug.Log("currentlySwitched:" + switched);
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

        ShowLightPreview();
        //ShowAxis();
    }

    /*
    private void ShowAxis()
    {
        if (updateLightBrightness)
        {
            axesController.zAxis.SetActive(true);
        }
        else
        {
            axesController.zAxis.SetActive(false);
        }

        if (updateLightColor)
        {
            axesController.xAxis.SetActive(true);
            axesController.yAxis.SetActive(true);
        }
        else
        {
            if (updateLightTemperature) {
                axesController.xAxis.SetActive(false);
                axesController.yAxis.SetActive(true);

            }
            else
            {
                axesController.xAxis.SetActive(false);
                axesController.yAxis.SetActive(false);
            }
        }
    }
    */

    public void InsertCachedLightValues()
    {
        SetLightBrightness(lightBrightnessCache);
        SetLightColor(lightColorCache);
        SetLightTemperature(lightTemperatureCache);
    }

    private void CacheLightValues()
    {
        lightBrightnessCache = ((Lamp)device).LightBrightness;
        lightColorCache = ((Lamp)device).LightColor;
        lightTemperatureCache = ((Lamp)device).LightTemperature;
    }

    // locking for brightness and color only
    public void SetLocked(bool locked)
    {
        if (locked)
        {
            CacheLightValuesForLock();
            lockedPosition = new Vector3(colorCalculator.sidewardDistance, colorCalculator.upwardDistance, brightnessCalculator.forwardDistance);
            Debug.Log(lockedPosition);
        }
        else
        {
            // reset
            updateLightColor = true;
            updateLightBrightness = true;
            switched = false;
            Handheld.Vibrate();
        }

        isLocked = locked;
    }


    private void CacheLightValuesForLock()
    {
        lightBrightnessLockCache = ((Lamp)device).LightBrightness;
        lightColorLockCache = ((Lamp)device).LightColor;
        Debug.Log("locked brightness: " + lightBrightnessCache + "; locked color: " + lightColorCache);
    }

    private float ConvertDistanceToBrightnessValue(float distanceForBrightness)
    {
        float brightnessDelta = distanceForBrightness * 100;                         // example: 0.0035 -> 0.35 (für farbsättigung wo 0-100%: *10000)
        float brightness = 1 - (1 - lightBrightnessCache) - brightnessDelta;

        if (brightness < 0.15f)
        {
            brightness = 0.15f;
        }

        if (brightness > 1f)
        {
            brightness = 1f;
        }

        return brightness;
    }

    private Color ConvertDistanceToColorValue(float distanceForHue, float distanceForSaturation)
    {
        float hDelta = distanceForHue * 100;
        float sDelta = distanceForSaturation * 2 * 100;             // distance multiplied by 2 for a smaller max distance

        // get hue and saturation from cached color
        float hCache, sCache, vCache;
        Color.RGBToHSV(lightColorCache, out hCache, out sCache, out vCache);

        // hue h and saturation s from hsv
        // Mathf.Abs() to keep hue value positive
        float h = Mathf.Abs(hCache + hDelta);
        float s = 1 - (1 - sCache) - sDelta;

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
        int texYDelta = Mathf.RoundToInt(distanceForTemperature * 100000);

        // get texY from temperatureCache
        int texYCache = GetYOfPixelByColor(temperatureTexture, lightTemperatureCache);

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

        Debug.Log("pixel not found");
        return 0;
    }

    private void ShowLightPreview()
    {
        float brightness = ((Lamp)device).LightBrightness;
        Color color = ((Lamp)device).LightColor;
        Color temperatureColor = ((Lamp)device).LightTemperature;

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

        lightImagePreview.gameObject.SetActive(true);
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