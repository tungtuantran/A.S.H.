using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisteredDeviceLog : MonoBehaviour
{
    [SerializeField]
    public Text txt;

    // Update is called once per frame
    void Update()
    {
        txt.text = DeviceCollection.DeviceCollectionInstance.registeredDevices.Count.ToString() + "\n ";
        foreach(Device device in DeviceCollection.DeviceCollectionInstance.registeredDevices)
        {
            txt.text += device.ToString() + "\n";
        }
    }
}
