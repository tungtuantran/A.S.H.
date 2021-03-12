using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAllOffOnSystem : MonoBehaviour
{
    public static bool active = true;

    public GameObject turnAllOffOnImage;
    public DisableUIInteractions disableUIInteractions;
    public float requiredHoldTime = 1.0f;

    [SerializeField]
    private DistanceCalculator distanceCalculator;

    private bool mouseDown;
    private float mouseDownTimer = 0.0f;


    private void Start()
    {
        turnAllOffOnImage.SetActive(false);
    }

    private void Update()
    {
    
        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = true;
            distanceCalculator.Active = true;

            turnAllOffOnImage.SetActive(true);
            KeepDistanceInfront keepDistanceInfront = turnAllOffOnImage.GetComponent<KeepDistanceInfront>();
            keepDistanceInfront.SetDirection();
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
                disableUIInteractions.DisableInteractions();

                float distance = distanceCalculator.Distance * 100;
                if (distance > 0.3f)
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
        turnAllOffOnImage.SetActive(false);
        //fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
    }
}
