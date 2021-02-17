using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using System;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTracking : MonoBehaviour
{

    [SerializeField]
    private GameObject[] placeablePrefabs;

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;

    private void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        foreach(GameObject prefab in placeablePrefabs)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            newPrefab.SetActive(false);
            spawnedPrefabs.Add(prefab.name, newPrefab);
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
        foreach(ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            spawnedPrefabs[trackedImage.name].SetActive(false);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        
        string codeString = trackedImage.referenceImage.name;       //example: TL1_001      //string name = trackedImage.referenceImage.name;
        string[] splittedCode = codeString.Split('_');
        string deviceShortName = splittedCode[0];               //TL1 = table_lamp1; ...
        int deviceId;
        try
        {
            deviceId = Convert.ToInt32(splittedCode[1]);   //example: 001, 002
        }catch(Exception e)                                         //example: if deviceId = null or not an integer
        {
            Debug.LogError("Invalid Device ID");
            return;
        }

        string deviceName = "";
        switch (deviceShortName)
        {
            case "SL1":
                deviceName = Enum.GetName(typeof(DeviceName), DeviceName.standing_lamp1);
                break;
            case "SL2":
                deviceName = Enum.GetName(typeof(DeviceName), DeviceName.standing_lamp2);
                break;
            case "TL1":
                deviceName = Enum.GetName(typeof(DeviceName), DeviceName.table_lamp1);
                break;
            case "TL4":
                deviceName = Enum.GetName(typeof(DeviceName), DeviceName.table_lamp4);
                break;
            case "WL4":
                deviceName = Enum.GetName(typeof(DeviceName), DeviceName.wall_lamp4);
                break;
            default:
                Debug.LogError("Invalid Device Name");
                return;
        }

        GameObject devicePrefab = spawnedPrefabs[deviceName];         //GameObject prefab = spawnedPrefabs[name];

        Vector3 position = trackedImage.transform.position;
        devicePrefab.transform.position = position;
        devicePrefab.SetActive(true);

        foreach(GameObject gameObject in spawnedPrefabs.Values)
        {
            if(gameObject.name != deviceName)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
