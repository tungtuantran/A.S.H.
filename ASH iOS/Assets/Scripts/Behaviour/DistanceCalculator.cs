using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceCalculator: MonoBehaviour
{
    public bool upwards;
    public DistancePreview distancePreview;

    private Transform aRCamera;
    private Vector3 supportVector;
    private Vector3 normalVector;

    public float distance { get; set; }
    private bool active;

    public bool Active
    {
        get { return active; }

        set {
            if (value)
            {
                Reset();
            }
            active = value;
        }
    }

    private void Update()
    {
        //set preview
        if (distancePreview != null)
        {
            distancePreview.SetActive(active);
            distancePreview.SetDistance(distance);
        }

        if (Active)
        {
            CalculateDistancBetweenCameraAndPlane();
        }
    }

    private void CalculateDistancBetweenCameraAndPlane()
    {
        float a = normalVector.x * supportVector.x + normalVector.y * supportVector.y + normalVector.z * supportVector.z;       //plane
        Vector3 p = aRCamera.position;                                                                                          // current camera position

        float realDistance = Mathf.Abs(normalVector.x * p.x + normalVector.y * p.y + normalVector.z * p.z - a) / Mathf.Sqrt(Mathf.Pow(normalVector.x, 2) + Mathf.Pow(normalVector.y, 2) + Mathf.Pow(normalVector.z, 2));
        distance = realDistance / 2;                                                            
    }

    private void Reset()
    {
        aRCamera = Camera.main.transform;
        supportVector = aRCamera.position;
        distance = 0;

        if (!upwards)
        {
            normalVector = aRCamera.forward;
        }
        else
        {
            normalVector = aRCamera.up;
        }
    }

}
