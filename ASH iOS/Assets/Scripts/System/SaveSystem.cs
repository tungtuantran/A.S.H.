using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    private const string FILE_NAME = "/deviceCollection.dc";

    public static void SaveDeviceCollection(DeviceCollection deviceCollection)
    {
        Debug.Log("saving device collection");

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + FILE_NAME;
        FileStream stream = new FileStream(path, FileMode.Create);

        DeviceCollectionData deviceCollectionData = new DeviceCollectionData(deviceCollection);
        formatter.Serialize(stream, deviceCollectionData);
        stream.Close();
    }

    public static DeviceCollectionData LoadDeviceCollection()                         
    {
        Debug.Log("loading device collection");

        string path = Application.persistentDataPath + FILE_NAME;
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

    private void OnApplicationPause(bool pause)                         // save on pause & exit
    {
        SaveDeviceCollection(DeviceCollection.DeviceCollectionInstance);
    }
}