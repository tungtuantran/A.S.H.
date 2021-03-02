using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARViewController : MonoBehaviour
{
    protected const string ADD_NAME_INPUTFIELD_PATH = "Pop Up/Content/Name InputField";
    protected InputField addNameInputField;

    private Device trackedAndRegisteredDevice;

    [SerializeField]
    public DeviceController deviceController;

    [SerializeField]
    public Button addDeviceButton;

    [SerializeField]
    public GameObject addDevicePopUp;

    [SerializeField]
    public GameObject removeDevicePopUp;

    void Start()
    {
        addNameInputField = addDevicePopUp.transform.Find(ADD_NAME_INPUTFIELD_PATH).gameObject.GetComponent<InputField>();

        addDeviceButton.gameObject.SetActive(false);
        addDevicePopUp.SetActive(false);
        removeDevicePopUp.SetActive(false);
        deviceController.gameObject.SetActive(false);
    }

    void Update()
    {
        trackedAndRegisteredDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(ImageTracking.deviceId);

        if (trackedAndRegisteredDevice != null)                         // if tracked Device is also registered in DeviceCollection
        {
            addDeviceButton.gameObject.SetActive(false);
            deviceController.gameObject.SetActive(true);
        }
        else
        {
            addDeviceButton.gameObject.SetActive(true);
            deviceController.gameObject.SetActive(false);

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
