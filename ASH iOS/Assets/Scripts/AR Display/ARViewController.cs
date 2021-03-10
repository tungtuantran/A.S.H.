﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARViewController : MonoBehaviour
{
    private const string ADD_NAME_INPUTFIELD_PATH = "Pop Up/Content/Name InputField";
    private const string VALUE_DISPLAY_TEXT_PATH = "Scroll View/Viewport/Content/Value Display Text";
    private const string EDIT_NAME_INPUTFIELD_PATH = "Edit Name InputField";

    [SerializeField]
    private DeviceController deviceController;

    [SerializeField]
    private Button addDeviceButton;

    [SerializeField]
    private GameObject addDevicePopUp;

    [SerializeField]
    private GameObject removeDevicePopUp;

    [SerializeField]
    private GameObject aRView;

    private Text valueDisplayText;
    private InputField editNameInputField;
    private InputField addNameInputField;

    private Device trackedAndRegisteredDevice;
    private bool setNameOnFirstTrack;

    void Start()
    {
        addNameInputField = addDevicePopUp.transform.Find(ADD_NAME_INPUTFIELD_PATH).gameObject.GetComponent<InputField>();
        valueDisplayText = aRView.transform.Find(VALUE_DISPLAY_TEXT_PATH).gameObject.GetComponent<Text>();
        editNameInputField = aRView.transform.Find(EDIT_NAME_INPUTFIELD_PATH).gameObject.GetComponent<InputField>();

        addDeviceButton.gameObject.SetActive(false);
        addDevicePopUp.SetActive(false);
        removeDevicePopUp.SetActive(false);
        deviceController.gameObject.SetActive(false);
        aRView.SetActive(false);
    }

    void Update()
    {
        trackedAndRegisteredDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(ImageTracking.deviceId);

        if (trackedAndRegisteredDevice != null)                         // if tracked Device is also registered in DeviceCollection
        {      
            addDeviceButton.gameObject.SetActive(false);
            deviceController.gameObject.SetActive(true);

            UpdateValueDisplay();

            if (!setNameOnFirstTrack)
            {
                setNameOnFirstTrack = true;
                UpdateName();
            }

            aRView.SetActive(true);
            editNameInputField.gameObject.SetActive(true);

        }
        else
        {
            addDeviceButton.gameObject.SetActive(true);
            deviceController.gameObject.SetActive(false);
            aRView.SetActive(false);
        }
    }

    private void UpdateValueDisplay()
    {
        valueDisplayText.text = trackedAndRegisteredDevice.DeviceValuesToString();
    }

    private void UpdateName()
    {

        editNameInputField.text = trackedAndRegisteredDevice._name;
    }

    public void EditName()
    {
        if (!editNameInputField.text.Equals(""))
        {
            deviceController.SelectDeviceByCurrentlyTrackedDevice();
            deviceController.EditNameOfSelectedDevice(editNameInputField.text);
        }

        UpdateName();
    }

    public void AddTrackedDevice()                     
    {
        string name = addNameInputField.text;
        if (name != "")
        {
            deviceController.AddCurrentlyTrackedDevice(name);
            editNameInputField.text = name;
        }
        else
        {
            //TODO: fehlermeldung im UI
        }

        addDevicePopUp.SetActive(false);
        addDeviceButton.gameObject.SetActive(false);
    }

    public void RemoveDevice()
    {
        deviceController.RemoveSelectedDevice();
        removeDevicePopUp.SetActive(false);
    }

    public void ShowHideAddDevicePopUp()                                // for "add me" button and cancel button
    {
        if (addDevicePopUp.activeSelf)                                  // Cancel Button (in Pop-Up)
        {
            addDevicePopUp.SetActive(false);
        }
        else
        {                                                               // Add Me Button
            addDevicePopUp.SetActive(true);
        }
        addNameInputField.text = "";                                    // clears textInput
    }

    public void ShowHideRemoveDevicePopUp()
    {
        deviceController.SelectDeviceByCurrentlyTrackedDevice();        //selects device by current track

        if (removeDevicePopUp.activeSelf)
        {
            removeDevicePopUp.SetActive(false);                         // Cancel Button (in Pop-Up)
        }
        else
        {
            removeDevicePopUp.SetActive(true);                          // Remove Button
        }
    }

}
