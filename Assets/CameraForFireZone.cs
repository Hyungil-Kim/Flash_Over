using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForFireZone : MonoBehaviour
{
    public int areaNum;
    void Start()
    {
        Turn.areaCamera.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
