using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARViewController : MonoBehaviour
{
    private const string ADD_NAME_INPUTFIELD_PATH = "Pop Up/Content/Name InputField";
    private const string VALUE_DISPLAY_TEXT_PATH = "Scroll View/Viewport/Content/Value Display Text";
    private const string NAME_DISPLAY_TEXT_PATH = "Text";


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

    [SerializeField]
    private InputField editNameInputField;

    private InputField addNameInputField;
    private Text valueDisplayText;
    private Text nameDisplayText;
    private Device trackedAndRegisteredDevice;

    void Start()
    {
        Debug.Log("start called");

        addNameInputField = addDevicePopUp.transform.Find(ADD_NAME_INPUTFIELD_PATH).gameObject.GetComponent<InputField>();
        valueDisplayText = valueDisplay.transform.Find(VALUE_DISPLAY_TEXT_PATH).gameObject.GetComponent<Text>();
        nameDisplayText = editNameInputField.transform.Find(NAME_DISPLAY_TEXT_PATH).gameObject.GetComponent<Text>();

        //nameDisplayText.text = trackedAndRegisteredDevice._name;            //set name display on start

        addDeviceButton.gameObject.SetActive(false);
        addDevicePopUp.SetActive(false);
        removeDevicePopUp.SetActive(false);
        deviceController.gameObject.SetActive(false);
        valueDisplay.SetActive(false);
        editNameInputField.gameObject.SetActive(false);

        Debug.Log("pop ups set false");
    }

    void Update()
    {
        trackedAndRegisteredDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(ImageTracking.deviceId);

        if (trackedAndRegisteredDevice != null)                         // if tracked Device is also registered in DeviceCollection
        {
            
            addDeviceButton.gameObject.SetActive(false);
            deviceController.gameObject.SetActive(true);

            UpdateValueDisplay();
            UpdateName();

            valueDisplay.SetActive(true);
            editNameInputField.gameObject.SetActive(true);

        }
        else
        {
            addDeviceButton.gameObject.SetActive(true);
            deviceController.gameObject.SetActive(false);
            valueDisplay.SetActive(false);
            editNameInputField.gameObject.SetActive(false);

        }
    }

    private void UpdateValueDisplay()
    {
        valueDisplayText.text = trackedAndRegisteredDevice.DeviceValuesToString();
    }

    public void UpdateName()
    {
        if (nameDisplayText.text.Equals(""))
        {
            nameDisplayText.text = trackedAndRegisteredDevice._name;
        }
        else
        {
            deviceController.SelectDeviceByCurrentlyTrackedDevice();
            deviceController.EditNameOfSelectedDevice(nameDisplayText.text);
        }
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

    public void RemoveDevice()
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
        deviceController.SelectDeviceByCurrentlyTrackedDevice();        //selects device by current track

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
