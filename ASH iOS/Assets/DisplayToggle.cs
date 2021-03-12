using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayToggle : MonoBehaviour
{
    [SerializeField]
    private GameObject activeDisplay;

    [SerializeField]
    private GameObject inactiveDisplay;

    public bool activeOnDefault;

    private bool active;
    public bool Active
    {
        get{
            return active;
        }

        set{
            active = value;

            if (active)
            {
                activeDisplay.SetActive(true);
                inactiveDisplay.SetActive(false);
            }
            else
            {
                activeDisplay.SetActive(false);
                inactiveDisplay.SetActive(true);
            }
        }
    }
    

    void Start()
    {
        active = activeOnDefault;
    }

}
