using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyOfTheArea : MonoBehaviour
{
    private Color color;

    void Start()
    {
        color = new Color();
    }

    void Update()
    {

    }
    //private void OnCollisionEnter(Collision collision)
    //{

    //}
    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.collider.CompareTag("Wall"))
    //    {
    //        Debug.Log("찾음");
    //    }
    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.collider.CompareTag("Wall"))
    //    {
    //        Debug.Log("나감");
    //    }
    //}


    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;
        if (other.tag == "Wall" || other.tag == "DoorColider"|| other.tag == "Untagged" || other.tag =="Door")
            color = other.gameObject.GetComponent<MeshRenderer>().material.color;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other == null) return;
        if (other.tag == "Wall" || other.tag == "DoorColider" || other.tag == "Untagged" || other.tag == "Door")
        {
            other.gameObject.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, 0.33f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other == null) return;
        if (other.tag == "Wall" || other.tag == "DoorCollider" || other.tag == "Untagged" || other.tag == "Door")
        {
            other.gameObject.GetComponent<MeshRenderer>().material.color = color;
        }
    }
}
