using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public Player targetPlayer;
	public Player pretargetPlayer;
	public GroundTile preTile;
	public TilemapManager tilemapManager;
	public PlayerMove playerMove; //player매니저로 옮길것인가?
	public Vector3 mousePos;
	public UIManager uIManager;
	public GroundTile targetTile;
	public Color setMoveColor;
	public Color setAttackColor;
	public Color setPathColor;
	public Tilemap tilemap;
	public GameObject target;
	public bool press;
	public GroundTile groundTile;
	public CameraController cameraController;

	/////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////
	public int num = -1;
	public int move = 0;
	private Vector3 mouse3DPos;
	private bool point;

	public MouseInput multiTouch;
	private float maxZoom = 60f;
	private float minZoom = 100f;
	public Vector3 mouseMove;
	public bool drag;

	private Vector3 prevPos;
	public bool isStart;

	public bool pickup;
	public bool putdown;

	public bool showMeleeRange;
	public bool showthrowwRange;

	public void Awake()
	{
		instance = this;
		tilemapManager = GetComponent<TilemapManager>();
	}
	public void Start()
	{
		//VisionCheck.Init();
		var characters = GameObject.FindGameObjectsWithTag("CreateCharacter");
        foreach (var character in characters)
        {
			character.GetComponent<CreateCharacter>().Create();
        }
		if(AllTileMesh.instance !=null)
		AllTileMesh.instance.Init();
	}
	public void GetTilePosition(Vector2 mousePosition)
	{
		mousePos = mousePosition;

	}
	public void GetClickedStartMouse(Vector2 callBack)
	{
		prevPos = callBack;
		//if (firstclick)
		//{
		//	firstclick = false;
		//	return;
		//}
		drag = true;

	}
	public Player changePlayer = null;
	public void CharacterChangeStart(Vector2 callBack)
	{
		Ray ray = Camera.main.ScreenPointToRay(mousePos);
		int layerMask = (1 << LayerMask.NameToLayer("GroundPanel") | 1 << LayerMask.NameToLayer("Fade"));
		layerMask = ~layerMask;
		if (Physics.Raycast(ray, out RaycastHit raycastHit, float.PositiveInfinity, layerMask))
		{
			target = raycastHit.transform.gameObject;// 레이 맞은 오브젝트
			targetTile = tilemapManager.ReturnTile(target);
			mouse3DPos = raycastHit.point;
		}
		else
		{
			target = null;
			targetTile = null;
		}
		if (target != null)
		{
			if (target.tag == "Player")
			{
				changePlayer = target.GetComponent<Player>();
			}
			else
            {
				changePlayer = null;
            }
		}
	}
	public void GetClickedEndMouse()
	{

		if (!point && isStart)
		{
			press = false;
			Debug.Log("Start");
			/////////////////////////////////////////////////////////////////////////////////////////////// 마우스 위치 저장
			Ray ray = Camera.main.ScreenPointToRay(mousePos);
			int layerMask = (1 << LayerMask.NameToLayer("GroundPanel") | 1 << LayerMask.NameToLayer("Fade"));
			layerMask = ~layerMask;
			if (Physics.Raycast(ray, out RaycastHit raycastHit, float.PositiveInfinity, layerMask))
			{
				target = raycastHit.transform.gameObject;// 레이 맞은 오브젝트
				targetTile = tilemapManager.ReturnTile(target);
				mouse3DPos = raycastHit.point;
			}
			else
			{
				target = null;
				targetTile = null;
			}
			/////////////////////////////////////////////////////////////////////////////////////////////// 
			if (target != null)
			{
				if (target.tag == "Player")
				{
					if (targetPlayer == null || targetPlayer.curStateName != PlayerState.Move)
					{
						targetPlayer = target.GetComponent<Player>(); // 현재 선택된 플레이어를 저장하기위해 사용
						Debug.Log(targetPlayer);
						//Debug.Log(pretargetPlayer);
						switch (targetPlayer.curStateName)
						{
							case PlayerState.Idle:
								tilemapManager.ShowMoveRange(targetTile, targetPlayer, pretargetPlayer, setMoveColor);
								playerMove.ResetMoveList();
								playerMove.AddMoveList(targetTile.transform.position, setPathColor);
								targetPlayer.ChangeState(PlayerState.Move);
								break;
							case PlayerState.Move:
								break;
							case PlayerState.Action:
								tilemapManager.ChangeColorAttack(targetPlayer.gameObject, num, setAttackColor);
								break;
							case PlayerState.End:
								break;
						}
						pretargetPlayer = targetPlayer;
						preTile = tilemapManager.ReturnTile(targetPlayer.gameObject);
					}
				}
				else if (target.tag == "Ground" && targetTile.movefloodFill && preTile.fillList.Contains(targetTile.gameObject))//땅클릭 + 이동범위인지
				{
					switch (targetPlayer.curStateName)
					{
						case PlayerState.Idle:
							break;
						case PlayerState.Move:
							targetPlayer.moveHelper.SetActive(true);
							targetPlayer.moveHelper.transform.position = new Vector3(targetTile.transform.position.x, targetPlayer.transform.position.y, targetTile.transform.position.z);
							break;
						case PlayerState.Action:
							break;
						case PlayerState.End:
							break;
					}

					preTile = tilemapManager.ReturnTile(mousePos);
				}
				else if (pickup)
				{
					var playerTile = tilemapManager.ReturnTile(targetPlayer.gameObject);
					if (target.tag == "Claimant" && targetTile.nextTileList.Contains(playerTile))
					{
						target.GetComponent<Claimant>().SetState(ClaimantState.Resuce);
						playerMove.moveList.Add(target.transform.position);
						targetPlayer.handList.Add(target);
						uIManager.battleUiManager.rescueButton.gameObject.SetActive(false);
						playerMove.moveList.Add(targetPlayer.transform.position);
						//target.SetActive(false);
						playerMove.go = true;
						targetPlayer.handFull = true;
						pickup = false;
					}
				}
				else if (putdown)
				{
					var playerTile = tilemapManager.ReturnTile(targetPlayer.gameObject);
					if (target.tag == "Ground" && targetTile.nextTileList.Contains(playerTile))
					{
						var hand = targetPlayer.handList[0];
						playerMove.moveList.Add(target.transform.position);
						uIManager.battleUiManager.putDownButton.gameObject.SetActive(false);
						playerMove.moveList.Add(targetPlayer.transform.position);
						hand.transform.position = new Vector3(target.transform.position.x, targetPlayer.handList[0].transform.position.y, target.transform.position.z);
						//hand.SetActive(true);
						if(hand.tag == "Claimant")
							hand.GetComponent<Claimant>().SetState(ClaimantState.End);
						targetPlayer.handList.RemoveAt(0);
						playerMove.go = true;
						targetPlayer.handFull = false;
						putdown = false;
					}
				}
				else if (showMeleeRange && !showthrowwRange)
				{
					if (target.tag == "Ground" && uIManager.battleUiManager.useItemManager.listRange.Contains(targetTile))
					{
						
						StartCoroutine(uIManager.battleUiManager.useItemManager.UseItem());
					}
				}
				else if (showMeleeRange && showthrowwRange)
				{
					if (target.tag == "Ground" && uIManager.battleUiManager.useItemManager.listRange.Contains(targetTile))
					{

						
					}
				}


				if (targetPlayer != null && targetPlayer.curStateName == PlayerState.Action)
				{
					var fixedPos = mouse3DPos - targetPlayer.transform.position;
					if (0 < fixedPos.x && Mathf.Abs(fixedPos.x) > Mathf.Abs(fixedPos.z))//오
					{
						Debug.Log("right");
						targetPlayer.transform.rotation = Quaternion.Euler(0, 90, 0);
						tilemapManager.ChangeColorAttack(targetPlayer.gameObject, num, setAttackColor);
					}
					else if (0 > fixedPos.x && Mathf.Abs(fixedPos.x) >= Mathf.Abs(fixedPos.z))//왼
					{
						Debug.Log("left");
						targetPlayer.transform.rotation = Quaternion.Euler(0, 270, 0);
						tilemapManager.ChangeColorAttack(targetPlayer.gameObject, num, setAttackColor);
					}
					else if (0 < fixedPos.z && Mathf.Abs(fixedPos.z) > Mathf.Abs(fixedPos.x))//위
					{
						Debug.Log("up");
						targetPlayer.transform.rotation = Quaternion.Euler(0, 0, 0);
						tilemapManager.ChangeColorAttack(targetPlayer.gameObject, num, setAttackColor);
					}
					else if (0 > fixedPos.z && Mathf.Abs(fixedPos.z) >= Mathf.Abs(fixedPos.x))//아래
					{
						Debug.Log("down");
						targetPlayer.transform.rotation = Quaternion.Euler(0, 180, 0);
						tilemapManager.ChangeColorAttack(targetPlayer.gameObject, num, setAttackColor);
					}
				}
			}
			else
			{
				Debug.Log("No Target");
			}
		}
	}
	public void CharacterChanageEnd()
    {
		Ray ray = Camera.main.ScreenPointToRay(mousePos);
		int layerMask = (1 << LayerMask.NameToLayer("GroundPanel") | 1 << LayerMask.NameToLayer("Fade"));
		layerMask = ~layerMask;
		if (Physics.Raycast(ray, out RaycastHit raycastHit, float.PositiveInfinity, layerMask))
		{
			target = raycastHit.transform.gameObject;// 레이 맞은 오브젝트
			targetTile = tilemapManager.ReturnTile(target);
			mouse3DPos = raycastHit.point;
		}
		else
		{
			target = null;
			targetTile = null;
		}
		if (target != null)
		{
			if (target.tag == "Player")
			{
				changePlayer = target.GetComponent<Player>();
			}
			else
			{
				changePlayer = null;
			}
		}
	}
	public void GetClickingMouse()
	{
		if (!point && isStart)
		{
			press = true;
			if (targetPlayer != null && targetPlayer.curStateName == PlayerState.Move && (targetPlayer == pretargetPlayer || pretargetPlayer == null))
			{
				StartCoroutine(playerMove.Move(setPathColor, setMoveColor, targetPlayer, move));
			}
		}
	}

	public void ChangeMousePointer()
    {
		
    }

	private bool IsPointerOverUI()
	{
		return EventSystem.current.IsPointerOverGameObject();
	}




	public void Update()
	{
		//mousePos = multiTouch.mousePoint.Mouse.Move.ReadValue<Vector2>();
		var pointer = IsPointerOverUI();
		if (pointer)
		{
			point = true;
		}
		else
		{
			point = false;
		}

		if (multiTouch.Zoom != 0f)
		{
			var view = Camera.main.fieldOfView;
			var change = view * (1 + multiTouch.Zoom);
			Camera.main.fieldOfView = (change > minZoom) ? minZoom : change;
			Camera.main.fieldOfView = (change < maxZoom) ? maxZoom : change;
		}
	}
	public void LateUpdate()
	{
		if(drag && isStart)
		{
			CameraMove();
		}
	}
	public void CameraMove()
	{
		var currPos = mousePos;
		currPos.z = 10f;
		var pos1 = Camera.main.ScreenToWorldPoint(currPos);
		prevPos.z = 10f;
		var pos2 = Camera.main.ScreenToWorldPoint(prevPos);

		var delta = pos2 - pos1;

		delta.y = 0f;

		Camera.main.transform.position = Camera.main.transform.position + delta;

		prevPos = currPos;
	}
}
