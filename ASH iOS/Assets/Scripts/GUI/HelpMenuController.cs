using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenuController : MonoBehaviour
{

    [SerializeField]
    private Button helpButton;

    [SerializeField]
    private GameObject helpView;

    void Start()
    {
        helpView.SetActive(false);
    }

    public void ShowHideHelpView()
    {
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
