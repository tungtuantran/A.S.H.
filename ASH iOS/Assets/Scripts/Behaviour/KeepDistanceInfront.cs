using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Keeps certain distance from Camera in the direction it looks
 **/
public class KeepDistanceInfront : MonoBehaviour
{
    public float distance = 0.02f;
    public bool lookAtARCamera;

    private Transform aRCamera;
    private Vector3 supportVector;
    private Vector3 directionVector;

    private void Awake()
    {
        aRCamera = Camera.main.transform;
        SetDirection();
    }

    private void Update()
    {
        Vector3 p = aRCamera.position;                              // current camera position
        Vector3 normalVector = directionVector;
        Vector3 intersection = IntersectPoint(directionVector, supportVector, normalVector, p);
        supportVector = intersection;
        transform.position = supportVector + directionVector * distance;

        if(lookAtARCamera)                                          
        {
            transform.LookAt(supportVector);
        }
    }

    private Vector3 IntersectPoint(Vector3 rayVector, Vector3 rayPoint, Vector3 planeNormal, Vector3 planePoint)
    {
        var diff = rayPoint - planePoint;
        var prod1 = diff.x * planeNormal.x + diff.y * planeNormal.y + diff.z * planeNormal.z;
        var prod2 = rayVector.x * planeNormal.x + rayVector.y * planeNormal.y + rayVector.z * planeNormal.z;
        var prod3 = prod1 / prod2;
        return rayPoint - rayVector * prod3;
    }

    public void SetDirection()
    {
        supportVector = aRCamera.position;
        directionVector = aRCamera.forward;
    }
}
