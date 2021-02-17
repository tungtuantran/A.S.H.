using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeviceDisplay : MonoBehaviour
{
    public Device trackedDevice { get; set; }

    [SerializeField]
    public GameObject aRController;
}
