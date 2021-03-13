using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LampARViewController : ARViewController
{

    [SerializeField]
    private Image lightColorImage;

    [SerializeField]
    private Image lightTemperatureColorImage;

    [SerializeField]
    private Text brightnessText;

    protected override void UpdateValueDisplay()
    {
        base.UpdateValueDisplay();

        lightColorImage.color = ((Lamp)trackedAndRegisteredDevice).lightColor;
        lightTemperatureColorImage.color = ((Lamp)trackedAndRegisteredDevice).lightTemperature;
        float brightnessInPercent = ((Lamp)trackedAndRegisteredDevice).lightBrightness * 100;
        brightnessText.text = Convert.ToInt32(brightnessInPercent).ToString() + "%";

    }
}
