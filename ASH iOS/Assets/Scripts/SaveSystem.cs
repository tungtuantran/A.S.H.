using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    private static string fileName = "/deviceCollection.dc";

    public static void SaveDeviceCollection(DeviceCollection deviceCollection)
    {
        Debug.Log("saving device collection");

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + fileName;
        FileStream stream = new FileStream(path, FileMode.Create);

        DeviceCollectionData deviceCollectionData = new DeviceCollectionData(deviceCollection);
        formatter.Serialize(stream, deviceCollectionData);
        stream.Close();
    }

    public static DeviceCollectionData LoadDeviceCollection()                         
    {
        Debug.Log("loading device collection");

        string path = Application.persistentDataPath + fileName;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            DeviceCollectionData deviceCollectionData = formatter.Deserialize(stream) as DeviceCollectionData;
            stream.Close();
            return deviceCollectionData;                               
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }

    private void Awake()
    {
        Debug.Log("Awake");
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable");
    }


    private void OnApplicationPause(bool pause)
    {
        Debug.Log("Application paused");
        SaveDeviceCollection(DeviceCollection.DeviceCollectionInstance);
    }
}