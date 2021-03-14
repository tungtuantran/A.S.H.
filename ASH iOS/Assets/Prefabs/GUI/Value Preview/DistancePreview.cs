using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistancePreview : MonoBehaviour
{
    [SerializeField]
    protected Text distancePreview;

    public virtual void SetDistance(float distance)
    {
        distancePreview.text = distance.ToString();
    }

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
