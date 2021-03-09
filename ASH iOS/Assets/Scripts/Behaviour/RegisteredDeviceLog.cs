using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisteredDeviceLog : MonoBehaviour
{
    [SerializeField]
    private Text txt;

    void Update()
    {
        txt.text = "Device Counter: " + DeviceCollection.DeviceCollectionInstance.registeredDevices.Count.ToString() + "\n \n";

        foreach(Device device in DeviceCollection.DeviceCollectionInstance.registeredDevices)
        {
            txt.text += device.ToString() + "\n \n";
        }
    }
}
