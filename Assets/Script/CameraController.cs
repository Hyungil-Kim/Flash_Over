using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject[] players;
    private Vector3 offset;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            var player = players[i].GetComponent<Player>();
            offset = transform.position - player.transform.position;
        }   
    }
    // Update is called once per frame
    void LateUpdate()
    {
        for (int i = 0; i < players.Length; i++)
        {
            var player = players[i].GetComponent<Player>();

            if (player.curStateName == PlayerState.End) return;
            transform.position = players[i].transform.position + offset;
        }
    }

    public void CameraRotateRight()
    {
        transform.RotateAround(transform.position, Vector3.up, 90.0f);
    }
    public void CameraRotateLeft()
    {
        transform.RotateAround(transform.position, Vector3.up, -90.0f);

    }
}
