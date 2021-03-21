using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPreview : MonoBehaviour
{
    [SerializeField]
    private Image colorPreview;

    public void ShowPreview(float upwardDistane, float forwardDistance, float sidewardDistance)
    {
        //colorPreview.color = color;
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
