using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForFire : MonoBehaviour
{
    public int areaNum;
    void Start()
    {
        Turn.fireCamera.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}