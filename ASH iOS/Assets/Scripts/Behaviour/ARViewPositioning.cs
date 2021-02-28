using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARViewPositioning : MonoBehaviour
{
    public float distance = 1000f;

    private Transform aRCamera;

    private Vector3 supportVector;
    private Vector3 directionVector;

    private void Awake()
    {
        aRCamera = Camera.main.transform;
        SetPositionAndDirection();
    }

    private void Update()
    {
        Vector3 p = aRCamera.position;                                                                  // current camera position
        Vector3 normalVector = directionVector;
        float a = normalVector.x * p.x + normalVector.y * p.y + normalVector.z * p.z;                   // plane

        Vector3 intersection = IntersectPoint(directionVector, supportVector, normalVector, p);
        supportVector = intersection;
        transform.position = supportVector + directionVector * distance;

        transform.LookAt(supportVector);
        Debug.Log("a=" + a + "; p=" + p.ToString() + "; realDistance=" + null + "; result position=" + transform.position.ToString());
    }

    private Vector3 IntersectPoint(Vector3 rayVector, Vector3 rayPoint, Vector3 planeNormal, Vector3 planePoint)
    {
        var diff = rayPoint - planePoint;
        var prod1 = diff.x * planeNormal.x + diff.y * planeNormal.y + diff.z * planeNormal.z;
        var prod2 = rayVector.x* planeNormal.x + rayVector.y * planeNormal.y + rayVector.z * planeNormal.z;
        var prod3 = prod1 / prod2;
        return rayPoint - rayVector * prod3;
    }

    public void SetPositionAndDirection()
    {
        supportVector = aRCamera.position;
        directionVector = aRCamera.forward;
    }
}
