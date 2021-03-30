using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDistanceCalculator
{
    float upwardDistance { get; set; }
    float forwardDistance { get; set; }
    float sidewardDistance { get; set; }

    bool Active { get; set; }
}
