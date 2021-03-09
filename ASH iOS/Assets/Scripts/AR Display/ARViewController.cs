using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARViewController : MonoBehaviour
{
    private const string ADD_NAME_INPUTFIELD_PATH = "Pop Up/Content/Name InputField";
    private const string VALUE_DISPLAY_TEXT_PATH = "Scroll View/Viewport/Content/Value Display Text";

    [SerializeField]
    private DeviceController deviceController;

    [SerializeField]
    private Button addDeviceButton;

    [SerializeField]
    private GameObject addDevicePopUp;

    [SerializeField]
    private GameObject removeDevicePopUp;

    [SerializeField]
    private GameObject valueDisplay;

    private InputField addNameInputField;
    private Text valueDisplayText;
    private Device trackedAndRegisteredDevice;

    void Start()
    {
        addNameInputField = addDevicePopUp.transform.Find(ADD_NAME_INPUTFIELD_PATH).gameObject.GetComponent<InputField>();
        valueDisplayText = valueDisplay.transform.Find(VALUE_DISPLAY_TEXT_PATH).gameObject.GetComponent<Text>();

        addDeviceButton.gameObject.SetActive(false);
        addDevicePopUp.SetActive(false);
        removeDevicePopUp.SetActive(false);
        deviceController.gameObject.SetActive(false);
        valueDisplay.SetActive(false);
    }

    void Update()
    {
        trackedAndRegisteredDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(ImageTracking.deviceId);

        if (trackedAndRegisteredDevice != null)                         // if tracked Device is also registered in DeviceCollection
        {
            
            addDeviceButton.gameObject.SetActive(false);
            deviceController.gameObject.SetActive(true);

            UpdateValueDisplay();
            valueDisplay.SetActive(true);
            
        }
        else
        {
            addDeviceButton.gameObject.SetActive(true);
            deviceController.gameObject.SetActive(false);
            valueDisplay.SetActive(false);

        }
    }

    private void UpdateValueDisplay()
    {
        valueDisplayText.text = trackedAndRegisteredDevice.DeviceValuesToString();
    }

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
