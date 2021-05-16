using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownButton : MonoBehaviour
{
    public Image arrowDown;
    public Image arrowUp;

    [SerializeField]
    public GameObject body;

    [SerializeField]
    public GameObject footer;

    void Start()
    {
        if (arrowDown != null & arrowUp != null)
        {
            arrowDown.gameObject.SetActive(true);
            arrowUp.gameObject.SetActive(false);
        }

        Collapse();
    }

    // toggles beteween expand and collabse
    public void ExpandCollapse()
    {
        if (!body.activeSelf)
        {
            body.SetActive(true);
            footer.SetActive(true);
        }
        else
        {
            body.SetActive(false);
            footer.SetActive(false);
        }

        UpdateArrowImage();
    }

    // gets called by dropdown mananger
    public void Collapse()
    {
        body.SetActive(false);
        footer.SetActive(false);

        UpdateArrowImage();
    }

    private void UpdateArrowImage()
    {
        if (arrowDown != null & arrowUp != null)
        {
            if (!body.activeSelf)
            {
                arrowDown.gameObject.SetActive(true);
                arrowUp.gameObject.SetActive(false);
            }
            else
            {
                arrowDown.gameObject.SetActive(false);
                arrowUp.gameObject.SetActive(true);
            }
        }
    }
}
