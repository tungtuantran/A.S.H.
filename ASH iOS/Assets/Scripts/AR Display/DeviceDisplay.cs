﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DeviceDisplay : MonoBehaviour
{
    protected Device trackedAndRegisteredDevice;

    private int deviceId;

    void Awake()
    {
        deviceId = ImageTracking.deviceId;
    }

    void Update()
    {
        trackedAndRegisteredDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(deviceId);

        // if tracked Device is also registered in DeviceCollection
        if (trackedAndRegisteredDevice != null)                         
        {
            if (!DeviceCollection.DeviceCollectionInstance.allDevicesOff)
            {
                DisplayPropertiesOfTrackedAndRegisteredDevice();
            }
            else
            {
                DisplayOffState();
            }
        }
    }

    protected abstract void DisplayPropertiesOfTrackedAndRegisteredDevice();

    protected abstract void DisplayOffState();
}
