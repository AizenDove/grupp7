using JetBrains.Annotations;
using UnityEngine;
using WxTools.IO;


public class SerialRotation : SerialDataTransciever
{
    [SerializeField]
    private Vector3 axis = Vector3.up;

    [SerializeField]
    private float maximumAngle = 360.0f;


    private float angle = 0;

    // Update is called once per frame
    void Update()
    {
        Quaternion q = Quaternion.AngleAxis(angle, axis);
        transform.rotation = q;
    }


    protected override void RecieveDataAsRatio02(float ratio1, float ratio2, float ratio3, float ratio4, float ratio5)
    {
        transform.Rotate(0f, ratio1 * 2 * Time.deltaTime, 0f);
    }
}
