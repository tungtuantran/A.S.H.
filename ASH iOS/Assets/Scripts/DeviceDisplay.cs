using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DeviceDisplay : MonoBehaviour
{
    public static Device trackedAndRegisteredDevice { get; set; }

    [SerializeField]
    public GameObject addDeviceButton;

    [SerializeField]
    public GameObject aRDeviceController;

    [SerializeField]
    public GameObject addDevicePopUp;

    [SerializeField]
    public Text addDeviceName;

    [SerializeField]
    public GameObject removeDeviceButton;

    [SerializeField]
    public GameObject removeDevicePopUp;
}
