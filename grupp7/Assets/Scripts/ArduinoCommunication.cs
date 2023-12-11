using UnityEngine;
using System.IO.Ports;

public class ArduinoCommunication : MonoBehaviour
{
    SerialPort serialPort = new SerialPort("COM9", 9600); // Change COM3 to your Arduino's port

    public GameObject targetObject;

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
                int buttonState = int.Parse(serialPort.ReadLine());

                // Toggle GameObject visibility on button press
                if (buttonState == 1)
                {
                    if (targetObject.activeSelf)
                        targetObject.SetActive(false);
                    else
                        targetObject.SetActive(true);
                }
            }
            catch (System.Exception)
            {
                // Handle parsing errors or other exceptions
            }
        }
    }

    void OnApplicationQuit()
    {
        serialPort.Close();
    }
}
