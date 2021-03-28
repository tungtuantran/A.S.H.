using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectDevice : MonoBehaviour
{
	public static Device selectedDevice { get; set; }

	private int deviceId;

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
				Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit myHitInfo = new RaycastHit();

				if (GetComponent<BoxCollider>().Raycast(inputRay, out myHitInfo, 10000f))
				{
					selectedDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(deviceId);
				}
			}
		}

        if (Input.GetMouseButtonUp(0))
        {
			selectedDevice = null;
		}
	} 
}
