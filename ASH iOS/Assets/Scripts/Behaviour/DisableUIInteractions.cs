using UnityEngine;

/*
 * Disables certain UI interactions like:
 * Long Press Gesture to turn all device off/on,
 * Colliding with device to select it (Raycasting).
 */
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
