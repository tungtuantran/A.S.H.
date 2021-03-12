using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LampARViewController : ARViewController
{

    [SerializeField]
    private Image lightColor;

    [SerializeField]
    private Image lightTemperatureColor;

    [SerializeField]
    private Text brightness;

    protected override void UpdateValueDisplay()
    {
        lightColor.color = ((Lamp)trackedAndRegisteredDevice).lightColor;
        lightTemperatureColor.color = ((Lamp)trackedAndRegisteredDevice).lightTemperature;
        float brightnessInPercent = ((Lamp)trackedAndRegisteredDevice).lightBrightness * 100;
        brightness.text = Convert.ToInt32(brightnessInPercent).ToString() + "%";

    }
}
