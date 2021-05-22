using UnityEngine;
using UnityEngine.UI;

/*
 * Controller for the Instruction.
 */
public class InstructionController : MonoBehaviour
{

    [SerializeField]
    private Button instructionButton;

    [SerializeField]
    private Image buttonIconImage;

    [SerializeField]
    private Sprite instructionIcon;

    [SerializeField]
    private Sprite closeIcon;

    [SerializeField]
    private GameObject instructionView;

    void Start()
    {
        instructionView.SetActive(false);
        buttonIconImage.sprite = instructionIcon;
    }

    public void ShowHideInstructionView()
    {
        if (instructionView.activeSelf)
        {
            instructionView.SetActive(false);
            buttonIconImage.sprite = instructionIcon;
        }
        else
        {
            instructionView.SetActive(true);
            buttonIconImage.sprite = closeIcon;
        }
    }
}
