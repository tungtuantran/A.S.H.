using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axes : MonoBehaviour
{
    private Vector3 CameraRotation;
    private Transform _camera;
    private bool rotate;

    [SerializeField]
    public GameObject axesCenter;

    [SerializeField]
    public GameObject xAxis;

    [SerializeField]
    public GameObject yAxis;

    [SerializeField]
    public GameObject zAxis;

    private void Awake()
    {
        // hide axes on default
        HideAxes();

        CameraRotation = Camera.main.transform.localEulerAngles;
        _camera = Camera.main.transform;

        // assign ar render camera to the "screen space - camera" canvas
        Canvas canvas = transform.GetComponentInParent<Canvas>() ?? null;
        if (canvas != null)
        {
            canvas.worldCamera = Camera.main;
            canvas.planeDistance = 0.02f;
        }
    }

    private void Update()
    {
        if (rotate)
        {
            RotateAxisWithCamera();
        }
    }

    private void RotateAxisWithCamera()
    {
        // delta = camera rotation - new camera rotation
        float deltaX = CameraRotation.x - _camera.transform.localEulerAngles.x;
        float deltaY = CameraRotation.y - _camera.transform.localEulerAngles.y;
        float deltaZ = CameraRotation.z - _camera.transform.localEulerAngles.z;

        axesCenter.transform.localEulerAngles = new Vector3(deltaX, deltaY, deltaZ);
    }

    public void HideAxesAndStopRotation()
    {
        rotate = false;
        HideAxes();
        axesCenter.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    public void ShowAxesAndStartRotation()
    {
        CameraRotation = Camera.main.transform.localEulerAngles;
        ShowAxes();
        rotate = true;
    }

    private void ShowAxes()
    {
        axesCenter.SetActive(true);
        xAxis.SetActive(true);
        yAxis.SetActive(true);
        zAxis.SetActive(true);
    }

    private void HideAxes()
    {
        axesCenter.SetActive(false);
        xAxis.SetActive(false);
        yAxis.SetActive(false);
        zAxis.SetActive(false);
    }

}
