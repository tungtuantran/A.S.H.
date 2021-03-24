using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LongPressMenuButtonController : MonoBehaviour
{
    //public UnityEvent onSelect;
    public UnityEvent onRelease;

    [SerializeField]
    private LongPressButton mainButton;

    [SerializeField]
    private GameObject subButtonCollection;

    [SerializeField]
    private LongPressSubButton cancelButton;

    private LongPressSubButton[] subButtons;

    void Awake()
    {
        subButtons = subButtonCollection.GetComponentsInChildren<LongPressSubButton>(true);     // optional parameter includes inactive components
        subButtonCollection.SetActive(false);
        cancelButton.gameObject.SetActive(false);
    }

    void Update()
    {
        foreach(LongPressSubButton subButton in subButtons){
            if (subButton.CurrentlyActive)
            {
                SetAllSubButtonsInactiveBesidesCurrentActiveSubButton();
            }
        }
    }

    public void ShowSubButtonCollection()
    {
        SetAllSubButtonsActive();
        subButtonCollection.SetActive(true);
    }

    public void ShowCancelButton()
    {
        cancelButton.gameObject.SetActive(true);
    }

    public void HideSubButtonCollection()
    {
        SetAllSubButtonsCurrentlyInactive();
        subButtonCollection.SetActive(false);

        cancelButton.CurrentlyActive = false;
        cancelButton.gameObject.SetActive(false);

        if (onRelease != null)
        {
            onRelease.Invoke();
        }
    }

    private void SetAllSubButtonsCurrentlyInactive()
    {
        foreach (LongPressSubButton subButton in subButtons)
        {
            subButton.CurrentlyActive = false;
        }
    }

    private void SetAllSubButtonsActive()
    {
        foreach (LongPressSubButton subButton in subButtons)
        {
            subButton.gameObject.SetActive(true);
        }
    }

    private void SetAllSubButtonsInactiveBesidesCurrentActiveSubButton()
    {
        foreach (LongPressSubButton subButton in subButtons)
        {
            if (!subButton.CurrentlyActive)
            {
                subButton.gameObject.SetActive(false);
            }
        }
    }
}
