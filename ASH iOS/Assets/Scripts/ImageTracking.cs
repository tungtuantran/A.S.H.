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

    private Dictionary<string, GameObject> spawnedDevicePrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;

    private void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        foreach (GameObject devicePrefab in placeableDevicePrefabs)
        {
            GameObject newDevicePrefab = Instantiate(devicePrefab, Vector3.zero, Quaternion.identity);
            newDevicePrefab.name = devicePrefab.name;
            newDevicePrefab.SetActive(false);
            spawnedDevicePrefabs.Add(devicePrefab.name, newDevicePrefab);
        }
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
            spawnedDevicePrefabs[trackedImage.name].SetActive(false);
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
        GameObject devicePrefab = spawnedDevicePrefabs[deviceName];
        Vector3 position = trackedImage.transform.position;
        devicePrefab.transform.position = position;
        devicePrefab.SetActive(true);

        //Sets other devicePrefabs inactive
        foreach (GameObject gameObject in spawnedDevicePrefabs.Values)
        {
            if (gameObject.name != deviceName)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private int ConvertIdStringToInteger(string idInString)
    {
        try
        {
            return Convert.ToInt32(idInString);
        }
        catch (Exception e)                                         // example: if deviceId = null or not an integer
        {
            throw new InvalidMarkerException("Invalid Device ID");
        }
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
class InvalidMarkerException : Exception
{
    public InvalidMarkerException()
    {
    }

    public InvalidMarkerException(string message) : base(message)
    {
    }
}