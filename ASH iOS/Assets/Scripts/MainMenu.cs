﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    [SerializeField]
    private GameObject overview;

    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    private GameObject aRCamera;

    public float smoothFactor = 0.5f;


    public void showGeneralOverview()
    {
        overview.SetActive(true);
    }

    public void showGroupOverview()
    {
        overview.SetActive(true);
    }

    public void Leave()
    {
        overview.SetActive(false);

    }

    public void refreshPosition()
    {
        /*
        var targetObject = menu.transform.GetChild(0);
        var currentPosition = targetObject.position;

        Debug.Log("Current: " + currentPosition.x + ", " + currentPosition.y + ", " + currentPosition.z);

        var targetPosition = ARCamera.transform.position
            + ARCamera.transform.forward * 0.6f
            + ARCamera.transform.up * 0.2f;

        Debug.Log("TargetPos: " + targetPosition.x + ", " + targetPosition.y + ", " + targetPosition.z);


        menu.transform.GetChild(0).position = Vector3.MoveTowards(currentPosition, targetPosition, smoothFactor);
        Debug.Log("NewCurrent: " + menu.transform.GetChild(0).position.x + ", " + menu.transform.GetChild(0).position.y + ", " + menu.transform.GetChild(0).position.z);
        */
        float distance = 1000;

        Debug.Log("Current: " + canvas.transform.position.x + ", " + canvas.transform.position.y + ", " + canvas.transform.position.z);

        canvas.transform.position = aRCamera.transform.position + aRCamera.transform.forward * distance;

        Debug.Log("TargetPos: " + aRCamera.transform.position.x + ", " + aRCamera.transform.position.y + ", " + aRCamera.transform.position.z);

        Debug.Log("NewCurrent: " + canvas.transform.position.x + ", " + canvas.transform.position.y + ", " + canvas.transform.position.z);

    }

}
