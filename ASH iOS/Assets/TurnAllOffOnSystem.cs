using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAllOffOnSystem : MonoBehaviour
{
    public void TurnAllOffOn()
    {
        if (DeviceCollection.DeviceCollectionInstance.allDevicesOff) {
            DeviceCollection.DeviceCollectionInstance.allDevicesOff = false;
        }
        else
        {
            DeviceCollection.DeviceCollectionInstance.allDevicesOff = true;
        }

        Debug.Log("All Devices Off State: " + DeviceCollection.DeviceCollectionInstance.allDevicesOff);
    }
}
