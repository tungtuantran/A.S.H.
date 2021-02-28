using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VectorDistanceCalculator: MonoBehaviour
{
    private Transform aRCamera;

    private Vector3 supportVector;
    private Vector3 normalVector;

    public float distance { get; set; }

    private bool calculate;

    public void StartCalculatingDistance()
    {
        aRCamera = Camera.main.transform;
        supportVector = aRCamera.position;
        normalVector = aRCamera.forward;
        calculate = true;
    }

    public void StopCalculatingDistance()
    {
        calculate = false;
    }

    private void Update()
    {
        if (calculate)
        {
            CalculateDistancBetweenCameraAndPlane();
        }
    }

    private void CalculateDistancBetweenCameraAndPlane()
    {
        float a = normalVector.x * supportVector.x + normalVector.y * supportVector.y + normalVector.z * supportVector.z;       //plane
        Vector3 p = aRCamera.position;                                                          // current camera position

        float realDistance = Mathf.Abs(normalVector.x * p.x + normalVector.y * p.y + normalVector.z * p.z - a) / Mathf.Sqrt(Mathf.Pow(normalVector.x, 2) + Mathf.Pow(normalVector.y, 2) + Mathf.Pow(normalVector.z, 2));
        distance = realDistance / 2;                                                            
    }
}
