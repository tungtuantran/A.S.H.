using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorMixer : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI textColorA;

    [SerializeField]
    public TextMeshProUGUI textColorB;

    [SerializeField]
    public TextMeshProUGUI text;

    private void Update()
    {
        float h, s, v;
        Color.RGBToHSV(textColorB.color, out h, out s, out v);
        text.color = MixColorWithTemperature(textColorA.color, textColorB.color, s);        //z.B.: weiss: RGB(255,255,255) => HSV(0,0%,100%) -> s = 0%
                                                                                
    }

    private Color MixColorWithTemperature(Color color, Color temperatureColor, float saturationOfTemperatureColor)
    {
        float temperatureColorPortion = saturationOfTemperatureColor;                       //the whiter the temperatureColor is, the smaller its portion
        float colorPortion = 1 - temperatureColorPortion;
        Debug.Log("tempColor Portion: " + temperatureColorPortion + "; color portion: " + colorPortion);
        float redValue, greenValue, blueValue;

        redValue = color.r * colorPortion + temperatureColor.r * temperatureColorPortion;
        greenValue = color.g * colorPortion + temperatureColor.g * temperatureColorPortion;
        blueValue = color.b * colorPortion + temperatureColor.b * temperatureColorPortion;

        return new Color(redValue, greenValue, blueValue);
    }
}
