using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampDisplay : DeviceDisplay
{
    [SerializeField]
    public GameObject lightGameObject;

    private Light _light;

    void Start()
    {
        _light = lightGameObject.GetComponent<Light>();

        OnOffAndSelectDeviceButton.SetActive(false);
        addDeviceButton.gameObject.SetActive(false);
        removeDeviceButton.gameObject.SetActive(false);
        deviceControllerGameObject.SetActive(false);
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
            DisplayPropertiesOfTrackedAndRegisteredDevice();

            addDeviceButton.gameObject.SetActive(false);
            removeDeviceButton.gameObject.SetActive(true);

            OnOffAndSelectDeviceButton.SetActive(true);
            TextOfOnOffAndSelectDeviceButton.text = trackedAndRegisteredDevice._name;
        }
        else
        {
            addDeviceButton.gameObject.SetActive(true);
            removeDeviceButton.gameObject.SetActive(false);
            OnOffAndSelectDeviceButton.SetActive(false);
            deviceControllerGameObject.SetActive(false);

        }
    }

    private void DisplayPropertiesOfTrackedAndRegisteredDevice()     //hier werden alle eigeschaften von lamp object zum displayn gesetzt
    {
        Color lightColor = ((Lamp)trackedAndRegisteredDevice).lightColor;
        float lightBrightness = ((Lamp)trackedAndRegisteredDevice).lightBrightness;

        lightGameObject.SetActive(trackedAndRegisteredDevice.isOn);
        _light.color = lightColor;
        _light.intensity = lightBrightness;
        _light.colorTemperature = ((Lamp)trackedAndRegisteredDevice).lightTemperature;

        //TODO: timer,...
    }


    public void SelectedAndShowHideDeviceController()
    {
        SetSelectedDeviceIsTrackedDevice();

        if (deviceControllerGameObject.activeSelf)
        {
            deviceControllerGameObject.SetActive(false);
        }
        else
        {
            deviceControllerGameObject.SetActive(true);
        }
    }

    private void SetSelectedDeviceIsTrackedDevice()                                     //set selectedDevice = currently Tracked Device
    {
        deviceController.selectedDevice = trackedAndRegisteredDevice;
    }

    private void SetTrackedAndRegisteredDevice()
    {
        trackedAndRegisteredDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(ImageTracking.deviceId);
    }

    public void SelectDeviceAndSetDeviceOnOff()
    {
        SetSelectedDeviceIsTrackedDevice();
        deviceController.SetSelectedDeviceOnOff();
        lightGameObject.SetActive(trackedAndRegisteredDevice.isOn);

    }

    public void ShowHideAddDevicePopUp()            //for add device and cancel button
    {
        if (addDevicePopUp.activeSelf)              //Cancel Button
        {
            addDevicePopUp.SetActive(false);
            addDeviceNameInputField.text = "";      //clears textInput if pop get canceled
        }
        else
        {                                           //Add Button
            addDevicePopUp.SetActive(true);
        }
    }

    public void SaveAddedDevice()
    {
        if (addDeviceNameInputField.text != "")
        {
            deviceController.AddCurrentTrackedDevice(addDeviceNameInputField.text);
        }
        else
        {
            //TODO: fehlermeldung im UI 
        }

        addDevicePopUp.SetActive(false);
        addDeviceButton.gameObject.SetActive(false);

        addDeviceNameInputField.text = "";                                            //clears textInput if pop get canceled
    }

    public void ShowHideRemoveTrackedAndRegisteredDevicePopUp()             //for add device and cancel button
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

    public void RemoveDevice()             //for menu controller
    {
        deviceController.RemoveSelectedDevice();              //remove a selected (specific) device

        removeDevicePopUp.SetActive(false);
    }

}
