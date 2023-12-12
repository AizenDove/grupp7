using UnityEngine;
using System.IO.Ports;

public class NFCObjectSpawner : MonoBehaviour
{
    SerialPort serialPort = new SerialPort("COM9", 9600); // Update to COM9 or your actual port

    public GameObject objectPrefab1;
    public GameObject objectPrefab2;

    void Start()
    {
        serialPort.Open();
    }

    void Update()
    {
        if (serialPort.IsOpen)
        {
            try
            {
                if (serialPort.BytesToRead > 0)
                {
                    byte[] data = new byte[serialPort.BytesToRead];
                    serialPort.Read(data, 0, data.Length);

                    // Check the received data and spawn objects accordingly
                    if (CheckTag1(data))
                    {
                        SpawnObject(objectPrefab1);
                    }
                    else if (CheckTag2(data))
                    {
                        SpawnObject(objectPrefab2);
                    }
                }
            }
            catch (System.Exception) { }
        }
    }

    void SpawnObject(GameObject prefab)
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }

    bool CheckTag1(byte[] data)
    {
        byte[] tag1UID = { 0xD0, 0x1B, 0x3C, 0x13 };
        return CompareByteArrays(data, tag1UID);
    }

    bool CheckTag2(byte[] data)
    {
        byte[] tag2UID = { 0xD0, 0x64, 0x2F, 0x13 };
        return CompareByteArrays(data, tag2UID);
    }

    bool CompareByteArrays(byte[] array1, byte[] array2)
    {
        if (array1.Length != array2.Length)
        {
            return false;
        }
        for (int i = 0; i < array1.Length; i++)
        {
            if (array1[i] != array2[i])
            {
                return false;
            }
        }
        return true;
    }

    void OnApplicationQuit()
    {
        serialPort.Close();
    }
}
