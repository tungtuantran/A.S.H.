using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class LightEstimation : MonoBehaviour
{
    private ARCameraManager aRCameraManger;

    [SerializeField]
    private Light _light;

    /*
    [SerializeField]
    private Text text1;

    [SerializeField]
    private Text text2;

    [SerializeField]
    private Text text3;
    */

    private void Awake()
    {
        GameObject arCamera = GameObject.FindGameObjectWithTag("MainCamera");
        aRCameraManger = arCamera.GetComponent<ARCameraManager>();
    }

    private void OnEnable()
    {
        aRCameraManger.frameReceived += FrameUpdated;
    }

    private void OnDisable()
    {
        aRCameraManger.frameReceived -= FrameUpdated;    
    }

    private void FrameUpdated(ARCameraFrameEventArgs args)
    {
        if (args.lightEstimation.averageBrightness.HasValue)
        {
            _light.intensity = args.lightEstimation.averageBrightness.Value;
            //text1.text = $"Brightness: {args.lightEstimation.averageBrightness.Value}";
            Debug.Log("Average Brightness: " + args.lightEstimation.averageBrightness.Value);
        }

        if (args.lightEstimation.averageColorTemperature.HasValue)
        {
            _light.colorTemperature = args.lightEstimation.averageColorTemperature.Value;
            //text2.text = $"Temperature: {args.lightEstimation.averageColorTemperature.Value}";
            Debug.Log("Average Color Temperature: " + args.lightEstimation.averageColorTemperature.Value);
        }

        if (args.lightEstimation.colorCorrection.HasValue)
        {
            _light.color = args.lightEstimation.colorCorrection.Value;              //_light.color ??
            //text3.text = $"ColorCorrection: {args.lightEstimation.colorCorrection.Value}";
            Debug.Log("Color Correction: " + args.lightEstimation.colorCorrection.Value);
        }
    }

}
