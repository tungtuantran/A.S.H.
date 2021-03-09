using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenuController : MonoBehaviour
{
    [SerializeField]
    public DisableUIInteractions disableUIInteractions;

    [SerializeField]
    public Button helpButton;

    [SerializeField]
    public GameObject helpView;

    void Start()
    {
        helpView.SetActive(false);
    }

    private void Update()
    {
        if (helpView.activeSelf)
        {
            CopyPasteSystem.swipeToCopyPaste = false;
        }
    }

    public void ShowHideHelpView()
    {
        disableUIInteractions.DisableInteractions();

        if (helpView.activeSelf)
        {
            helpView.SetActive(false);
        }
        else
        {
            helpView.SetActive(true);
        }

    }
}
