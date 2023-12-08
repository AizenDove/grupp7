using UnityEngine;
using System.IO.Ports;

public class PotentiometerController : MonoBehaviour
{
    public GameObject objectPrefab;
    public float rotationSpeed = 5f;

    void Update()
    {
        // Read potentiometer value
        int potValue = Mathf.RoundToInt(Input.GetAxis("Vertical") * 1023); // Simulate potentiometer input

        // Rotate the object based on the potentiometer value
        RotateObject(potValue);
    }

    void RotateObject(int potValue)
    {
        // Rotate the object based on the potentiometer value
        float rotationAngle = Mathf.Lerp(0f, 360f, potValue / 1023f); // Assuming the potentiometer range is 0-1023
        transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
    }
}
