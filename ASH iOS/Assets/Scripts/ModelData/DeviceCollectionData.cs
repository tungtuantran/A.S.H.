using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeviceCollectionData
{
    public IDeviceData[] deviceDatas { get; set; }
    public bool allDevicesOff { get; set; }

    public DeviceCollectionData(DeviceCollection deviceCollection)
    {
        deviceDatas = new IDeviceData[deviceCollection.registeredDevices.Count];
        for (int i = 0; i < deviceCollection.registeredDevices.Count; i++) {
            switch (deviceCollection.registeredDevices[i].GetType().Name) {
                case "Lamp":
                    deviceDatas[i] = new LampData((Lamp) deviceCollection.registeredDevices[i]);
                    break;
                default:
                    Debug.LogError("Unknown Device Type");
                    break;
            }
        }

        allDevicesOff = deviceCollection.allDevicesOff;
    }
}
