using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
   
    public void CameraRotateRight()
    {
        transform.RotateAround(transform.position, Vector3.up, 90.0f);
    }
    public void CameraRotateLeft()
    {
        transform.RotateAround(transform.position, Vector3.up, -90.0f);

    }
  
}
