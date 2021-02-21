using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DeviceDisplay : MonoBehaviour
{
    public static Device trackedAndRegisteredDevice { get; set; }

    [SerializeField]
    public GameObject OnOffAndSelectDeviceButton;

    [SerializeField]
    public DeviceController deviceController;

    [SerializeField]
    public GameObject deviceControllerGameObject;

    [SerializeField]
    public GameObject addDeviceButton;

    [SerializeField]
    public GameObject addDevicePopUp;

    [SerializeField]
    public InputField addDeviceNameInputField;

    [SerializeField]
    public GameObject removeDeviceButton;

    [SerializeField]
    public GameObject removeDevicePopUp;
}
