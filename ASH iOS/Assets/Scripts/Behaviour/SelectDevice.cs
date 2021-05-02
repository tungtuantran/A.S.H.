using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SelectDevice : MonoBehaviour
{
	public static bool active = true;
	public static IDevice SelectedDevice { get; set; }

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
		if (active && SelectedDevice == null)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit myHitInfo = new RaycastHit();

				if (GetComponent<BoxCollider>().Raycast(inputRay, out myHitInfo, 10000f))
				{
					SelectedDevice = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(deviceId);

                    if (SelectedDevice != null)
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
			Reset();

			if (onStopCollision != null)
			{
				onStopCollision.Invoke();
			}
		}
	}

    private void Reset()
    {
		SelectedDevice = null;
		meshRenderer.enabled = false;
		active = true;
	}
}
