using UnityEngine;
using System.IO;
using System.IO.Ports;

public class ArduinoRotation : MonoBehaviour
{
    SerialPort serialPort = new SerialPort("COM9", 9600); // Adjust the port accordingly
    public GameObject cube;

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
                string receivedData = serialPort.ReadLine();
                Debug.Log("Received data: " + receivedData);

                string[] data = receivedData.Split(',');
                if (data.Length == 3)
                {
                    float rotX, rotY, rotZ;

                    if (float.TryParse(data[0], out rotX) && float.TryParse(data[1], out rotY) && float.TryParse(data[2], out rotZ))
                    {
                        cube.transform.rotation = Quaternion.Euler(rotX, rotY, rotZ);
                    }
                    else
                    {
                        Debug.LogWarning("Failed to parse rotation data.");
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }
    }

    void OnApplicationQuit()
    {
        serialPort.Close();
    }
}
