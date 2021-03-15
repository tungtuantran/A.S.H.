using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputEnableButton : MonoBehaviour
{
    [SerializeField]
    private Text textInput;

    [SerializeField]
    private Button button;

    void Update()
    {
        if (string.IsNullOrWhiteSpace(textInput.text))
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }
}
