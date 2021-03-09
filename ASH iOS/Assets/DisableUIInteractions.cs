﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisableUIInteractions : MonoBehaviour
{
    public bool longpressToTurnAllOffOn;
    public bool swipeToCopyPaste;

    public void DisableInteractions()
    {
        if (longpressToTurnAllOffOn)
        {
            TurnAllOffOnSystem.longpressToTurnAllOffOn = false;
        }
        
        if(swipeToCopyPaste){
            CopyPasteSystem.swipeToCopyPaste = false;
        }
    }

}