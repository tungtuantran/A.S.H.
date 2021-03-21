using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DistancePreview : MonoBehaviour
{
    public abstract void ShowPreview(float upwardDistane, float forwardDistance, float sidewardDistance);

    public void SetActive(bool active)
    {
        if (active)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
