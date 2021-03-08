using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LongPressButtonController : MonoBehaviour
{
    public UnityEvent onSelect;
    public UnityEvent onRelease;

    [SerializeField]
    public LongPressButton mainButton;

    [SerializeField]
    public GameObject subButtonCollection;

    private LongPressSubButton[] subButtons;

    void Start()
    {
        subButtons = subButtonCollection.GetComponentsInChildren<LongPressSubButton>(true);     // optional paramter includes inactive components
        subButtonCollection.SetActive(false);
    }

    void Update()
    {
        foreach(LongPressSubButton subButton in subButtons){
            Debug.Log(subButton.gameObject.name + " " + subButton.currentlyActive);

            if (subButton.currentlyActive)
            {
                SetAllSubButtonsInactiveBesidesCurrentActiveSubButton();
            }
        }
    }

    public void ShowSubButtonCollection()
    {
        SetAllSubButtonsActive();
        subButtonCollection.SetActive(true);

        if (onSelect != null)
        {
            onSelect.Invoke();
        }
    }

    public void HideSubButtonCollection()
    {
        SetAllSubButtonsCurrentlyInactive();
        subButtonCollection.SetActive(false);

        if (onRelease != null)
        {
            onRelease.Invoke();
        }
    }

    private void SetAllSubButtonsCurrentlyInactive()
    {
        foreach (LongPressSubButton subButton in subButtons)
        {
            subButton.currentlyActive = false;
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
            if (!subButton.currentlyActive)
            {
                subButton.gameObject.SetActive(false);
            }
        }
    }
}
