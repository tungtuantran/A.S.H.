using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class BrightnessPreview : DistancePreview
{

    public override void SetDistance(float distance)
    {
        float brightnessInPercent = 100 - distance * 10000;           //170.121212%

        if (brightnessInPercent < 15f)
        {
            distancePreview.text = "15%";
        }
        else
        {
            distancePreview.text = Convert.ToInt32(brightnessInPercent).ToString() + "%";
        }
    }
}