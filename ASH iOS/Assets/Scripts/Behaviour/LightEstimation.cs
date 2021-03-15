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
        }

        if (args.lightEstimation.averageColorTemperature.HasValue)
        {
            _light.colorTemperature = args.lightEstimation.averageColorTemperature.Value;
        }

        if (args.lightEstimation.colorCorrection.HasValue)
        {
            _light.color = args.lightEstimation.colorCorrection.Value;
        }
    }

}
