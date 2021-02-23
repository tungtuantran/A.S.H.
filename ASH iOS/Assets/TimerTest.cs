using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string Time = "16:23:01";
        DateTime date = DateTime.Parse(Time, System.Globalization.CultureInfo.CurrentCulture);

        string t = date.ToString("HH:mm:ss tt");
        Debug.Log(t);
    }
}
