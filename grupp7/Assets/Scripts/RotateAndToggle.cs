using UnityEngine;
using System.IO.Ports;

public class RotateAndToggle : MonoBehaviour
{
    public string portName = "COM9"; // Set this to your Arduino port
    public int baudRate = 9600;

    SerialPort serialPort;
    public float rotationSpeed = 5f;
    public GameObject objectToToggle;

    bool isObjectActive = true;

    void Start()
    {
        serialPort = new SerialPort(portName, baudRate);
        serialPort.Open();
    }

    void Update()
    {
        if (serialPort.IsOpen)
        {
            try
            {
                // Read the potentiometer value from Arduino
                int sensorValue = int.Parse(serialPort.ReadLine());

                // Map the sensor value to the rotation angle
                float angle = Mathf.Lerp(0f, 360f, sensorValue / 1023f);

                // Rotate the GameObject
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                // Check button state to toggle GameObject
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isObjectActive = !isObjectActive;
                    objectToToggle.SetActive(isObjectActive);
                }
            }
            catch (System.Exception)
            {
                // Handle exceptions, if any
            }
        }
    }

    void OnDestroy()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
