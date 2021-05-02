using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class SaveSystem : MonoBehaviour
{
    private const string FILE_NAME = "/deviceCollection.dc";

    private void Awake()
    {
        //It's neccesary for serialization on iOS devices, because it forces a different code path in the BinaryFormatter that doesn't rely on run-time code generation which would break on iOS.
        Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes"); 
    }

    public static void SaveDeviceCollection(DeviceCollection deviceCollection)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + FILE_NAME;
        FileStream stream = new FileStream(path, FileMode.Create);

        DeviceCollectionData deviceCollectionData = new DeviceCollectionData(deviceCollection);
        formatter.Serialize(stream, deviceCollectionData);
        stream.Close();
    }

    public static DeviceCollectionData LoadDeviceCollection()                         
    {
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

    private void OnApplicationPause(bool pause)                         // saves on pause and also on exit
    {
        SaveDeviceCollection(DeviceCollection.DeviceCollectionInstance);
    }
}