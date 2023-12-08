using UnityEngine;
using System.IO.Ports;

public class RotationController : MonoBehaviour
{
    SerialPort serialPort = new SerialPort("COM9", 9600); // Update to your actual port

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

                    // Parse the received data
                    ParseData(System.Text.Encoding.UTF8.GetString(data));
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Exception: " + ex.Message);
            }
        }
    }

    void ParseData(string data)
    {
        // Split the data into parts based on the delimiters (',', ':')
        string[] parts = data.Split(',', ':');

        for (int i = 0; i < parts.Length; i++)
        {
            // Check for the desired data and parse the values
            if (parts[i] == "Pot")
            {
                int potValue = int.Parse(parts[i + 1]);
                RotateObject(potValue);
            }
        }
    }

    void RotateObject(int potValue)
    {
        // Rotate the object based on the potentiometer value
        float rotationAngle = Mathf.Lerp(0f, 360f, potValue / 1023f); // Assuming the potentiometer range is 0-1023
        transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
    }

    void OnApplicationQuit()
    {
        serialPort.Close();
    }
}
