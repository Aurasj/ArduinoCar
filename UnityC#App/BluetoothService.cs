using System;
using UnityEngine;
public class BluetoothService
{
    private static AndroidJavaClass unityPlayer;
    private static AndroidJavaObject activity;
    private static AndroidJavaObject context;
    private static AndroidJavaClass unity3dbluetoothplugin;
    private static AndroidJavaObject BluetoothConnector;
 
    public static void CreateBluetoothObject()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            context = activity.Call<AndroidJavaObject>("getApplicationContext");
            unity3dbluetoothplugin = new AndroidJavaClass("com.example.unity3dbluetoothplugin.BluetoothConnector");
            BluetoothConnector = unity3dbluetoothplugin.CallStatic<AndroidJavaObject>("getInstance");
        }
    }

    public static string[] GetBluetoothDevices()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            try
            {
                return BluetoothConnector.Call<string[]>("GetBluetoothDevices");
            }
            catch (Exception e)
            {
                Toast("No Device found");
                return null;
            }
        }

        return null;

    }

    public static bool StartBluetoothConnection(string DeviceName)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            try
            {
                string connectionStatus = BluetoothConnector.Call<string>("StartBluetoothConnection", DeviceName);
                Toast("Start connection status: " + connectionStatus);
                if (connectionStatus == "Connected") 
                    return true;

            }
            catch (Exception e)
            {
                Toast("Start connection error");
            }
        }

        return false;
    }

    public static void StopBluetoothConnection()
    {

        if (Application.platform == RuntimePlatform.Android)
        {
            try
            {
                BluetoothConnector.Call("StopBluetoothConnection");
                Toast("Connction stoped");

            }
            catch (Exception e)
            {
                Toast("Stop connction error");
            }
        }
    }

    public static void WritetoBluetooth(string data)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            try
            {
                BluetoothConnector.Call("WriteData", data);
            }
            catch (Exception e)
            {
                Toast("Write data error");
            }
        }
    }

    public static string ReadFromBluetooth()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            try
            {
                return BluetoothConnector.Call<string>("ReadData");
            }
            catch (Exception e)
            {
                BluetoothConnector.Call("PrintOnScreen", context, "Read data error");
            }
        }
         
        return "";

    }

    public static void Toast(string data)
    {
        BluetoothConnector.Call("PrintOnScreen", context, data);
        Debug.Log(data);
    }

}
