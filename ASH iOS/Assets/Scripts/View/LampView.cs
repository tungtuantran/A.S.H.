using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class LampView : DeviceView
{
    public Axes axes;
    public AxesLabeling axesLabeling;
    public Text lightTextPreview;
    public Image lightImagePreview;

    [SerializeField]
    private Image lightColorImage;

    [SerializeField]
    private Image lightTemperatureColorImage;

    [SerializeField]
    private Text brightnessText;

    void Awake()
    {
        // preview hidden on default
        HideLightPreview();

        //hide axes
        axes.HideAxesAndStopRotation();

        // hide axes labeling
        axesLabeling.HideLabelOfXAxis();
        axesLabeling.HideLabelOfYAxis();
        axesLabeling.HideLabelOfZAxis();

    }

    public void UpdateAxis(bool updateLightBrightness, bool updateLightColor, bool updateLightTemperature)
    {
        if (updateLightBrightness)
        {
            axes.zAxis.SetActive(true);

            axesLabeling.SetLabelTextOfZAxis("Brightness");
            axesLabeling.ShowLabelOfZAxis();
        }
        else
        {
            axes.zAxis.SetActive(false);

            axesLabeling.HideLabelOfZAxis();
        }

        if (updateLightColor)
        {
            axes.xAxis.SetActive(true);
            axes.yAxis.SetActive(true);

            axesLabeling.SetLabelTextOfXAxis("Hue");
            axesLabeling.SetLabelTextOfYAxis("Saturation");
            axesLabeling.ShowLabelOfXAxis();
            axesLabeling.ShowLabelOfYAxis();

        }
        else
        {
            axes.xAxis.SetActive(false);
            axes.yAxis.SetActive(false);

            axesLabeling.HideLabelOfXAxis();
            axesLabeling.HideLabelOfYAxis();
        }

        if (updateLightTemperature)
        {
            axes.xAxis.SetActive(false);
            axes.yAxis.SetActive(true);
            axes.zAxis.SetActive(false);

            axesLabeling.SetLabelTextOfYAxis("Temperature");
            axesLabeling.HideLabelOfXAxis();
            axesLabeling.ShowLabelOfYAxis();
            axesLabeling.HideLabelOfZAxis();
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

    protected override void UpdateValueDisplay()
    {
        base.UpdateValueDisplay();

        lightColorImage.color = ((Lamp)trackedDevice).LightColor;
        lightTemperatureColorImage.color = ((Lamp)trackedDevice).LightTemperature;
        float brightnessInPercent = ((Lamp)trackedDevice).LightBrightness * 100;
        brightnessText.text = Convert.ToInt32(brightnessInPercent).ToString() + "%";

    }

    private void HideLightPreview()
    {
        lightTextPreview.gameObject.SetActive(false);
        lightImagePreview.gameObject.SetActive(false);
    }

}
