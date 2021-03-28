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
    private Image onOffImage;

    void Start()
    {     
        addNameInputField = addDevicePopUp.transform.Find(AddNameInputFieldPath).gameObject.GetComponent<InputField>();
        addDevicePopUp.SetActive(false);
        removeDevicePopUp.SetActive(false);

        DisplayARView(false);
    }

    void Update()
    {
        trackedAndRegisteredDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(deviceController.Device.Id);

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
        if (trackedAndRegisteredDevice.IsOn)
        {
            onOffImage.color = Color.green;
        }
        else
        {
            onOffImage.color = Color.red;
        }
    }

    private void UpdateName()
    {

        editNameInputField.text = trackedAndRegisteredDevice.Name;
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
        deviceController.EditNameOfDevice(editNameInputField.text);

        UpdateName();
    }

    public void AddDevice()                     
    {
        string name = addNameInputField.text;

        deviceController.AddDevice(name);
        editNameInputField.text = name;

        addDevicePopUp.SetActive(false);
        addButton.gameObject.SetActive(false);
    }

    public void RemoveDevice()
    {
        deviceController.RemoveDevice();
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

}
