using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject overview;

    public void showGeneralOverview()
    {
        overview.SetActive(true);
    }

    public void showGroupOverview()
    {
        overview.SetActive(true);
    }

    public void Leave()
    {
        overview.SetActive(false);

    }
}
