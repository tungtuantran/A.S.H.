using UnityEngine;
using UnityEngine.UI;

/*
 * Enables Button if TextInput is not empty.
 */
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
