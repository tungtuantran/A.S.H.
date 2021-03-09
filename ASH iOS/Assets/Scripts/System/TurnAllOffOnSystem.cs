using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAllOffOnSystem : MonoBehaviour
{
    [SerializeField]
    public DistanceCalculator distanceCalculator;

    public DisableUIInteractions disableUIInteractions;

    public float requiredHoldTime = 1.0f;

    public static bool longpressToTurnAllOffOn = true;
    private bool mouseDown;
    private float mouseDownTimer = 0.0f;

    private void Update()
    {
    
        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = true;
            distanceCalculator.active = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Reset();
        }

        if (mouseDown)
        {
            Debug.Log("got in");

            mouseDownTimer += Time.deltaTime;

            if (mouseDownTimer >= requiredHoldTime)
            {
                disableUIInteractions.DisableInteractions();

                float distance = distanceCalculator.distance * 100;
                if (distance > 0.3f)
                {
                    TurnAllOffOn();
                    Debug.Log("turned on/off");
                }
            }
            //fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
        }
    }

    private void TurnAllOffOn()
    {
        if (longpressToTurnAllOffOn)
        {
            if (DeviceCollection.DeviceCollectionInstance.allDevicesOff)
            {
                DeviceCollection.DeviceCollectionInstance.allDevicesOff = false;
            }
            else
            {
                DeviceCollection.DeviceCollectionInstance.allDevicesOff = true;
            }
            Handheld.Vibrate();
            Debug.Log("All Devices Off State: " + DeviceCollection.DeviceCollectionInstance.allDevicesOff);
        }

        Reset();
    }

    public void Reset()
    {
        longpressToTurnAllOffOn = true;
        mouseDown = false;
        distanceCalculator.active = false;
        mouseDownTimer = 0.0f;
        //fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
    }
}
