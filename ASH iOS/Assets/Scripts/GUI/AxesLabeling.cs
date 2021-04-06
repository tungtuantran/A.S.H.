using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxesLabeling : MonoBehaviour
{
    [SerializeField]
    private Text xAxisLabel;

    [SerializeField]
    private Text yAxisLabel;

    [SerializeField]
    private Text zAxisLabel;

    [SerializeField]
    private Image xAxisColorImage;

    [SerializeField]
    private Image yAxisColorImage;

    [SerializeField]
    private Image zAxisColorImage;

    public void ShowLabelOfXAxis()
    {
        xAxisLabel.gameObject.SetActive(true);
        xAxisColorImage.gameObject.SetActive(true);
    }

    public void ShowLabelOfYAxis()
    {
        yAxisLabel.gameObject.SetActive(true);
        yAxisColorImage.gameObject.SetActive(true);
    }

    public void ShowLabelOfZAxis()
    {
        zAxisLabel.gameObject.SetActive(true);
        zAxisColorImage.gameObject.SetActive(true);
    }

    public void HideLabelOfXAxis()
    {
        xAxisLabel.gameObject.SetActive(false);
        xAxisColorImage.gameObject.SetActive(false);
    }

    public void HideLabelOfYAxis()
    {
        yAxisLabel.gameObject.SetActive(false);
        yAxisColorImage.gameObject.SetActive(false);
    }

    public void HideLabelOfZAxis()
    {
        zAxisLabel.gameObject.SetActive(false);
        zAxisColorImage.gameObject.SetActive(false);
    }

    public void SetLabelTextOfXAxis(string labelText)
    {
        xAxisLabel.text = labelText;
    }

    public void SetLabelTextOfYAxis(string labelText)
    {
        yAxisLabel.text = labelText;
    }

    public void SetLabelTextOfZAxis(string labelText)
    {
        zAxisLabel.text = labelText;
    }
}
