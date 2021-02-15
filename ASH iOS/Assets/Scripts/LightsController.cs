using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsController : MonoBehaviour
{
    [SerializeField]
    public GameObject light;

    public void turnOnAndOff()
    {
        if(light.active == true)
        {
            light.SetActive(false);
        }
        else
        {
            light.SetActive(true);
        }
    }
}
