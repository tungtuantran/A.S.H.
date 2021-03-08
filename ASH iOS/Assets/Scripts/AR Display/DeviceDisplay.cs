using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DeviceDisplay : MonoBehaviour
{
    protected Device trackedAndRegisteredDevice;

    void Start()
    {
    }

    void Update()
    {
        trackedAndRegisteredDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(ImageTracking.deviceId);

        if (trackedAndRegisteredDevice != null)                         // if tracked Device is also registered in DeviceCollection
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
