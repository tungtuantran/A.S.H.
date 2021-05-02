using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using System;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTracking : MonoBehaviour
{
    //Tracked Data of TrackedImage
    public static int deviceId { get; set; }                // example: 001
    public static string deviceName { get; set; }           // example: table_lamp1

    [SerializeField]
    private GameObject[] placeableDevicePrefabs;

    public static Dictionary<int, GameObject> spawnedDevicePrefabs = new Dictionary<int, GameObject>();
    private ARTrackedImageManager trackedImageManager;

    private void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            string codeString = trackedImage.referenceImage.name;
            string[] splittedCode = codeString.Split('_');
            deviceId = ConvertIdStringToInteger(splittedCode[1]);

            spawnedDevicePrefabs[deviceId].SetActive(false);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        //Decode codeString to get device name and Id
        string codeString = trackedImage.referenceImage.name;       // example: TL1_001
        string[] splittedCode = codeString.Split('_');              // example: [TL1], [001]

        deviceName = DeviceShortNameToName(splittedCode[0]);
        deviceId = ConvertIdStringToInteger(splittedCode[1]);

        //Spawn devicePrefab
        GameObject devicePrefab = null;

        try
        {
            devicePrefab = spawnedDevicePrefabs[deviceId];
        }
        catch (KeyNotFoundException e)
        {
            foreach (GameObject placeableDevicePrefab in placeableDevicePrefabs)
            {
                if (placeableDevicePrefab.name.Equals(deviceName))
                {
                    GameObject newDevicePrefab = Instantiate(placeableDevicePrefab, Vector3.zero, Quaternion.identity);
                    newDevicePrefab.name = placeableDevicePrefab.name;
                    spawnedDevicePrefabs.Add(deviceId, newDevicePrefab);
                    devicePrefab = spawnedDevicePrefabs[deviceId];
                }
            }
        }

        if (devicePrefab != null)
        {
            Vector3 position = trackedImage.transform.position;
            Vector3 rotation = new Vector3(trackedImage.transform.eulerAngles.x, trackedImage.transform.eulerAngles.y, trackedImage.transform.eulerAngles.z);
            devicePrefab.transform.position = position;
            devicePrefab.transform.eulerAngles = rotation;

            devicePrefab.SetActive(true);
        }
    }

    private int ConvertIdStringToInteger(string idInString)
    {
        int idInInteger;
        try
        {
            idInInteger = Convert.ToInt32(idInString);
        }
        catch (FormatException e)
        {
            //if idInString is not convertable to an integer
            throw new InvalidMarkerException("Invalid Device ID", e);       
        }

        if(idInInteger == 0)
        {
            //if id = 0
            throw new InvalidMarkerException("Invalid Device ID");
        }

        return idInInteger;
    }

    private string DeviceShortNameToName(string deviceShortName)
    {
        switch (deviceShortName)
        {
            case "SL1":
                return Enum.GetName(typeof(DeviceName), DeviceName.standing_lamp1);
            case "SL2":
                return Enum.GetName(typeof(DeviceName), DeviceName.standing_lamp2);
            case "TL1":
                return Enum.GetName(typeof(DeviceName), DeviceName.table_lamp1);
            case "TL4":
                return Enum.GetName(typeof(DeviceName), DeviceName.table_lamp4);
            case "WL4":
                return Enum.GetName(typeof(DeviceName), DeviceName.wall_lamp4);
            default:
                throw new InvalidMarkerException("Invalid Device Name");
        }
    }
}

[Serializable]
public class InvalidMarkerException : Exception
{
    public InvalidMarkerException()
    {
    }

    public InvalidMarkerException(string message) : base(message)
    {
    }

    public InvalidMarkerException(string message, Exception e): base(message, e)
    {

    }
}