using System;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class BluetoothTest : MonoBehaviour
{
    public Text deviceName;
    public Text dataToSend;
    private bool IsConnected;
    public static string dataRecived = "";
    public GameObject startCarPanel;
    public GameObject controllerPanel;
    public TMP_Text isConnectText;

    void Start()
    {
#if UNITY_2020_2_OR_NEWER
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation)
          || !Permission.HasUserAuthorizedPermission(Permission.FineLocation)
          || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_SCAN")
          || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_ADVERTISE")
          || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_CONNECT"))
            Permission.RequestUserPermissions(new string[] {
                        Permission.CoarseLocation,
                            Permission.FineLocation,
                            "android.permission.BLUETOOTH_SCAN",
                            "android.permission.BLUETOOTH_ADVERTISE",
                             "android.permission.BLUETOOTH_CONNECT"
                    });
#endif
#endif

        IsConnected = false;
        BluetoothService.CreateBluetoothObject();

    }
    void Update()
    {
        if (IsConnected)
        {
            try
            {
                string datain = BluetoothService.ReadFromBluetooth();
                if (datain.Length > 1)
                {
                    dataRecived = datain;
                    print(dataRecived);
                }

            }
            catch (Exception e)
            {
                BluetoothService.Toast("Error in connection");
            }
            isConnectText.text = "Pornita";
            isConnectText.color = Color.green;
        }
        else
        {
            isConnectText.text = "Oprita";
            isConnectText.color = Color.red;
        }
    }

    //Connection
    public void GetDevicesButton()
    {
        string[] devices = BluetoothService.GetBluetoothDevices();

        foreach (var d in devices)
        {
            Debug.Log(d);
        }
    }
    public void StartButton()
    {
        if (!IsConnected)
        {
            IsConnected = BluetoothService.StartBluetoothConnection(deviceName.text.ToString());
            BluetoothService.Toast(deviceName.text.ToString() + " status: " + IsConnected);
        }
        if (IsConnected)
        {
            startCarPanel.gameObject.SetActive(false);
            controllerPanel.gameObject.SetActive(true);
        }
    }
    public void SendButton()
    {
        if (IsConnected && (dataToSend.ToString() != "" || dataToSend.ToString() != null))
            BluetoothService.WritetoBluetooth(dataToSend.text.ToString());
        else
            BluetoothService.WritetoBluetooth("Not connected");
    }
    public void StopButton()
    {
        if (IsConnected)
        {
            BluetoothService.StopBluetoothConnection();
        }
    }

    //Controls
    public void UpEvent()
    {
        BluetoothService.WritetoBluetooth("S");
        Debug.Log("Send null");
    }
    public void ControllUp()
    {
        BluetoothService.WritetoBluetooth("F");
        Debug.Log("F");
    }
    public void ControllDown()
    {
        BluetoothService.WritetoBluetooth("B");
        Debug.Log("B");

    }
    public void ControllRight()
    {
        BluetoothService.WritetoBluetooth("R");
        Debug.Log("R");

    }
    public void ControllLeft()
    {
        BluetoothService.WritetoBluetooth("L");
        Debug.Log("L");

    }

    //Music
    public void MusicOn()
    {
        BluetoothService.WritetoBluetooth("M");
        Debug.Log("M");
    }
    public void MusicOff()
    {
        BluetoothService.WritetoBluetooth("N");
        Debug.Log("N");
    }
}
