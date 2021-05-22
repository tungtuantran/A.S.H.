using UnityEngine;

/*
 * GameObject looks at (AR) Camera.
 */
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