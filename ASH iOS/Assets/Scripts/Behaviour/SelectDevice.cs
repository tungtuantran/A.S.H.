using UnityEngine;
using UnityEngine.Events;

/*
 * Selects a device through raycasting.
 */
public class SelectDevice : MonoBehaviour
{
	public static bool active = true;
	public static DevicePresenter DevicePresenterOfSelectedDevice { get; set; }

	public UnityEvent onCollision;
	public UnityEvent onStopCollision;

	[SerializeField]
	private MeshRenderer meshRenderer;

	[SerializeField]
	private DevicePresenter devicePresenter;

	//private int deviceId;

	void Awake()
    {
		//deviceId = ImageTracking.deviceId;
		meshRenderer.enabled = false;
    }

	void Update()
	{
		if (active && DevicePresenterOfSelectedDevice == null)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit myHitInfo = new RaycastHit();

				if (GetComponent<BoxCollider>().Raycast(inputRay, out myHitInfo, 10000f))
				{
					//if device is registered
					if(DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(devicePresenter.Device.Id) != null) {

						DevicePresenterOfSelectedDevice = devicePresenter;
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
		DevicePresenterOfSelectedDevice = null;
		meshRenderer.enabled = false;
		active = true;
	}
}
