using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnAllOffOnSystem : MonoBehaviour
{
    private const float RequiredDistance = 0.6f;

    public static bool active = true;

    [SerializeField]
    private DisplayToggle aRDisplayToggle;

    [SerializeField]
    private GameObject uIDisplay;

    [SerializeField]
    private GameObject allOffPopUp;

    public DisableUIInteractions disableUIInteractions;
    public float requiredHoldTime = 1.0f;

    [SerializeField]
    private DistanceCalculator distanceCalculator;

    private bool mouseDown;
    private float mouseDownTimer = 0.0f;

    private void Start()
    {
        allOffPopUp.SetActive(false);
        aRDisplayToggle.gameObject.SetActive(false);
        uIDisplay.SetActive(DeviceCollection.DeviceCollectionInstance.AllDevicesOff);
        SetActiveOfARDisplayToggle(DeviceCollection.DeviceCollectionInstance.AllDevicesOff);
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = true;
            distanceCalculator.Active = true;

            KeepDistanceInfront keepDistanceInfront = aRDisplayToggle.gameObject.GetComponent<KeepDistanceInfront>();
            if (keepDistanceInfront != null)
            {
                keepDistanceInfront.SetLine();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Reset();
        }

        if (mouseDown)
        {
            mouseDownTimer += Time.deltaTime;

            if (mouseDownTimer >= requiredHoldTime)
            {
                if (active)                                                     
                {
                    ShowARDisplayToggle();                                      // show ar display if it is not disabled
                    disableUIInteractions.DisableInteractions();                // disable swipe gesture to copy paste

                    float distance = distanceCalculator.forwardDistance * 100;
                    if (distance > RequiredDistance)
                    {

                        if (DeviceCollection.DeviceCollectionInstance.AllDevicesOff)
                        {
                            TurnAllOff(false);
                            Handheld.Vibrate();                                 // turn all on
                        }
                        else
                        {
                            if (!allOffPopUp.activeSelf)                        // prevents it from always show and hide per frame
                            {
                                ShowHideAllOffPopUp();
                                Handheld.Vibrate();
                            }
                        }
                    }
                }
            }
        }
    }
    
    public void ShowHideAllOffPopUp()
    {
        if (allOffPopUp.activeSelf)
        {
            allOffPopUp.SetActive(false);

        }
        else
        {
            allOffPopUp.SetActive(true);
        }
    }

    public void TurnAllOff(bool off)
    {
        DeviceCollection.DeviceCollectionInstance.AllDevicesOff = off;
        SetActiveOfARDisplayToggle(DeviceCollection.DeviceCollectionInstance.AllDevicesOff);
        uIDisplay.SetActive(DeviceCollection.DeviceCollectionInstance.AllDevicesOff);
        Reset();
    }

    public void Reset()
    {
        active = true;
        mouseDown = false;
        distanceCalculator.Active = false;
        mouseDownTimer = 0.0f;
        aRDisplayToggle.gameObject.SetActive(false);
        //fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
    }

    private void SetActiveOfARDisplayToggle(bool isActive)
    {
        if (isActive)
        {
            aRDisplayToggle.Active = false;

        }
        else
        {
            aRDisplayToggle.Active = true;
        }
    }

    private void ShowARDisplayToggle()
    {
        //if aRDisplayToggle not already activated
        if (!aRDisplayToggle.gameObject.activeSelf)
        {
            //aRDisplayToggle imitating rotation of camera
            aRDisplayToggle.transform.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z);
        }

        aRDisplayToggle.gameObject.SetActive(true);
    }
}
