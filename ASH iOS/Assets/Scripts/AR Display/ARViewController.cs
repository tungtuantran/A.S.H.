using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARViewController : MonoBehaviour
{
    private const string AddNameInputFieldPath = "Pop Up/Content/Name InputField";
    private const string ValueTextPath = "Viewport/Content/Values Text";

    [SerializeField]
    private DeviceController deviceController;

    [SerializeField]
    private GameObject addDevicePopUp;

    [SerializeField]
    private GameObject removeDevicePopUp;

    [SerializeField]
    private GameObject valuesScrollViewGO;

    [SerializeField]
    private InputField editNameInputField;

    [SerializeField]
    private Button addButton;

    [SerializeField]
    private Button deleteButton;

    private InputField addNameInputField;
    private Text valuesText;
    private Device trackedAndRegisteredDevice;
    private bool setNameOnFirstTrack;

    void Start()
    {     
        valuesText = valuesScrollViewGO.transform.Find(ValueTextPath).gameObject.GetComponent<Text>();
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

            if (!setNameOnFirstTrack)                                   //execute update name only on first track
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

    private void UpdateValueDisplay()
    {
        valuesText.text = trackedAndRegisteredDevice.DeviceValuesToString();
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

            editNameInputField.gameObject.SetActive(true);
            deleteButton.gameObject.SetActive(true);
            valuesScrollViewGO.SetActive(true);
        }
        else
        {
            addButton.gameObject.SetActive(true);
            deviceController.gameObject.SetActive(false);

            editNameInputField.gameObject.SetActive(false);
            deleteButton.gameObject.SetActive(false);
            valuesScrollViewGO.SetActive(false);
        }
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
