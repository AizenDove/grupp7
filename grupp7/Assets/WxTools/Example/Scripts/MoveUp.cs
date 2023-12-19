using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WxTools.IO;

public class MoveUp : SerialDataTransciever
{

    Vector3 orgPos;
    float ratio;

    // Use this for initialization
    void Start()
    {
        orgPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(orgPos, orgPos + Vector3.up * 4.0f, ratio);

    }

    protected override void RecieveDataAsRatio01(float ratio)
    {
        this.ratio = ratio;
    }
}
