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
        txt.text = "Device Counter: " + DeviceCollection.DeviceCollectionInstance.RegisteredDevices.Count.ToString() + "\n \n";

        foreach(Device device in DeviceCollection.DeviceCollectionInstance.RegisteredDevices)
        {
            txt.text += device.ToString() + "\n \n";
        }
    }
}
