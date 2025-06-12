using UnityEngine;

public class DeviceInfoListItem : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI mDeviceNameText;
    [SerializeField]
    private TMPro.TextMeshProUGUI mDeviceAddressText;


    private string mDeviceAddress;
    private string mDeviceName;

    public void InitDevice(string aDeviceAddress, string aDeviceName)
    {
        mDeviceAddressText.text = aDeviceAddress;
        mDeviceNameText.text = aDeviceName;
        mDeviceName = aDeviceName;
        mDeviceAddress = aDeviceAddress;
    }

    public void OnConnectButtonPressed()
    {
        BLEManager.Instance.Connect(mDeviceAddress);
    }
}
