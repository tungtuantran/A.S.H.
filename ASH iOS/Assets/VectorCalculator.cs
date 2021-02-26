using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorCalculator : MonoBehaviour
{
    //private const float DISTANZ = 0.01f; 

    private Transform aRCamera;

    [SerializeField]
    public GameObject ebeneGo;

    private Vector3 stuetzVektor;
    private Vector3 normalVektor;
    //private Vector3 stuetzVektorEbene;

    private void Awake()
    {
        aRCamera = Camera.main.transform;
        transform.LookAt(aRCamera.transform);
    }

    private void Update()
    {
        //Debug.Log("ARCamera Position: (" + aRCamera.position.x + ", " + aRCamera.position.y + ", " + aRCamera.position.z + ")");
        //Debug.Log("ARCamera forward: (" + aRCamera.forward.x + ", " + aRCamera.forward.y + ", " + aRCamera.forward.z + ") ");
        GetDistanzZwischenAktuelleKameraPositionUndEbene();
    }

    private void GetDistanzZwischenAktuelleKameraPositionUndEbene()
    {
        float a = normalVektor.x * stuetzVektor.x + normalVektor.y * stuetzVektor.y + normalVektor.z * stuetzVektor.z;
        Vector3 p = aRCamera.position;      //aktuelle kamera-position

        float distanz = Mathf.Abs(normalVektor.x * p.x + normalVektor.y * p.y + normalVektor.z * p.z - a) / Mathf.Sqrt(Mathf.Pow(normalVektor.x, 2) + Mathf.Pow(normalVektor.y, 2) + Mathf.Pow(normalVektor.z, 2));
        //Debug.Log("Distanz zwischen Kamera Position und Ebene: " + Math.Round(distanz,4));
        Debug.Log("Distanz zwischen Kamera Position und Ebene in %: " + Math.Round(distanz, 4) * 10000);

        //return distanz;
    }

    public void SetEbene()
    {
        SetStuetzVektor();
        SetNormalVektor();
        Debug.Log("Stuetz Vektor: " + stuetzVektor);
        Debug.Log("Normalvektor: " + normalVektor);
    }

    /*
    private void SetStuetzVektorEbene()
    {
        stuetzVektorEbene = stuetzVektorGerade + DISTANZ * normalVektor;

        stuetzVeKtorEbeneGo.transform.position = stuetzVektorEbene;
    }
    */

    private void SetNormalVektor()
    {
        normalVektor = aRCamera.forward;    
    }

    private void SetStuetzVektor()
    {
        stuetzVektor = aRCamera.position;

        ebeneGo.transform.position = stuetzVektor + normalVektor * (0.01f + 0.001f);
    }

}
