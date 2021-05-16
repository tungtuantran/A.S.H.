using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeviceCollectionData
{
    [SerializeReference]
    public IDeviceData[] DeviceDataList;
    public bool AllDevicesOff;

    public DeviceCollectionData(DeviceCollection deviceCollection)
    {
        DeviceDataList = new IDeviceData[deviceCollection.RegisteredDevices.Count];
        for (int i = 0; i < deviceCollection.RegisteredDevices.Count; i++) {
            switch (deviceCollection.RegisteredDevices[i].GetType().Name) {
                case "Lamp":
                    DeviceDataList[i] = new LampData((Lamp) deviceCollection.RegisteredDevices[i]);
                    break;
                default:
                    Debug.LogError("Unknown Device Type");
                    break;
            }
        }

        AllDevicesOff = deviceCollection.AllDevicesOff;
    }
}
