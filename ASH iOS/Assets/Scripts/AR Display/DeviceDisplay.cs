using UnityEngine;

/*
 * Displays Property Values of Device
 */
public abstract class DeviceDisplay : MonoBehaviour
{
    protected IDevice registeredDevice;

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
