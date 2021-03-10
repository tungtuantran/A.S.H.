using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldTest : MonoBehaviour
{

    private const string PATH = "TextName";

    [SerializeField]
    private InputField inputField1;

    [SerializeField]
    private InputField inputField2;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("inputfield 1 text: " + inputField1.text);
        Debug.Log("inputfield 2 text: " + inputField2.text);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("inputfield 1 text: " + inputField1.text);
        Debug.Log("inputfield 2 text: " + inputField2.text);
        inputField2.text = inputField1.text;
    }
}
