using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{

    private Camera aRCamera;

    // Start is called before the first frame update
    void Start()
    {
        aRCamera = Camera.main;
        GetComponent<Canvas>().worldCamera = aRCamera;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(aRCamera.transform);
    }
}
