using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LampARViewController : DeviceARViewController
{

    [SerializeField]
    private Image lightColorImage;

    [SerializeField]
    private Image lightTemperatureColorImage;

    [SerializeField]
    private Text brightnessText;

    /*
    protected override void UpdateValueDisplay()
    {
        base.UpdateValueDisplay();

        lightColorImage.color = ((Lamp)trackedAndRegisteredDevice).LightColor;
        lightTemperatureColorImage.color = ((Lamp)trackedAndRegisteredDevice).LightTemperature;
        float brightnessInPercent = ((Lamp)trackedAndRegisteredDevice).LightBrightness * 100;
        brightnessText.text = Convert.ToInt32(brightnessInPercent).ToString() + "%";

    }
    */
}
