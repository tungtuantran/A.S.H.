using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ColorMixer : MonoBehaviour
{
    [SerializeField]
    public Image textColorA;

    [SerializeField]
    public Image textColorB;

    [SerializeField]
    public TextMeshProUGUI tempText;

    [SerializeField]
    public TextMeshProUGUI resultText;

    public float temperature;

    private void Update()
    {
        float h, s, v;

        //Color tempColor = BlackBodyColor(GetTemperature(textColorB.color));
        //text.color = tempColor;

        Color tempColor = BlackBodyColor(GetTemperature(textColorB.color));
        tempText.color = tempColor;

        
        Color.RGBToHSV(textColorB.color, out h, out s, out v);
        resultText.color = MixColorWithTemperature(textColorA.color, tempColor, s);        //z.B.: weiss: RGB(255,255,255) => HSV(0,0%,100%) -> s = 0%
                                                                             
    }

    private Color MixColorWithTemperature(Color color, Color temperatureColor, float saturationOfTemperatureColor)
    {
        float temperatureColorPortion = saturationOfTemperatureColor;                       //the whiter the temperatureColor is, the smaller its portion
        float colorPortion = 1 - temperatureColorPortion;
        //Debug.Log("tempColor Portion: " + temperatureColorPortion + "; color portion: " + colorPortion);
        float redValue, greenValue, blueValue;

        redValue = color.r * colorPortion + temperatureColor.r * temperatureColorPortion;
        greenValue = color.g * colorPortion + temperatureColor.g * temperatureColorPortion;
        blueValue = color.b * colorPortion + temperatureColor.b * temperatureColorPortion;

        return new Color(redValue, greenValue, blueValue);
    }

    public float GetTemperature(Color temperatureColor)
    {
        float X = (-0.14282f) *temperatureColor.r + 1.54924f * temperatureColor.g + (- 0.95641f) * temperatureColor.b;
        float Y = (-0.32466f) * temperatureColor.r + 1.57837f * temperatureColor.g + (-0.73191f) * temperatureColor.b;
        float Z = (-0.68202f) * temperatureColor.r + 0.77073f * temperatureColor.g + 0.56332f * temperatureColor.b;

        float x = X / (X + Y + Z);
        float y = Y / (X + Y + Z);

        float n = (x - 0.3320f) / (0.1858f - y);
        float CCT = 449 * Mathf.Pow(n, 3) + 3525 * Mathf.Pow(n, 2) + 6823.2f * n + 5520.33f;

        Debug.Log("Temperature in CCT / Kelvin: " + CCT.ToString());
        return CCT;

    }

    private Color BlackBodyColor(float temp)
    {
        float x = (float)(temp / 1000.0);
        float x2 = x * x;
        float x3 = x2 * x;
        float x4 = x3 * x;
        float x5 = x4 * x;

        float R, G, B = 0f;

        // red
        if (temp <= 6600)
            R = 1f;
        else
            R = 0.0002889f * x5 - 0.01258f * x4 + 0.2148f * x3 - 1.776f * x2 + 6.907f * x - 8.723f;

        // green
        if (temp <= 6600)
            G = -4.593e-05f * x5 + 0.001424f * x4 - 0.01489f * x3 + 0.0498f * x2 + 0.1669f * x - 0.1653f;
        else
            G = -1.308e-07f * x5 + 1.745e-05f * x4 - 0.0009116f * x3 + 0.02348f * x2 - 0.3048f * x + 2.159f;

        // blue
        if (temp <= 2000f)
            B = 0f;
        else if (temp < 6600f)
            B = 1.764e-05f * x5 + 0.0003575f * x4 - 0.01554f * x3 + 0.1549f * x2 - 0.3682f * x + 0.2386f;
        else
            B = 1f;

        Color tempColor = new Color(R, G, B, 1f);
        Debug.Log("Temperature in RGB: r =" + tempColor.r + ", g =" + tempColor.g + ", b =" + tempColor.b);
        return tempColor;
    }
}
