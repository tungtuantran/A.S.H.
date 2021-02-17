using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/*
public static class SaveSystem
{
    public static void SaveDevice(Device device)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + device.GetType().Name + ".dv";
        FileStream stream = new FileStream(path, FileMode.Create);

        switch (device.GetType().Name)
        {
            case "Lamp":
                LampData lampData = new LampData((Lamp) device);
                formatter.Serialize(stream, lampData);
                break;
            default:
                Debug.LogError("Device type not found.");
                break;
        }
        stream.Close();
    }

    public static IDeviceData LoadDevice(string deviceType)                         //deviceType example: "Lamp"
    {
        string path = Application.persistentDataPath + "/" + deviceType + ".dv";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            IDeviceData deviceData;
            switch (deviceType)
            {
                case "Lamp":
                    deviceData = formatter.Deserialize(stream) as LampData;
                    break;
                default:
                    Debug.Log("Device type not found.");
                    deviceData = null;
                    break;
            }

            stream.Close();
            return deviceData;                                                      //for example: if LampData is wanted -> Cast: (LampData) deviceData;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
*/

public static class SaveSystem
{
    public static void SaveDeviceCollection(DeviceCollection deviceCollection)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/deviceCollection.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        DeviceCollectionData deviceCollectionData = new DeviceCollectionData(deviceCollection);
        formatter.Serialize(stream, deviceCollectionData);
        stream.Close();
    }

    public static DeviceCollectionData LoadDeviceCollection()                         
    {
        string path = Application.persistentDataPath + "/deviceCollection.txt";
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
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}