using UnityEngine;
using System.IO;

/*
 * Saves and loads data of the registered devices.
 */
public class SaveAndLoadSystem : MonoBehaviour
{
    private const string FILE_NAME = "/deviceCollection.txt";

    public static void SaveDeviceCollection(DeviceCollection deviceCollection)
    {
        string path = Application.persistentDataPath + FILE_NAME;
        DeviceCollectionData deviceCollectionData = new DeviceCollectionData(deviceCollection);
        
        using (StreamWriter stream = new StreamWriter(path))
        {
            string json = JsonUtility.ToJson(deviceCollectionData);
            stream.Write(json);
        }
    }

    public static DeviceCollectionData LoadDeviceCollection()
    {
        string path = Application.persistentDataPath + FILE_NAME;

        if (File.Exists(path))
        {
            DeviceCollectionData deviceCollectionData;
          
            using (StreamReader stream = new StreamReader(path))
            {
                string json = stream.ReadToEnd();
                deviceCollectionData = JsonUtility.FromJson<DeviceCollectionData>(json);
            }

            return deviceCollectionData;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }

    private void OnApplicationPause(bool pause)     // saves on pause and also on exit
    {
        SaveDeviceCollection(DeviceCollection.DeviceCollectionInstance);
    }
}