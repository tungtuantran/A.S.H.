using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LookAtARCamera : MonoBehaviour
{
    private Camera aRCamera;

    void Start()
    {
        aRCamera = Camera.main;
    }

    void Update()
    {
        transform.LookAt(aRCamera.transform);
    }
}