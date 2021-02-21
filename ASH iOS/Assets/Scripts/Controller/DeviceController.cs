using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class DeviceController : MonoBehaviour
{
    public Device selectedDevice { get; set; }          //device that's selected by menu
    
    public Device trackedDevice { get; set; }           //??? /deevice that is (selected) by tracking  

    //public Button OnOffButton { get; set; }

    public void SetSelectedDeviceOnOff()
    {
        if (selectedDevice.isOn)
        {
            selectedDevice.isOn = false;
        }
        else
        {
            selectedDevice.isOn = true;
        }
    }

    public void AddCurrentTrackedDevice(string name)
    {
        switch (ImageTracking.deviceShortName)
        {
            case "SL1":
            case "SL2":
            case "TL1":
            case "TL4":
            case "WL4":
                Device deviceToAdd = new Lamp(ImageTracking.deviceName, ImageTracking.deviceId, name);
                DeviceCollection.DeviceCollectionInstance.AddRegisteredDevice(deviceToAdd);
                break;
            default:
                Debug.LogError("Invalid Device Short Name");
                throw new InvalidMarkerException("Invalid Device Short Name");
        }


        //just for testing:
        //DeviceCollection.DeviceCollectionInstance.AddRegisteredDevice(new Lamp("standing_lamp1", 1, "RandomName"));
    }

    public void RemoveSelectedDevice()
    {
        DeviceCollection.DeviceCollectionInstance.RemoveRegisteredDevice(selectedDevice);

        //just for testing:
        //DeviceCollection.DeviceCollectionInstance.RemoveRegisteredDevice(DeviceCollection.DeviceCollectionInstance.registeredDevices[DeviceCollection.DeviceCollectionInstance.registeredDevices.Count-1]);      //TODO
    }

}
