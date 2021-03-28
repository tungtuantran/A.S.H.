using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class LampView : DeviceView
{
    public Axes axes;
    public Text lightTextPreview;
    public Image lightImagePreview;

    void Awake()
    {
        // preview hidden on default
        HideLightPreview();
    }

    public void UpdateAxis(bool updateLightBrightness, bool updateLightColor, bool updateLightTemperature)
    {
        if (updateLightBrightness)
        {
            axes.zAxis.SetActive(true);
        }
        else
        {
            axes.zAxis.SetActive(false);
        }

        if (updateLightColor)
        {
            axes.xAxis.SetActive(true);
            axes.yAxis.SetActive(true);
        }
        else
        {
            axes.xAxis.SetActive(false);
            axes.yAxis.SetActive(false);
        }

        if (updateLightTemperature)
        {
            axes.xAxis.SetActive(false);
            axes.yAxis.SetActive(true);
            axes.zAxis.SetActive(false);

        }
    }

    public void ShowLightImagePreview()
    {
        lightImagePreview.gameObject.SetActive(true);
    }

    public void UpdateLightPreview(Lamp lamp, bool updateLightBrightness, bool updateLightColor, bool updateLightTemperature)
    {

        if (!updateLightBrightness && !updateLightColor && !updateLightTemperature)
        {
            HideLightPreview();
        }
        else
        {
            float brightness = lamp.LightBrightness;
            Color color = lamp.LightColor;
            Color temperatureColor = lamp.LightTemperature;

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
    }

    private void HideLightPreview()
    {
        lightTextPreview.gameObject.SetActive(false);
        lightImagePreview.gameObject.SetActive(false);
    }

}
