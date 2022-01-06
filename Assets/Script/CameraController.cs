using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject[] players;

    private Vector3 offset;
    private Camera camera;
    private GameManager gameManager;
    private Player target;
    private List<Player> playerList = new List<Player>();
    private VisionRange visionRange;
    private GroundTile groundTile;

    public GameObject area1;
    public GameObject area2;
    public GameObject area4;

    // Start is called before the first frame update
    void Start()
    {
        // players = GameObject.FindGameObjectsWithTag("Player");

        camera = Camera.main;
        playerList = Turn.players;
        for (int i = 0; i < playerList.Count; i++)
        {
            offset = transform.position - playerList[i].transform.position;
        }
        gameManager = GameManager.instance;
        //클릭 판단

    }
    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < playerList.Count; i++)
        //{
        //    camera.transform.LookAt(playerList[i].transform);

        //}
        //if (gameManager.targetPlayer != null)
        target = gameManager.targetPlayer;

        for (int i = 0; i < playerList.Count; i++)
        {
            //var player = players[i].GetComponent<Player>();

            //if (player.curStateName == PlayerState.End) return;
            //transform.position = players[i].transform.position + offset;
            //CameraForObjectsCenter(players);

            if (target != null)
            {
                Debug.Log("타겟이다");

                if (target.curStateName == PlayerState.Idle)
                {
                    transform.position = new Vector3(playerList[i].transform.position.x, transform.position.y, offset.z);
                  
                }
                if (target.curStateName == PlayerState.Move)
                {
                    //transform.position = new Vector3(playerList[i].transform.position.x, transform.position.y, offset.z);

                    transform.position = target.transform.position + offset;
                }
                if (target.curStateName == PlayerState.Action)
                {
                    transform.position = target.transform.position + offset;
                }
                //if (playerList[i].curStateName == PlayerState.End) return;
            }
            else if (target == null)
            {
                //Debug.Log("타겟이 널이다");
                if (playerList[i].curStateName == PlayerState.Idle)
                {
                    transform.position = new Vector3(playerList[i].transform.position.x, transform.position.y, playerList[i].transform.position.z + offset.z);
                }
                if (playerList[i].curStateName == PlayerState.Move)
                {
                    transform.position = playerList[i].transform.position + offset;
                }
                if (playerList[i].curStateName == PlayerState.Action)
                {
                    transform.position = playerList[i].transform.position + offset;
                }
                //if (playerList[i].curStateName == PlayerState.End) return;
            }

        }

    }
    private void CameraForObjectsCenter(GameObject[] objects)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            transform.position = new Vector3(objects[i].transform.position.x, transform.position.y, transform.position.z);
        }

    }
    public void CameraForObjectsCenter(GameObject objects)
    {
        transform.RotateAround(transform.position, Vector3.up, -90.0f);
        transform.position = new Vector3(objects.transform.position.x, transform.position.y, transform.position.z);
    }

    public void CameraByArea()
    {
        var vision = visionRange.crossResetQueue;
        vision.Contains(groundTile);
        if (groundTile.tileArea == 1)
        {
            camera.transform.position = area1.transform.position;
        }
        else if (groundTile.tileArea == 2)
        {
            camera.transform.position = area2.transform.position;
        } 
        else if (groundTile.tileArea == 4)
        {
            camera.transform.position = area4.transform.position;
        }
    }
}



