using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DeviceDisplay : MonoBehaviour
{
    public static Device trackedAndRegisteredDevice { get; set; }

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
}
