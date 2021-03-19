using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DeviceDisplay : MonoBehaviour
{
    protected Device registeredDevice;

    private int deviceId;

    void Awake()
    {
        deviceId = ImageTracking.deviceId;
    }

    void Update()
    {
        registeredDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(deviceId);

        // if tracked Device is also registered in DeviceCollection
        if (registeredDevice != null)                         
        {
            if (!DeviceCollection.DeviceCollectionInstance.AllDevicesOff)
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
