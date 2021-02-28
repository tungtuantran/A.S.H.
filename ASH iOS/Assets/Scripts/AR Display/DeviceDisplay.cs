using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DeviceDisplay : MonoBehaviour
{
    public static Device trackedAndRegisteredDevice { get; set; }
    //public static LongPressSubButton currentActiveSubButton { get; set; }

    [SerializeField]
    public LongPressButton selectOnOffDeviceButton;

    [SerializeField]
    public LongPressSubButton[] subButtons;

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

    public void SetAllSubButtonsActive()
    {
        foreach (LongPressSubButton subButton in subButtons)
        {
            subButton.gameObject.SetActive(true);
            subButton.currentlyActive = false;
        }
    }

    public void SetAllSubButtonsInactiveBesidesCurrentActiveSubButton()
    {
        foreach (LongPressSubButton subButton in subButtons)
        {
            if (!subButton.currentlyActive)
            {
                subButton.gameObject.SetActive(false);
            }
        }
    }
}
