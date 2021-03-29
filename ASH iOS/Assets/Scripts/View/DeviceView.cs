using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceView : MonoBehaviour, IDeviceView
{
    private const string AddNameInputFieldPath = "Pop Up/Content/Name InputField";

    [SerializeField]
    private DeviceMenu deviceMenu;

    [SerializeField]
    private GameObject aRDisplay;

    [SerializeField]
    private GameObject addDevicePopUp;

    [SerializeField]
    private GameObject removeDevicePopUp;

    [SerializeField]
    public InputField editNameInputField;

    [SerializeField]
    private Button addButton;

    [SerializeField]
    private Image onOffImage;

    [SerializeField]
    public InputField addNameInputField;

    protected Device trackedDevice;

    public Device TrackedDevice
    {
        get
        {
            return trackedDevice;
        }

        set
        {
            trackedDevice = value;
        }
    }

    protected bool setNameOnFirstTrack;

    public bool SetNameOnFirstTrack
    {
        get
        {
            return setNameOnFirstTrack;
        }

        set
        {
            setNameOnFirstTrack = value;
        }
    }

    void Start()
    {
        addNameInputField = addDevicePopUp.transform.Find(AddNameInputFieldPath).gameObject.GetComponent<InputField>();
        addDevicePopUp.SetActive(false);
        removeDevicePopUp.SetActive(false);

        DisplayARView(false);
    }

    void Update()
    {
        if (DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(trackedDevice.Id) != null)                         // if tracked device is registered in DeviceCollection
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

    public void OnEditDeviceName()
    {
        UpdateName();
    }

    public void OnDeviceRemoved()
    {
        removeDevicePopUp.SetActive(false);
    }

    private void UpdateName()
    {
        editNameInputField.text = trackedDevice.Name;
    }

    protected virtual void UpdateValueDisplay()
    {
        if (trackedDevice.IsOn)
        {
            onOffImage.color = Color.green;
        }
        else
        {
            onOffImage.color = Color.red;
        }
    }

    private void DisplayARView(bool active)
    {
        if (active)
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
