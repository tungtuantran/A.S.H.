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
        addDevicePopUp.SetActive(false);
        removeDeviceButton.gameObject.SetActive(false);
        removeDevicePopUp.SetActive(false);
        deviceControllerGameObject.SetActive(false);
        Setup();
    }

    void Update()
    {
        Setup();
    }

    private void Setup()
    {
        SetTrackedAndRegisteredDevice();

        if (trackedAndRegisteredDevice != null)                         // if tracked Device is also registered in DeviceCollection
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

    private void DisplayPropertiesOfTrackedAndRegisteredDevice()
    {
        lightGameObject.SetActive(trackedAndRegisteredDevice.isOn);

        Color lightColor = ((Lamp)trackedAndRegisteredDevice).lightColor;
        float lightBrightness = ((Lamp)trackedAndRegisteredDevice).lightBrightness;
        float lightTemperature = ((Lamp)trackedAndRegisteredDevice).lightTemperature;

        _light.color = lightColor;
        _light.intensity = lightBrightness;
        _light.colorTemperature = lightTemperature;

        //TODO: timer,...
    }


    /**
     * Selects device & then shows/hides device controller
     * 
     */
    public void SelectAndShowHideDeviceController()
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

    public void SelectAndSetDeviceOnOff()
    {
        SetSelectedDeviceIsTrackedDevice();

        deviceController.SetSelectedDeviceOnOff();
        //lightGameObject.SetActive(trackedAndRegisteredDevice.isOn);           -> wird von DisplayPropertiesOfTrackedAndRegisteredDevice() durch updaten automisch schon gehandeled?

    }

    private void SetSelectedDeviceIsTrackedDevice()                                     // set selectedDevice = currently Tracked Device
    {
        deviceController.selectedDevice = trackedAndRegisteredDevice;
    }

    private void SetTrackedAndRegisteredDevice()
    {
        trackedAndRegisteredDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(ImageTracking.deviceId);
    }

    /**
     * Adds currently TRACKED Device
     */
    public void AddTrackedDevice()
    {
        if (addDeviceNameInputField.text != "")
        {
            deviceController.AddCurrentlyTrackedDevice(addDeviceNameInputField.text);
        }
        else
        {
            //TODO: fehlermeldung im UI
        }

        addDevicePopUp.SetActive(false);
        addDeviceButton.gameObject.SetActive(false);
    }

    /**
     * Removes SELECTED Device
     */
    public void RemoveSelectedDevice()
    {
        deviceController.RemoveSelectedDevice();
        removeDevicePopUp.SetActive(false);
    }

    public void ShowHideAddDevicePopUp()            // for "add me" button and cancel button
    {
        if (addDevicePopUp.activeSelf)              // Cancel Button (in Pop-Up)
        {
            addDevicePopUp.SetActive(false);
        }
        else
        {                                           // Add Me Button
            addDevicePopUp.SetActive(true);
        }
        addDeviceNameInputField.text = "";          // clears textInput
    }

    public void ShowHideRemoveDevicePopUp() 
    {
        if (removeDevicePopUp.activeSelf)
        {
            removeDevicePopUp.SetActive(false);     // Cancel Button (in Pop-Up)
        }
        else
        {
            removeDevicePopUp.SetActive(true);      // Remove Button
        }
    }

}
