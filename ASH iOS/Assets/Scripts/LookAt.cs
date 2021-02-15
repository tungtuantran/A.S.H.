using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{

    [SerializeField]
    private GameObject menu;

    private Transform aRCamera;

    // Start is called before the first frame update
    void Start()
    {
        aRCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(aRCamera.transform);
    }
}
