using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceView : MonoBehaviour, IDeviceView
{
    public InputField editNameInputField { get; set; }
    public InputField addNameInputField { get; set; }

    [SerializeField]
    private DeviceMenu deviceMenu;

    [SerializeField]
    private GameObject aRDisplay;

    [SerializeField]
    private GameObject addDevicePopUp;

    [SerializeField]
    private GameObject removeDevicePopUp;

    [SerializeField]
    private InputField EditNameInputField;

    [SerializeField]
    private Button addButton;

    [SerializeField]
    private Image onOffImage;

    [SerializeField]
    private InputField AddNameInputField;

    void Start()
    {
        addNameInputField = AddNameInputField;
        editNameInputField = EditNameInputField;
        addDevicePopUp.SetActive(false);
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
        if (removeDevicePopUp.activeSelf)
        {
            removeDevicePopUp.SetActive(false);                         // Cancel Button (in Pop-Up)
        }
        else
        {
            removeDevicePopUp.SetActive(true);                          // Remove Button
        }
    }

    public void OnDeviceAdded(string deviceName)
    {
        editNameInputField.text = deviceName;
        addDevicePopUp.SetActive(false);
        addButton.gameObject.SetActive(false);
    }

    public void OnDeviceRemoved()
    {
        removeDevicePopUp.SetActive(false);
    }

    public void OnUpdateIsOn(bool isOn)
    {
        if (isOn)
        {
            onOffImage.color = Color.green;
        }
        else
        {
            onOffImage.color = Color.red;
        }
    }

    public void OnUpdateName(string name)
    {
        editNameInputField.text = name;
    }

    public void OnRegisteredDevice(bool registered)
    {
        if (registered)
        {
            addButton.gameObject.SetActive(false);
            deviceMenu.gameObject.SetActive(true);
            aRDisplay.SetActive(true);
        }
        else
        {
            addButton.gameObject.SetActive(true);
            deviceMenu.gameObject.SetActive(false);
            aRDisplay.SetActive(false);
        }
    }
}
