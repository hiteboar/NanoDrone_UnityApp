using System;
using UnityEngine;
using UnityEngine.Events;

interface AndroidLib
{
    public void OnDeviceFound(string aDeviceAdress);
    public void OnStartScan();
    public void OnStopScan();
    public void OnStartConnection();
    public void OnDeviceConnected(string aDeviceAdress);
    public void OnDeviceDisconnected();
}

public class BLEManager : MonoBehaviour, AndroidLib
{
    AndroidJavaClass BLEController;

    public UnityEvent OnBLEStartScan;
    public UnityEvent OnBLEStopScan;
    public UnityEvent OnBLEStartConnection;
    public UnityEvent<string> OnBLEDeviceConnected;
    public UnityEvent OnBLEDeviceDisconnected;
    public UnityEvent<string> OnBLEDeviceFound;
    public UnityEvent<byte, byte, byte, byte> OnDataReceived;

    public static BLEManager Instance;

    private void Awake()
    {
        BLEController = new AndroidJavaClass("com.example.unityblemanager.BluetoothController");
        Instance = this;
    }

    void Start()
    {
        BLEController.CallStatic("Init");
    }

    public void Scan()
    {
        //#if UNITY_ANDROID && !UNITY_EDITOR
        // Obtener el currentActivity de Unity
        var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        // Ejecutar la llamada JNI en el hilo de la UI
        activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
            BLEController.CallStatic("Scan");
        }));
        //#endif
    }

    public void StopScan()
    {
        BLEController.CallStatic("StopScan");
    }

    public void Connect(string aDeviceAdress)
    {
        Debug.Log("Connect to " + aDeviceAdress);
        BLEController.CallStatic("Connect", aDeviceAdress);
    }

    public bool IsConnected()
    {
        return false;
    }

    public void Disconnect()
    {

    }

    public string GetDeviceName(string aDeviceAdress)
    {
        return BLEController.CallStatic<string>("GetDeviceName", aDeviceAdress);
    }

    public void OnDeviceFound(string aDeviceAdress)
    {
        Debug.Log("Device found: " + aDeviceAdress);
        OnBLEDeviceFound?.Invoke(aDeviceAdress);
    }

    public void OnStartScan()
    {
        OnBLEStartScan?.Invoke();
    }

    public void OnStopScan()
    {
        OnBLEStopScan?.Invoke();
    }

    public void OnStartConnection()
    {
        OnBLEStartConnection?.Invoke();
    }

    public void OnDeviceConnected(string aDeviceAdress)
    {
        OnBLEDeviceConnected?.Invoke(aDeviceAdress);
    }

    public void OnDeviceDisconnected()
    {
        OnBLEDeviceDisconnected?.Invoke();
    }

    public void SendData(byte[] aData)
    {
        BLEController.CallStatic("SendData", aData);
    }

    public void OnDeviceDataReceived(string aDataString)
    {
        int mod = aDataString.Length % 4;
        if (mod > 0)
            aDataString += new string('=', 4 - mod);

        byte[] data = Convert.FromBase64String(aDataString.Replace('-', '+').Replace('_', '/'));

        byte yaw = data[0];
        byte pitch = data[1];
        byte roll = data[2];
        byte altitude = data[3];

        Debug.Log("[Received] y: " + yaw + " p: " + pitch + " r: " + roll);
        OnDataReceived?.Invoke(yaw, pitch, roll, altitude);
    }
}
