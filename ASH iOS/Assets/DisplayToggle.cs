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

    public bool active { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        active = activeOnDefault;
    }

    // Update is called once per frame
    void Update()
    {
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
