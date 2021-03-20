using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectDevice : MonoBehaviour
{
	public static Device selectedDevice { get; set; }

	private int deviceId;
	//public static string gameObjectName;

	void Awake()
    {
		deviceId = ImageTracking.deviceId;
    }

	void Update()
	{
		
		if (selectedDevice == null)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Debug.Log("mouse down");
				Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);

				RaycastHit myHitInfo = new RaycastHit();


				if (GetComponent<BoxCollider>().Raycast(inputRay, out myHitInfo, 10000f))
				{
					selectedDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(deviceId);
					//CopyPasteSystem.selectedDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(deviceId);
					Debug.Log("selected device set; id: " + deviceId + "; selected device name: " + selectedDevice.Name);
				}
			}
		}

        if (Input.GetMouseButtonUp(0))
        {
			selectedDevice = null;
			//CopyPasteSystem.selectedDevice = null;
			Debug.Log("selected device reset: " + selectedDevice);
		}
		

		/*
		Debug.Log(gameObjectName);

		if (Input.GetMouseButtonDown(0))
		{
			Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit myHitInfo = new RaycastHit();

			if (GetComponent<BoxCollider>().Raycast(inputRay, out myHitInfo, 10000f))
			{
				gameObjectName = gameObject.name;
            }
			
		}

		if (Input.GetMouseButtonUp(0))
		{
			//CopyPasteSystem.selectedDevice = null;
			//Debug.Log("mouse up");
			gameObjectName = "";
		}
		*/


	} 
}
