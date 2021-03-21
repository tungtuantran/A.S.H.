using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceCalculator: MonoBehaviour
{
    public DistancePreview distancePreview;

    private Transform aRCamera;
    private Vector3 supportVector;
    private Vector3 upwardVector;
    private Vector3 forwardVector;
    private Vector3 sidewardVector;

    public float upwardDistance { get; set; }
    public float forwardDistance { get; set; }
    public float sidewardDistance { get; set; }

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
        //show preview
        /*
        if (distancePreview != null)
        {
            distancePreview.SetActive(active);
            distancePreview.SetDistance(distance);
        }
        */

        if (Active)
        {
            upwardDistance = CalculateDistancBetweenCameraAndPlane(upwardVector);
            forwardDistance = CalculateDistancBetweenCameraAndPlane(forwardVector);
            sidewardDistance = CalculateDistancBetweenCameraAndPlane(sidewardVector);
        }
    }

    private float CalculateDistancBetweenCameraAndPlane(Vector3 normalVector)
    {
        // calculate a with plane equation
        float a = normalVector.x * supportVector.x + normalVector.y * supportVector.y + normalVector.z * supportVector.z;

        // calculate vector p -> urrent camera position
        Vector3 p = aRCamera.position;                                                                                          

        // calculate distance between plane and point vector p
        float realDistance = Mathf.Abs(normalVector.x * p.x + normalVector.y * p.y + normalVector.z * p.z - a) / Mathf.Sqrt(Mathf.Pow(normalVector.x, 2) + Mathf.Pow(normalVector.y, 2) + Mathf.Pow(normalVector.z, 2));

        // distance is real distance devided by 4
        return realDistance / 4;                                                            
    }

    private void Reset()
    {
        //set new support vector of plane
        aRCamera = Camera.main.transform;
        supportVector = aRCamera.position;

        //reset distance to 0
        upwardDistance = 0;
        forwardDistance = 0;
        sidewardDistance = 0;

        upwardVector = aRCamera.up;
        forwardVector = aRCamera.forward;
        sidewardVector = aRCamera.right;
    }

}

[Serializable]
public class NoDirectionException : Exception
{
    public NoDirectionException()
    {
    }

    public NoDirectionException(string message) : base(message)
    {
    }

    public NoDirectionException(string message, Exception e) : base(message, e)
    {

    }
}