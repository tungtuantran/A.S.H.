using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SelectDevice : MonoBehaviour
{
	public static Device selectedDevice { get; set; }

	public UnityEvent onCollision;
	public UnityEvent onStopCollision;

	[SerializeField]
	private MeshRenderer meshRenderer;

	private int deviceId;

	void Awake()
    {
		deviceId = ImageTracking.deviceId;
		meshRenderer.enabled = false;
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

                    if (selectedDevice != null)
                    {
						meshRenderer.enabled = true;

						if(onCollision != null)
                        {
							onCollision.Invoke();
                        }
					}
				}
			}
		}

        if (Input.GetMouseButtonUp(0))
        {
			selectedDevice = null;
			meshRenderer.enabled = false;

			if (onStopCollision != null)
			{
				onStopCollision.Invoke();
			}
		}
	} 
}
