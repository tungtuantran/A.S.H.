using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampDisplay : DeviceDisplay
{
    [SerializeField]
    public GameObject _light;

    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        Setup();
    }

    void Setup()
    {
        SetTrackedAndRegisteredDevice();
        if (trackedDevice != null)          //if registered in DeviceCollection
        {
            ShowDeviceController();
            SetOnOff();
        }
    }

    private void ShowDeviceController()
    {
        aRDeviceController.SetActive(true);
    }

    private void SetTrackedAndRegisteredDevice()
    {
        trackedDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(ImageTracking.deviceId);
    }

    private void SetOnOff()
    {
        if (trackedDevice.isOn)
        {
            _light.SetActive(true);
        }
        else
        {
            _light.SetActive(false);
        }
    }
}
