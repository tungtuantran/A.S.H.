using UnityEngine;

/*
 * Presenter of Device
 */
public abstract class DevicePresenter : MonoBehaviour
{
    // Model
    protected IDevice device;

    public IDevice Device
    {
        get
        {
            return device;
        }

        set
        {
            device = value;
        }
    }

    // View
    protected IDeviceView view;

    public IDeviceView View
    {
        get
        {
            return view;
        }

        set
        {
            view = value;
        }
    }

    [SerializeField]
    private DeviceView deviceView;

    protected virtual void Awake()
    {
        int deviceId = ImageTracking.deviceId;
        string deviceName = ImageTracking.deviceName;

        // set device model
        device = DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(deviceId);

        if(device == null)
        {
            SetDevice(deviceName, deviceId);
        }

        // set device view
        View = deviceView;
    }

    void Start()
    {
        ShowView();
    }

    public void SetDeviceOnOff()
    {
        if (device.IsOn)
        {
            device.IsOn = false;
        }
        else
        {
            device.IsOn = true;
        }

        view.OnUpdateIsOn(device.IsOn);
    }

    public void RemoveDevice()
    {
        DeviceCollection.DeviceCollectionInstance.RemoveRegisteredDevice(device);
        view.OnDeviceRemoved();
        view.OnRegisteredDevice(false);
    }

    public void EditNameOfDevice()
    {
        string name = view.editNameInputField.text;

        if (!string.IsNullOrWhiteSpace(name))
        {
            device.Name = name;
        }
        // else keep old name

        view.OnUpdateName(device.Name);
    }

    public void AddDevice()
    {
        // clears basically the values from the device object first -> inserts default values before adding the device 
        InsertDefaultValuesToDevice();

        string name = view.addNameInputField.text;

        if (!string.IsNullOrWhiteSpace(name))
        {
            device.Name = name;
            DeviceCollection.DeviceCollectionInstance.AddRegisteredDevice(device);
            view.OnDeviceAdded(name);

            view.OnUpdateIsOn(device.IsOn);
            view.OnUpdateName(device.Name);
            view.OnRegisteredDevice(true);
        }
        else
        {
            throw new NoInputException();
        }
    }

    public virtual void ShowView()
    {
        // if tracked device is registered in DeviceCollection
        if (DeviceCollection.DeviceCollectionInstance.GetRegisteredDeviceByDeviceId(device.Id) != null)
        {
            view.OnUpdateIsOn(device.IsOn);
            view.OnUpdateName(device.Name);
            view.OnRegisteredDevice(true);
        }
        else
        {
            view.OnRegisteredDevice(false);
        }
    }

    public abstract void StopUpdating();

    public abstract void InsertCopiedValuesToDevice(IDevice copiedDevice);

    protected abstract void InsertDefaultValuesToDevice();

    protected abstract void SetDevice(string deviceName, int deviceId);

}
