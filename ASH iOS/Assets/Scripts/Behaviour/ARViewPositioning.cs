using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARViewPositioning : MonoBehaviour
{
    public float distance = 1000;

    private Transform aRCamera;

    private void Awake()
    {
        aRCamera = Camera.main.transform;
        this.transform.LookAt(aRCamera.transform);
    }

    public void Reposition()
    {
        this.transform.position = aRCamera.position + aRCamera.forward * distance;
        this.transform.LookAt(aRCamera.transform);
    }
}
