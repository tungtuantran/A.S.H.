using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class BrightnessPreview : DistancePreview
{
    [SerializeField]
    private Text previewText;

    public override void ShowPreview(float upwardDistane, float forwardDistance, float sidewardDistance)
    {
        float brightnessInPercent = 100 - forwardDistance * 10000;

        if (brightnessInPercent < 15f)
        {
            previewText.text = "15%";
        }
        else
        {
            previewText.text = Convert.ToInt32(brightnessInPercent).ToString() + "%";
        }
    }
}