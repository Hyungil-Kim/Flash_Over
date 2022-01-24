using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset;
    private Camera camera;
    private GameManager gameManager;
    private Player target;
    private Fire fire;
    public List<Player> playerList = new List<Player>();
    private GroundTile groundTile;

    // Start is called before the first frame update
    void Start()
    {
        // players = GameObject.FindGameObjectsWithTag("Player");
        Turn.cameraController = this;
        camera = Camera.main;
        playerList = Turn.players;
        offset = new Vector3(0, 6, -3);
        gameManager = GameManager.instance;
        target = gameManager.targetPlayer;
        transform.position = playerList[0].transform.position + offset;

        //클릭 판단
    }
    // Update is called once per frame
    void LateUpdate()
    {
        
    }

    public void CameraMoving(Player player)
    {
        transform.position = player.transform.position + offset;
    }

 

    public void CameraRotationRight()
    { 
        if(target!=null)
        {
            StartCoroutine(CoTurnCameraRight(target.gameObject.transform.position));
        }
        else
        {
            RaycastHit hit;
            Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 11);
            var cameraCenter = hit.transform.position;
            StartCoroutine(CoTurnCameraRight(cameraCenter));
        }

    }
    public void CameraRotationLeft()
    {
        if (target != null)
        {
            StartCoroutine(CoTurnCameraLEft(target.gameObject.transform.position));
        }
        else
        {
            RaycastHit hit;
            Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 11);
            var cameraCenter = hit.transform.position;
            StartCoroutine(CoTurnCameraLEft(cameraCenter));
        }

    }
    IEnumerator CoTurnCameraRight(Vector3 cameraCenter)
    {
        for (int i = 0; i < 10; i++)
        {
            var angle = -9f;
            transform.RotateAround(cameraCenter, Vector3.up, angle);
            yield return 0;
        }
    }

    IEnumerator CoTurnCameraLEft(Vector3 cameraCenter)
    {
        for (int i = 0; i < 10; i++)
        {
            var angle = 9f;
            transform.RotateAround(cameraCenter, Vector3.up, angle);
            yield return 0;
        }
    }

}



