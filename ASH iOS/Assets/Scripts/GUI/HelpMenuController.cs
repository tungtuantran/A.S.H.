using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenuController : MonoBehaviour
{
    [SerializeField]
    private DisableUIInteractions disableUIInteractions;

    [SerializeField]
    private Button helpButton;

    [SerializeField]
    private GameObject helpView;

    void Start()
    {
        helpView.SetActive(false);
    }

    private void Update()
    {
        if (helpView.activeSelf)
        {
            disableUIInteractions.DisableInteractions();
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
