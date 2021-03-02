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
            DisplayPropertiesOfTrackedAndRegisteredDevice();
        }
    }

    protected abstract void DisplayPropertiesOfTrackedAndRegisteredDevice();
}
