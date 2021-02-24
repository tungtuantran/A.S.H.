using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LampDisplay : DeviceDisplay
{
    private const string ADD_NAME_INPUTFIELD_PATH = "Pop Up/Content/Name InputField";

    [SerializeField]
    public Light _light;

    private InputField addNameInputField;

    void Start()
    {
        addNameInputField = addDevicePopUp.transform.Find(ADD_NAME_INPUTFIELD_PATH).gameObject.GetComponent<InputField>();

        selectOnOffDeviceButton.gameObject.SetActive(false);
        addDeviceButton.gameObject.SetActive(false);
        addDevicePopUp.SetActive(false);
        removeDeviceButton.gameObject.SetActive(false);
        removeDevicePopUp.SetActive(false);
        deviceController.gameObject.SetActive(false);
    }

    void Update()
    {
        SetTrackedAndRegisteredDevice();

        if (trackedAndRegisteredDevice != null)                         // if tracked Device is also registered in DeviceCollection
        {
            DisplayPropertiesOfTrackedAndRegisteredDevice();

            addDeviceButton.gameObject.SetActive(false);
            removeDeviceButton.gameObject.SetActive(true);

            selectOnOffDeviceButton.gameObject.SetActive(true);
            selectOnOffDeviceButton.gameObject.GetComponentInChildren<Text>().text = trackedAndRegisteredDevice._name;
        }
        else
        {
            addDeviceButton.gameObject.SetActive(true);
            removeDeviceButton.gameObject.SetActive(false);

            selectOnOffDeviceButton.gameObject.SetActive(false);
            deviceController.gameObject.SetActive(false);

        }
    }

    private void DisplayPropertiesOfTrackedAndRegisteredDevice()
    {
        _light.gameObject.SetActive(trackedAndRegisteredDevice.isOn);

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

        if (deviceController.gameObject.activeSelf)
        {
            deviceController.gameObject.SetActive(false);
        }
        else
        {
            deviceController.gameObject.SetActive(true);
        }
    }

    public void SelectAndSetDeviceOnOff()
    {
        SetSelectedDeviceIsTrackedDevice();

        deviceController.SetSelectedDeviceOnOff();
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
        string name = addNameInputField.text;
        if (name != "")
        {
            deviceController.AddCurrentlyTrackedDevice(name);
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
        addNameInputField.text = "";                // clears textInput
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
