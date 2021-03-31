using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisableUIInteractions : MonoBehaviour
{
    public bool longpressToTurnAllOffOn;
    public bool collideToSelectDevice;

    public void DisableInteractions()
    {
        if (longpressToTurnAllOffOn)
        {
            TurnAllOffOnSystem.active = false;
        }

        if (collideToSelectDevice)
        {
            SelectDevice.active = false;
        }
    }

}
