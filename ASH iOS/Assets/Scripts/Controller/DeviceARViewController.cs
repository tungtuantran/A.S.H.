using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DeviceARViewController : MonoBehaviour
{
    private const string AddNameInputFieldPath = "Pop Up/Content/Name InputField";

    [SerializeField]
    private DeviceController deviceController;

    [SerializeField]
    private GameObject aRDisplay;

    [SerializeField]
    private GameObject addDevicePopUp;

    [SerializeField]
    private GameObject removeDevicePopUp;

    [SerializeField]
    private InputField editNameInputField;

    [SerializeField]
    private Button addButton;

    private InputField addNameInputField;
    protected Device trackedAndRegisteredDevice;
    private bool setNameOnFirstTrack;

    [SerializeField]
    private Text onOffText;

    void Start()
    {     
        addNameInputField = addDevicePopUp.transform.Find(AddNameInputFieldPath).gameObject.GetComponent<InputField>();
        addDevicePopUp.SetActive(false);
        removeDevicePopUp.SetActive(false);

        DisplayARView(false);
    }

    void Update()
    {
        trackedAndRegisteredDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(ImageTracking.deviceId);

        if (trackedAndRegisteredDevice != null)                         // if tracked Device is also registered in DeviceCollection
        {
            UpdateValueDisplay();

            if (!setNameOnFirstTrack)                                   // execute update name only on first track
            {
                setNameOnFirstTrack = true;
                UpdateName();
            }

            DisplayARView(true);
        }
        else
        {
            DisplayARView(false);
        }
    }

    protected virtual void UpdateValueDisplay()
    {
        if (trackedAndRegisteredDevice.isOn)
        {
            onOffText.text = "ON";
            onOffText.color = Color.green;

        }
        else
        {
            onOffText.text = "OFF";
            onOffText.color = Color.red;
        }
    }

    private void UpdateName()
    {

        editNameInputField.text = trackedAndRegisteredDevice._name;
    }

    private void DisplayARView(bool active)
    {
        if (active)
        {
            addButton.gameObject.SetActive(false);
            deviceController.gameObject.SetActive(true);
            aRDisplay.SetActive(true);
        }
        else
        {
            addButton.gameObject.SetActive(true);
            deviceController.gameObject.SetActive(false);
            aRDisplay.SetActive(false);

        }
    }

    public void EditName()
    {
        if (!string.IsNullOrWhiteSpace(editNameInputField.text))
        {
            deviceController.SelectDeviceByCurrentlyTrackedDevice();
            deviceController.EditNameOfSelectedDevice(editNameInputField.text);
        }

        UpdateName();
    }

    public void AddTrackedDevice()                     
    {
        string name = addNameInputField.text;
        if (!string.IsNullOrWhiteSpace(name))
        {
            deviceController.AddCurrentlyTrackedDevice(name);
            editNameInputField.text = name;
        }
        else
        {
            //TODO: fehlermeldung im UI
        }

        addDevicePopUp.SetActive(false);
        addButton.gameObject.SetActive(false);
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
