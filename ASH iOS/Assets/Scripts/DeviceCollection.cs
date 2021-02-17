using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceCollection : MonoBehaviour
{

    public List<Device> registeredDevices { get; set; } = new List<Device>();


    public void addRegisteredDevice(Device device)
    {
        this.registeredDevices.Add(device);
        this.SaveDeviceCollection();
    }

    public void deleteRegisteredDevice(Device device)
    {
        this.registeredDevices.Remove(device);
        this.SaveDeviceCollection();
    }

    private void SaveDeviceCollection()                 //has to be called if sth needs to be updated
    {
        SaveSystem.SaveDeviceCollection(this);
    }

    private void LoadDeviceCollection()                                                 
    {
        DeviceCollectionData deviceCollectionData = SaveSystem.LoadDeviceCollection();
        for(int i=0; i < deviceCollectionData.deviceDatas.Length; i++)
        {
            
            IDeviceData deviceData = deviceCollectionData.deviceDatas[i];
            Device device = null;

            switch (deviceData.GetType().Name)
            {
                case "LampData":
                    device = new Lamp(deviceData.deviceName, deviceData.id, deviceData._name);
                    break;
                default:
                    Debug.LogError("Uknown Data Type");
                    break;
            }

            if (device != null)
            {
                device.SetLoadedDeviceData(deviceData);
                registeredDevices.Add(device);
            }
        }
    }
}
