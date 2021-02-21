using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LongClick : MonoBehaviour
{
    public float ClickDuration = 2;

    [SerializeField]
    public UnityEvent OnLongClick;

    bool clicking = false;
    float totalDownTime = 0;


    // Update is called once per frame
    void Update()
    {
        // Detect the first click
        if (Input.GetMouseButtonDown(0))
        {
            totalDownTime = 0;
            clicking = true;
        }

        if (clicking && Input.GetMouseButton(0))
        {
            totalDownTime += Time.deltaTime;

            if (totalDownTime >= ClickDuration)
            {
                Debug.Log("Long click");
                clicking = false;
                OnLongClick.Invoke();
            }
        }

        if (clicking && Input.GetMouseButtonUp(0))
        {
            clicking = false;
        }
    }
}