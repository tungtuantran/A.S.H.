using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DeviceDisplay : MonoBehaviour
{
    public static Device trackedAndRegisteredDevice { get; set; }

    [SerializeField]
    public Button selectOnOffDeviceButton;

    //[SerializeField]
    //public Text TextOfOnOffAndSelectDeviceButton;

    //[SerializeField]
    //public GameObject deviceControllerGameObject;

    [SerializeField]
    public DeviceController deviceController;

    [SerializeField]
    public Button addDeviceButton;

    [SerializeField]
    public Button removeDeviceButton;

    [SerializeField]
    public GameObject addDevicePopUp;

    //[SerializeField]
    //public InputField addDeviceNameInputField;


    [SerializeField]
    public GameObject removeDevicePopUp;
}
