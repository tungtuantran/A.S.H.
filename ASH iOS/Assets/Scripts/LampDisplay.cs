using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampDisplay : DeviceDisplay
{
    [SerializeField]
    public GameObject _light;

    private void Awake()
    {
        addDeviceButton.SetActive(false);
        removeDeviceButton.SetActive(false);

    }

    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        Setup();
    }

    void Setup()
    {
        SetTrackedAndRegisteredDevice();
        if (trackedAndRegisteredDevice != null)          //if registered in DeviceCollection
        {
            addDeviceButton.SetActive(false);
            removeDeviceButton.SetActive(true);
            aRDeviceController.SetActive(true);
            SetDeviceOnOff();
        }
        else
        {
            addDeviceButton.SetActive(true);
            removeDeviceButton.SetActive(false);
        }
    }

    public void ShowHideDeviceController()
    {
        if (aRDeviceController.activeSelf)
        {
            aRDeviceController.SetActive(false);
        }
        else
        {
            aRDeviceController.SetActive(true);
        }
    }

    private void SetTrackedAndRegisteredDevice()
    {
        trackedAndRegisteredDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(ImageTracking.deviceId);
    }

    private void SetDeviceOnOff()
    {
        if (trackedAndRegisteredDevice.isOn)
        {
            _light.SetActive(true);
        }
        else
        {
            _light.SetActive(false);
        }
    }

    public void ShowHideAddDevicePopUp()            //for add device and cancel button
    {
        if (addDevicePopUp.activeSelf)              //Cancel Button
        {
            addDevicePopUp.SetActive(false);
            addDeviceName.text = "";                //clears textInput if pop get canceled
        }
        else
        {                                           //Add Button
            addDevicePopUp.SetActive(true);
        }
    }

    public void SaveAddedDevice()
    {
        ImageTracking.AddTrackedDevice(addDeviceName.text);

        addDevicePopUp.SetActive(false);
        addDeviceButton.SetActive(false);

        addDeviceName.text = "";                    //clears textInput if pop get canceled
    }

    public void ShowHideRemoveTrackedAndRegisteredDevicePopUp()            //for add device and cancel button
    {
        if (removeDevicePopUp.activeSelf)                                   //Cancel Button
        {
            removeDevicePopUp.SetActive(false);
        }
        else
        {
            removeDevicePopUp.SetActive(true);
        }
    }

    public void RemoveTrackedAndRegisteredDevice()
    {
        ImageTracking.RemoveTrackedDevice();

        removeDevicePopUp.SetActive(false);
        removeDevicePopUp.SetActive(false);
    }

}
