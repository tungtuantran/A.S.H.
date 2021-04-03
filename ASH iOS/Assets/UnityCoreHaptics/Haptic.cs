using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCoreHaptics;

public class Haptic : MonoBehaviour
{
    public float intensity = 1f;
    public float sharpness = 1f;
    public float duration = 0.5f;
    
    private bool createdEngineOnStart = false;

    void Start()
    {
        // Create haptic engine before using it to play haptics
        // this should only be called one tine
        UnityCoreHapticsProxy.CreateEngine();

        
        if (!createdEngineOnStart)
        {
            UnityCoreHapticsProxy.CreateEngine();
            createdEngineOnStart = true;
        }
    }

    public void PlayTransientHaptics()
    {
        UnityCoreHapticsProxy.PlayTransientHaptics(intensity, sharpness);
    }

    public void PlayContinuousHaptics()
    {
        UnityCoreHapticsProxy.PlayContinuousHaptics(intensity, sharpness, duration);
    }
}


