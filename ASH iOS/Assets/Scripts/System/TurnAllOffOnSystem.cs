using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnAllOffOnSystem : MonoBehaviour
{
    public static bool active = true;

    [SerializeField]
    private DisplayToggle aRDisplay;

    [SerializeField]
    private DisplayToggle uIDisplay;

    public DisableUIInteractions disableUIInteractions;
    public float requiredHoldTime = 1.0f;

    [SerializeField]
    private DistanceCalculator distanceCalculator;

    private bool mouseDown;
    private float mouseDownTimer = 0.0f;


    private void Start()
    {
        SetActiveOfDisplayToggles(DeviceCollection.DeviceCollectionInstance.allDevicesOff);
        aRDisplay.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = true;
            distanceCalculator.Active = true;

            KeepDistanceInfront keepDistanceInfront = aRDisplay.gameObject.GetComponent<KeepDistanceInfront>();
            if (keepDistanceInfront != null)
            {
                keepDistanceInfront.SetDirection();
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
                if (active)                                             // show ar display if it is not disabled
                {
                    ShowARDisplay();
                }

                disableUIInteractions.DisableInteractions();

                float distance = distanceCalculator.distance * 100;
                if (distance > 0.6f)                                    //old max distance: 0.3f
                {
                    TurnAllOffOn();
                }
            }
            //fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
        }
    }

    private void TurnAllOffOn()
    {
        if (active)
        {
            if (DeviceCollection.DeviceCollectionInstance.allDevicesOff)
            {
                DeviceCollection.DeviceCollectionInstance.allDevicesOff = false;
            }
            else
            {
                DeviceCollection.DeviceCollectionInstance.allDevicesOff = true;
            }

            SetActiveOfDisplayToggles(DeviceCollection.DeviceCollectionInstance.allDevicesOff);
            Handheld.Vibrate();
        }

        Reset();
    }

    public void Reset()
    {
        active = true;
        mouseDown = false;
        distanceCalculator.Active = false;
        mouseDownTimer = 0.0f;
        aRDisplay.gameObject.SetActive(false);
        //fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
    }

    private void SetActiveOfDisplayToggles(bool active)
    {
        if (DeviceCollection.DeviceCollectionInstance.allDevicesOff)
        {
            uIDisplay.Active = false;
            aRDisplay.Active = false;

        }
        else
        {
            uIDisplay.Active = true;
            aRDisplay.Active = true;
        }
    }

    private void ShowARDisplay()
    {
        //if arDisplay not already activated
        if (!aRDisplay.gameObject.activeSelf)
        {
            //aRDisplay imitating rotation of camera
            aRDisplay.transform.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z);
        }

        aRDisplay.gameObject.SetActive(true);
    }
}
