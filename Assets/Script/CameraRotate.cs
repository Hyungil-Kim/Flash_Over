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
    private void CameraForObjectsCenter(GameObject[] objects)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            GetComponent<Camera>().transform.position = new Vector3(objects[i].transform.position.x, GetComponent<Camera>().transform.position.y, GetComponent<Camera>().transform.position.z);
        }

    }
}
