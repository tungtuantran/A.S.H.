using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxesController : MonoBehaviour
{
    private Vector3 CameraRotation;
    private Transform _camera;
    private bool rotate;

    [SerializeField]
    public GameObject axes;

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
            Debug.Log("canvas found");
            canvas.worldCamera = Camera.main;
            canvas.planeDistance = 0.02f;
            Debug.Log("canvas plane distance: " + canvas.planeDistance);
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

        axes.transform.localEulerAngles = new Vector3(deltaX, deltaY, deltaZ);
    }

    public void HideAxesAndStopRotation()
    {
        rotate = false;
        HideAxes();
        axes.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

        Debug.Log("hide axes and stop rotation called");

    }

    public void ShowAxesAndStartRotation()
    {
        CameraRotation = Camera.main.transform.localEulerAngles;
        ShowAxes();
        rotate = true;

        Debug.Log("show axes and start rotation called");

    }

    private void ShowAxes()
    {
        axes.SetActive(true);
        xAxis.SetActive(true);
        yAxis.SetActive(true);
        zAxis.SetActive(true);
    }

    private void HideAxes()
    {
        axes.SetActive(false);
        xAxis.SetActive(false);
        yAxis.SetActive(false);
        zAxis.SetActive(false);
    }

    /*
    private static Vector3 CameraRotation;
    private static Transform _camera;
    private static bool show;

    [SerializeField]
    private GameObject axes;

    [SerializeField]
    private GameObject xAxis;

    [SerializeField]
    private GameObject yAxis;

    [SerializeField]
    private GameObject zAxis;


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
        if (show)
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

        axes.transform.localEulerAngles = new Vector3(deltaX, deltaY, deltaZ);
    }

    public void HideAxesAndStopRotation()
    {
        show = false;
        HideAxes();
        axes.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

        Debug.Log("hide axes and stop rotation called");

    }

    public static void Show()
    {
        show = true;
    }

    public void ShowAxesAndStartRotation()
    {
        CameraRotation = Camera.main.transform.localEulerAngles;
        ShowAxes();
        show = true;

        Debug.Log("show axes and start rotation called");

    }

    private void ShowAxes()
    {
        axes.SetActive(true);
        xAxis.SetActive(true);
        yAxis.SetActive(true);
        zAxis.SetActive(true);
    }

    private void HideAxes()
    {
        axes.SetActive(false);
        xAxis.SetActive(false);
        yAxis.SetActive(false);
        zAxis.SetActive(false);
    }
    */

}
