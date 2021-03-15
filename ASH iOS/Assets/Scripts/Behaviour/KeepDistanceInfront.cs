using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Keeps certain distance from Camera in the direction it looks
 **/
public class KeepDistanceInfront : MonoBehaviour
{
    public float distance = 0.02f;
    public bool lookAtlineSupportVectorOfLine;

    private Transform aRCamera;
    private Vector3 lineSupportVector;
    private Vector3 lineDirectionVector;

    private Vector3 planePoint;
    private Vector3 planeNormalVector;

    private void Awake()
    {
        aRCamera = Camera.main.transform;
        SetLine();
    }

    private void Update()
    {
        SetPlane();

        // calculate intersection of line and plane
        Vector3 intersection = IntersectPoint(lineDirectionVector, lineSupportVector, planeNormalVector, planePoint);
        CalculatePosition(intersection);
    }

    private Vector3 IntersectPoint(Vector3 rayVector, Vector3 rayPoint, Vector3 planeNormal, Vector3 planePoint)
    {
        var diff = rayPoint - planePoint;
        var prod1 = diff.x * planeNormal.x + diff.y * planeNormal.y + diff.z * planeNormal.z;
        var prod2 = rayVector.x * planeNormal.x + rayVector.y * planeNormal.y + rayVector.z * planeNormal.z;
        var prod3 = prod1 / prod2;
        return rayPoint - rayVector * prod3;
    }

    public void SetLine()
    {
        //set support and direction vector of line by camera position and alignment
        lineSupportVector = aRCamera.position;
        lineDirectionVector = aRCamera.forward;
    }

    private void SetPlane()
    {
        // point of plane is current camera position
        planePoint = aRCamera.position;

        // normal vector of plane is direction vector of line
        planeNormalVector = lineDirectionVector;
    }

    private void CalculatePosition(Vector3 linePlaneIntersection)
    {
        // calculated intersection is new lineSupportVector
        lineSupportVector = linePlaneIntersection;

        // set new position of object with line equation
        transform.position = lineSupportVector + lineDirectionVector * distance;

        //set alignment
        if (lookAtlineSupportVectorOfLine)
        {
            transform.LookAt(lineSupportVector);
        }
    }
}
