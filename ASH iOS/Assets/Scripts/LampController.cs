using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampController : MonoBehaviour
{
    [SerializeField]
    public GameObject lamp;

    public void turnOnAndOff()
    {
        if (lamp.active == true)
        {
            lamp.SetActive(false);
        }
        else
        {
            lamp.SetActive(true);
        }
    }
}
