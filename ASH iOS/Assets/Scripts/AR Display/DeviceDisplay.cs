using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DeviceDisplay : MonoBehaviour
{
    protected const string ADD_NAME_INPUTFIELD_PATH = "Pop Up/Content/Name InputField";
    protected InputField addNameInputField;
    protected Device trackedAndRegisteredDevice;

    [SerializeField]
    public DeviceController deviceController;

    [SerializeField]
    public Button addDeviceButton;

    [SerializeField]
    public Button removeDeviceButton;

    [SerializeField]
    public GameObject addDevicePopUp;

    [SerializeField]
    public GameObject removeDevicePopUp;

    protected abstract void DisplayPropertiesOfTrackedAndRegisteredDevice();

    protected void SetTrackedAndRegisteredDevice()
    {
        trackedAndRegisteredDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(ImageTracking.deviceId);
    }
}
