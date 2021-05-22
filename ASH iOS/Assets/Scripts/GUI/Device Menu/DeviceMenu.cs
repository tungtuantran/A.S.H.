using UnityEngine;
using UnityEngine.Events;

/*
 * Menu for the registered Devices.
 */
public class DeviceMenu : MonoBehaviour
{
    public UnityEvent onRelease;

    [SerializeField]
    private GameObject subButtonCollection;


    private MenuSubLongPressButton[] subButtons;

    void Awake()
    {
        subButtons = subButtonCollection.GetComponentsInChildren<MenuSubLongPressButton>(true);     // optional parameter includes inactive components
        subButtonCollection.SetActive(false);
    }

    void Update()
    {
        foreach(MenuSubLongPressButton subButton in subButtons){
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
        foreach (MenuSubLongPressButton subButton in subButtons)
        {
            subButton.CurrentlyActive = false;
        }
    }

    private void SetAllSubButtonsActive()
    {
        foreach (MenuSubLongPressButton subButton in subButtons)
        {
            subButton.gameObject.SetActive(true);
        }
    }

    private void SetAllSubButtonsInactiveBesidesCurrentActiveSubButton()
    {
        foreach (MenuSubLongPressButton subButton in subButtons)
        {
            if (!subButton.CurrentlyActive)
            {
                subButton.gameObject.SetActive(false);
            }
        }
    }
}
