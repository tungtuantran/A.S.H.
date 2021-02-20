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
    public static int deviceId;                         //001

    private static string codeString;                          //TL1_001
    private static string[] splittedCode;
    private static string deviceShortName;                     //TL1 = table_lamp1
    private static string deviceName;                          //table_lamp1

    [SerializeField]
    private GameObject[] placeableDevicePrefabs;

    private Dictionary<string, GameObject> spawnedDevicePrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;

    private void Awake()
    {
        //addDevicePopUp.SetActive(false);              //default: not active
        //removeDeviceButton.SetActive(false);

        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        foreach(GameObject devicePrefab in placeableDevicePrefabs)
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
        //addDevicePopUp.SetActive(false);                            //wenn neues bild gescannt wird, soll wieder inaktiv sein
        //removeDeviceButton.SetActive(false);

        codeString = trackedImage.referenceImage.name;
        splittedCode = codeString.Split('_');
        deviceShortName = splittedCode[0];                      
        try
        {
            deviceId = Convert.ToInt32(splittedCode[1]);
        }catch(Exception e)                                         //example: if deviceId = null or not an integer
        {
            Debug.LogError("Invalid Device ID");
            throw new InvalidMarkerException("Invalid Device ID");
        }

        deviceName = DeviceShortNameToName(deviceShortName);

        GameObject devicePrefab = spawnedDevicePrefabs[deviceName];

        Vector3 position = trackedImage.transform.position;
        devicePrefab.transform.position = position;
        devicePrefab.SetActive(true);

        
        foreach(GameObject gameObject in spawnedDevicePrefabs.Values)       //verhindert, dass mehrere prefabs gleichzeitig angezeigt werden, wenn mehrere marker zu sehen sind?
        {
            if(gameObject.name != deviceName)
            {
                gameObject.SetActive(false);
            }
        }

        //TODO: bool checken, ob hinzugefügt wurde oder nicht -> wenn ja, dann weiter (zeige an marker gehefteten AR-Controller), sonst nicht (return)
        bool deviceIsRegistered = checkIfDeviceIsRegistered(deviceId);
        if (!deviceIsRegistered)
        {
            //addDevicePopUp.SetActive(true);
        }
        else
        {
            Device trackedDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(deviceId);
            if(trackedDevice == null)                                        //TODO: neccessary? because it has to be != null
            {
                Debug.LogError("Device ID not Found");
                throw new InvalidMarkerException("Device ID not found");
            }
            //TODO: ??? was muss mit trackedDevice gemacht werden

            //removeDeviceButton.SetActive(true);
        }
    }

    public static void AddTrackedDevice(string name)                 //TODO: ist testweise; eigentlich: AddLamp(string name)
    {
        //addDevicePopUp.SetActive(false);
        switch (deviceShortName)
        {
            case "SL1":
            case "SL2":
            case "TL1":
            case "TL4":
            case "WL4":
                Device deviceToAdd = new Lamp(deviceName, deviceId, name);              //TODO: aendern zu (deviceName, id, name)
                DeviceCollection.DeviceCollectionInstance.AddRegisteredDevice(deviceToAdd);  
                break;
            default:
                Debug.LogError("Invalid Device Short Name");
                throw new InvalidMarkerException("Invalid Device Short Name");
        }
       

        //just for testing:
        //DeviceCollection.DeviceCollectionInstance.AddRegisteredDevice(new Lamp("standing_lamp1", 1, "RandomName"));
    }

    public static void RemoveTrackedDevice()
    {
        DeviceCollection.DeviceCollectionInstance.RemoveRegisteredDevice(DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(deviceId));

        //just for testing:
        //DeviceCollection.DeviceCollectionInstance.RemoveRegisteredDevice(DeviceCollection.DeviceCollectionInstance.registeredDevices[DeviceCollection.DeviceCollectionInstance.registeredDevices.Count-1]);      //TODO
    }

    private bool checkIfDeviceIsRegistered(int trackedDeviceId)
    {
        if (DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(trackedDeviceId) != null)
        {
            return true;
        }
        return false;
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
                Debug.LogError("Invalid Device Name");
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