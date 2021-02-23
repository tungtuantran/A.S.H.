using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LookAt : MonoBehaviour
{
    private Camera aRCamera;

    void Start()
    {
        aRCamera = Camera.main;
        try
        {
            GetComponent<Canvas>().worldCamera = aRCamera;
        }catch(Exception e)
        {
            //do nothing: somethimes no canvas needed
        }
    }

    void Update()
    {
        this.transform.LookAt(aRCamera.transform);
    }
}
