using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARViewController : MonoBehaviour
{
    private const string ADD_NAME_INPUTFIELD_PATH = "Pop Up/Content/Name InputField";
    private const string VALUES_SCROLLVIEW_PATH = "Values Scroll View";
    private const string VALUE_TEXT_PATH = "Viewport/Content/Values Text";
    private const string EDIT_NAME_INPUTFIELD_PATH = "Edit Name InputField";
    private const string ADD_BUTTON_PATH = "Add Button";
    private const string DELETE_BUTTON_PATH = "Delete Button";

    [SerializeField]
    private DeviceController deviceController;

    /*
    [SerializeField]
    private GameObject aRView;
    */

    [SerializeField]
    private GameObject addDevicePopUp;

    [SerializeField]
    private GameObject removeDevicePopUp;

    [SerializeField]
    private GameObject valuesScrollViewGO;

    private Text valuesText;

    [SerializeField]
    private InputField editNameInputField;

    private InputField addNameInputField;

    [SerializeField]
    private Button addButton;

    [SerializeField]
    private Button deleteButton;

    private Device trackedAndRegisteredDevice;
    private bool setNameOnFirstTrack;

    void Start()
    {
        
        //valuesScrollViewGO = aRView.transform.Find(VALUES_SCROLLVIEW_PATH).gameObject;
        valuesText = valuesScrollViewGO.transform.Find(VALUE_TEXT_PATH).gameObject.GetComponent<Text>();
        addNameInputField = addDevicePopUp.transform.Find(ADD_NAME_INPUTFIELD_PATH).gameObject.GetComponent<InputField>();
        //editNameInputField = aRView.transform.Find(EDIT_NAME_INPUTFIELD_PATH).gameObject.GetComponent<InputField>();
        //addButton = aRView.transform.Find(ADD_BUTTON_PATH).gameObject.GetComponent<Button>();
        //deleteButton = aRView.transform.Find(DELETE_BUTTON_PATH).gameObject.GetComponent<Button>();

        deviceController.gameObject.SetActive(false);
        addDevicePopUp.SetActive(false);
        removeDevicePopUp.SetActive(false);

        addButton.gameObject.SetActive(false);
        editNameInputField.gameObject.SetActive(false);
        deleteButton.gameObject.SetActive(false);
        valuesScrollViewGO.SetActive(false);
    }

    void Update()
    {
        trackedAndRegisteredDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(ImageTracking.deviceId);

        if (trackedAndRegisteredDevice != null)                         // if tracked Device is also registered in DeviceCollection
        {      
            addButton.gameObject.SetActive(false);
            deviceController.gameObject.SetActive(true);

            UpdateValueDisplay();

            if (!setNameOnFirstTrack)                                   //execute update name only on first track
            {
                setNameOnFirstTrack = true;
                UpdateName();
            }

            editNameInputField.gameObject.SetActive(true);
            deleteButton.gameObject.SetActive(true);
            valuesScrollViewGO.SetActive(true);

        }
        else
        {
            addButton.gameObject.SetActive(true);
            Debug.Log("add button state: " + addButton.gameObject.activeSelf);
            deviceController.gameObject.SetActive(false);

            editNameInputField.gameObject.SetActive(false);
            deleteButton.gameObject.SetActive(false);
            valuesScrollViewGO.SetActive(false);
        }
    }

    private void UpdateValueDisplay()
    {
        valuesText.text = trackedAndRegisteredDevice.DeviceValuesToString();
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
