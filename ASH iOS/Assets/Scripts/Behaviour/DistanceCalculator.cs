using System;
using UnityEngine;

/*
 * Calculates the distance in certain directions between start position and current position of AR Camera / mobile device.
 */
public class DistanceCalculator: MonoBehaviour, IDistanceCalculator
{
    private Transform aRCamera;
    private Vector3 supportVector;
    private Vector3 upwardVector;
    private Vector3 forwardVector;
    private Vector3 sidewardVector;

    public float upwardDistance { get; set; }
    public float forwardDistance { get; set; }
    public float sidewardDistance { get; set; }

    private bool active;

    public bool Active {

        get {
            return active;
        }

        set
        {
            if (value)
            {
                Reset();
            }
            active = value;
        }
    }

    private void Update()
    {
        if (Active)
        {
            upwardDistance = CalculateDistanceBetweenCameraAndPlane(upwardVector);
            forwardDistance = CalculateDistanceBetweenCameraAndPlane(forwardVector);
            sidewardDistance = CalculateDistanceBetweenCameraAndPlane(sidewardVector);
        }
    }

    private float CalculateDistanceBetweenCameraAndPlane(Vector3 normalVector)
    {
        // calculate a with plane equation
        float a = normalVector.x * supportVector.x + normalVector.y * supportVector.y + normalVector.z * supportVector.z;

        // calculate vector p -> current camera position
        Vector3 p = aRCamera.position;                                                                                          

        // calculate distance between plane and point vector p
        float realDistance = Mathf.Abs(normalVector.x * p.x + normalVector.y * p.y + normalVector.z * p.z - a) / Mathf.Sqrt(Mathf.Pow(normalVector.x, 2) + Mathf.Pow(normalVector.y, 2) + Mathf.Pow(normalVector.z, 2));

        // calcuate if distance is "negative"
        Vector3 negativeDistanceVector = supportVector - realDistance * normalVector;
        float distanceToNegativeDistanceVector = Mathf.Sqrt(Mathf.Pow(p.x - negativeDistanceVector.x,2) + Mathf.Pow(p.y - negativeDistanceVector.y, 2) + Mathf.Pow(p.z - negativeDistanceVector.z, 2));

        Vector3 positiveDistanceVector = supportVector + realDistance * normalVector;
        float distanceToPositiveDistanceVector = Mathf.Sqrt(Mathf.Pow(p.x - positiveDistanceVector.x, 2) + Mathf.Pow(p.y - positiveDistanceVector.y, 2) + Mathf.Pow(p.z - positiveDistanceVector.z, 2));

        if (distanceToNegativeDistanceVector < distanceToPositiveDistanceVector)
        {
            return (-1) * realDistance / 4;
        }

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