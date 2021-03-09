using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAllOffOnSystem : MonoBehaviour
{

    public DistanceCalculator distanceCalculator;

    private bool doUpdate;

    private void Update()
    {
        if (doUpdate)
        {
            float distance = distanceCalculator.distance * 100;
            Debug.Log("distance: " + distance);

            if (distance > 0.3f)
            {
                Debug.Log("is bigger than 0.3");
                TurnAllOffOn();
            }

        }
    }

    private void TurnAllOffOn()
    {
        if (DeviceCollection.DeviceCollectionInstance.allDevicesOff) {
            DeviceCollection.DeviceCollectionInstance.allDevicesOff = false;
        }
        else
        {
            DeviceCollection.DeviceCollectionInstance.allDevicesOff = true;
        }

        StopCalculatingDistance();

        Debug.Log("All Devices Off State: " + DeviceCollection.DeviceCollectionInstance.allDevicesOff);
    }

    public void StartCalculatingDistance()
    {
        doUpdate = true;
        distanceCalculator.active = true;
    }

    public void StopCalculatingDistance()
    {
        doUpdate = false;
        distanceCalculator.active = false;
    }
}
