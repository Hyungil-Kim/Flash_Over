using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;
using System.Linq;
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
	public AudioController audioController;
	public bool readyPlayerAction = true;
	public int num = -1;
	public int move;
	private Vector3 mouse3DPos;
	public bool point;
	public bool tutorial;
	public MouseInput multiTouch;
	private float maxZoom = 60f;
	private float minZoom = 100f;
	public Vector3 mouseMove;
	public bool drag;

	public Vector3 prevPos;
	public bool isStart;

	public bool pickup;
	public bool putdown;

	public bool showMeleeRange;
	public bool showthrowwRange;

	public int turnCount;
	public TextMeshProUGUI tMPro;
	public TextMeshProUGUI pressPro;
	public TextMeshProUGUI ready;

	public bool isUsingMap;
	public int escape;
	public List<GroundTile> exitTiles;

	public void Awake()
	{
		instance = this;
		tilemapManager = GetComponent<TilemapManager>();
		audioController = Camera.main.GetComponent<AudioController>();
	}
	public void Start()
	{
		Turn.lose = false;
		Turn.win = false;
		//VisionCheck.Init();
		var characters = GameObject.FindGameObjectsWithTag("CreateCharacter");

		if (PlaySaveSystem.ps != null)
		{

			foreach (var tile in AllTile.allTile)
			{
				if (PlaySaveSystem.ps.tsd.TryGetValue(tile.index, out var tiledata))
				{
					tile.SaveInit(tiledata);
				}
			}
			foreach (var character in characters)
			{

				var create = character.GetComponent<CreateCharacter>();
				var playerDict = PlaySaveSystem.ps.psd;
				if (playerDict.ContainsKey(create.characterIndex))
				{
					create.Create(playerDict[create.characterIndex].cd);
					var player = create.Character.GetComponent<Player>();
					player.SaveInit(playerDict[create.characterIndex]);
					//if (player.playerState == PlayerState.Move)
					//{
					//	player.playerState = PlayerState.Idle;
					//}
				}
			}
			targetPlayer = Turn.players.Find((x) => x.index == PlaySaveSystem.ps.gsd.targetIndex);
			if (targetPlayer != null)
			{
				if (targetPlayer.playerState == PlayerState.Move)
				{
					targetTile = tilemapManager.ReturnTile(targetPlayer.gameObject);
					tilemapManager.ShowMoveRange(targetTile, targetPlayer, pretargetPlayer, setMoveColor);
					playerMove.ResetMoveList();
					playerMove.AddMoveList(targetTile.transform.position, setPathColor);
					pretargetPlayer = targetPlayer;
					preTile = tilemapManager.ReturnTile(targetPlayer.gameObject);
					uIManager.OnCharacterInfo();
					uIManager.InfoUiScript.smallInfo.Init();
				}
				if (targetPlayer.playerState == PlayerState.Idle || targetPlayer.playerState == PlayerState.End)
				{
					targetPlayer = null;
					
				}
			}

			foreach (var claimant in Turn.claimants)
			{
				if (PlaySaveSystem.ps.csd.TryGetValue(claimant.index, out var claimantData))
				{
					claimant.SaveInit(claimantData);
				}
			}

			turnCount = PlaySaveSystem.ps.gsd.turnCount;

			uIManager.StartGame();
			PlaySaveSystem.ps = null;
		}
		else
		{
			foreach (var character in characters)
			{
				var create = character.GetComponent<CreateCharacter>();
				if (GameData.userData.fireManList.ContainsKey(create.characterIndex))
				{
					create.Create(GameData.userData.fireManList[create.characterIndex]);
				}
				else if (GameData.userData.fireManList.Count == 0)
				{
					create.Create(null);
				}
			}
		}
		//PlaySaveSystem.ps.isPlay = true;

		Turn.players = Turn.players.OrderBy((x) => x.index).ToList();

		
	}
	public void StartGame()
	{
		uIManager.StartGame();
		audioController.ChangeAudioClip(1);

        foreach (var fireman in Turn.players)
        {
            foreach (var characteristic in fireman.cd.buff)
            {
				if(characteristic.buffTiming.Check(BuffTiming.BuffTimingEnum.GameStart))
                {
					characteristic.StartBuff();
				}
            }
        }
	}
	public void Init()
	{
		if (AllTileMesh.instance != null)
		{
			AllTileMesh.instance.Init();
			if (Turn.players.Count != 0 && PlaySaveSystem.ps == null)
			{
				targetPlayer = Turn.players[0];
				ChangeTargetPlayer(Turn.players[0].gameObject);
				Debug.Log("in");
			}
			else if (targetPlayer != null)
			{
				ChangeTargetPlayer(targetPlayer.gameObject);
			}

		}
	}
	public void GetTilePosition(Vector2 mousePosition)
	{
		if(!point)
		mousePos = mousePosition;

	}
	public void GetClickedStartMouse(Vector2 callBack)
	{
		if (!point)
		{
			prevPos = callBack;
			drag = true;
		}
	}
	public Player changePlayer = null;
	public CreateCharacter change = null;
	public void CharacterChangeStart(Vector2 callBack)
	{
		if (!isStart)
		{
			Ray ray = Camera.main.ScreenPointToRay(mousePos);
			int layerMask = (1 << LayerMask.NameToLayer("GroundPanel") | 1 << LayerMask.NameToLayer("Fade") | 1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Exit") | 1 << LayerMask.NameToLayer("Obstacle"));
			if(tutorial)
			{
				layerMask = layerMask | (1 << LayerMask.NameToLayer("Green"));
			}
			layerMask = ~layerMask;

			RaycastHit[] hits;
			hits = Physics.RaycastAll(ray, float.PositiveInfinity, layerMask);
			foreach (var hit in hits)
			{
				var target = hit.transform.gameObject;
				if (target.tag == "Player")
				{
					changePlayer = target.GetComponent<Player>();
					change = target.GetComponentInParent<CreateCharacter>();
					uIManager.OnCharacterIcon();
					uIManager.OnCharacterInfo();
					uIManager.InfoUiScript.smallInfo.Init();
					break;
				}
				else if (target.tag == "CreateQuad")
				{
					change = target.GetComponentInParent<CreateCharacter>();
					changePlayer = change.GetComponentInChildren<Player>();
					uIManager.OnCharacterIcon();
					uIManager.OnCharacterInfo();
					uIManager.InfoUiScript.smallInfo.Init();
				}
				else
				{
					changePlayer = null;
					change = null;
				}
			}

			//	if (Physics.Raycast(ray, out RaycastHit raycastHit, float.PositiveInfinity, layerMask))
			//{	
			//	target = raycastHit.transform.gameObject;// 레이 맞은 오브젝트
			//	targetTile = tilemapManager.ReturnTile(target);
			//	mouse3DPos = raycastHit.point;
			//}
			//else
			//{
			//	target = null;
			//	targetTile = null;
			//}
			//if (target != null)
			//{
			//	if (target.tag == "Player")
			//	{
			//		changePlayer = target.GetComponent<Player>();
			//		change = target.GetComponentInParent<CreateCharacter>();
			//		uIManager.OnCharacterIcon();
			//		uIManager.OnCharacterInfo();
			//		uIManager.info.Init();
			//	}
			//	else
			//	{
			//		changePlayer = null;
			//		change = null;
			//	}
			//}
		}
	}
	public static void ChangeLayersRecursively(Transform trans, string name)
	{
		trans.gameObject.layer = LayerMask.NameToLayer(name);
		foreach (Transform child in trans)
		{
			ChangeLayersRecursively(child, name);
		}
	}
	public void ChangeTargetPlayer(GameObject go)
	{
		target = go;// 레이 맞은 오브젝트s
		targetTile = tilemapManager.ReturnTile(target);
		var player = target.GetComponent<Player>();
		ChangeLayersRecursively(player.transform, "Green");
		ChangeLayersRecursively(player.Fire_Hose.transform, "Player");

		cameraController.CameraMoving(player);
		if ((player.curStateName == PlayerState.Idle || player.curStateName == PlayerState.Action || player.curStateName == PlayerState.End))
		{
			switch (player.curStateName)
			{
				case PlayerState.Idle:
					targetPlayer = target.GetComponent<Player>(); // 현재 선택된 플레이어를 저장하기위해 사용
					tilemapManager.ShowMoveRange(targetTile, targetPlayer, pretargetPlayer, setMoveColor);
					playerMove.ResetMoveList();
					playerMove.AddMoveList(targetTile.transform.position, setPathColor);
					targetPlayer.ChangeState(PlayerState.Move);//버튼띄우기
					break;
				case PlayerState.Move:
					break;
				case PlayerState.Action:
					targetPlayer = target.GetComponent<Player>();

					//버튼 띄우기
					break;
				case PlayerState.End:
					//targetPlayer = target.GetComponent<Player>();
					//ChangeLayersRecursively(targetPlayer.transform, "Player");
					break;
			}
			pretargetPlayer = targetPlayer;
			preTile = tilemapManager.ReturnTile(targetPlayer.gameObject);
		}
		targetPlayer = target.GetComponent<Player>();
		uIManager.OnCharacterInfo();
		uIManager.InfoUiScript.smallInfo.Init();
		if (tutorial)
		{
			StartCoroutine(Turn.CoTurnSystem());
		}
	}





    public void GetClickedEndMouse()
	{
		press = false;
		if (!point && isStart && !uIManager.InfoUiScript.tileInfo.on)
		{

			uIManager.InfoUiScript.ChangeInfoUi(3);

			Debug.Log("Start");

			/////////////////////////////////////////////////////////////////////////////////////////////// 마우스 위치 저장
			Ray ray = Camera.main.ScreenPointToRay(mousePos);
			int layerMask = (1 << LayerMask.NameToLayer("GroundPanel") | 1 << LayerMask.NameToLayer("Fade") | 1 << LayerMask.NameToLayer("Obstacle"));
			if (targetPlayer != null)
			{
				if (targetPlayer.curStateName == PlayerState.Move)
				{
					layerMask = (1 << LayerMask.NameToLayer("GroundPanel") | 1 << LayerMask.NameToLayer("Fade") | 1 << LayerMask.NameToLayer("Obstacle") | 1 << LayerMask.NameToLayer("Door"));
				}
			}
			if (tutorial)
			{
				layerMask = layerMask | (1 << LayerMask.NameToLayer("Green"));
			}
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
					if ((targetPlayer == null && target.GetComponent<Player>().curStateName == PlayerState.Idle) || (readyPlayerAction && target.GetComponent<Player>().curStateName == PlayerState.Action))
					{
						targetPlayer = target.GetComponent<Player>(); // 현재 선택된 플레이어를 저장하기위해 사용
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
								break;
							case PlayerState.End:
								break;
						}
						pretargetPlayer = targetPlayer;
						preTile = tilemapManager.ReturnTile(targetPlayer.gameObject);
					}
					if (targetPlayer != target)
					{
						uIManager.InfoUiScript.ChangeInfoUi(0);
						uIManager.InfoUiScript.charaterInfo.UpdateData(target.GetComponent<Player>());
					}
					else
					{
					uIManager.InfoUiScript.smallInfo.Init();
					uIManager.OnCharacterInfo();
					}
				}
				else if (pickup)
				{
					PickUp();
				}
				else if (putdown)
				{
					PutDown();
				}
				else if (showMeleeRange)
				{
					UseItemMeleeRange();
				}
				else if (showthrowwRange)
				{
					UseItemthrowRange();
				}
				else if (target.tag == "Ground")//땅클릭 + 이동범위인지
				{
					if (targetTile.movefloodFill && preTile.nextTileList.Contains(targetTile))
					{
						switch (targetPlayer.curStateName)
						{
							case PlayerState.Idle:
								break;
							case PlayerState.Move:
								targetPlayer.moveHelper.SetActive(true);
								//targetPlayer.moveHelper.transform.localPosition = Vector3.zero;
								break;
							case PlayerState.Action:
								break;
							case PlayerState.End:
								break;
						}
						preTile = tilemapManager.ReturnTile(targetTile.gameObject);
					}
					else 
					{
						if (num == -1)
						{
							if (targetPlayer.moveHelper.activeSelf)
							{
								if (targetTile != tilemapManager.ReturnTile(targetPlayer.moveHelper))
								{
									uIManager.InfoUiScript.ChangeInfoUi(1);
									uIManager.InfoUiScript.tileInfo.UpdateTileInfo();
								}
							}
							else
							{
								if (targetTile != tilemapManager.ReturnTile(targetPlayer.gameObject))
								{
									uIManager.InfoUiScript.ChangeInfoUi(1);
									uIManager.InfoUiScript.tileInfo.UpdateTileInfo();
								}
							}
						}
						//uIManager.InfoUiScript.tileInfo.GetComponent<TileInfo>().UpdateTileInfo();
					}
				}
				else if (target.tag == "Claimant")
				{
					uIManager.InfoUiScript.ChangeInfoUi(2);
					uIManager.InfoUiScript.claimantInfo.UpdateClaimantInfo();
				}

				if (targetPlayer != null && targetPlayer.curStateName == PlayerState.Action && !targetPlayer.isPlayerParticle)
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
		else 
		{
			if (!point)
			{
				if (uIManager.InfoUiScript.tileInfo.on)
				{
					uIManager.InfoUiScript.tileInfo.on = false;
					uIManager.InfoUiScript.ChangeInfoUi(3);
				}
			}
		}
		
	}
	public void CharacterChanageEnd()
	{
		if (!isStart)
		{
			Ray ray = Camera.main.ScreenPointToRay(mousePos);
			int layerMask = (1 << LayerMask.NameToLayer("GroundPanel") | 1 << LayerMask.NameToLayer("Fade") | 1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Exit"));
			layerMask = ~layerMask;
			RaycastHit[] hits;


			hits = Physics.RaycastAll(ray, float.PositiveInfinity, layerMask);
			foreach (var hit in hits)
			{
				var target = hit.transform.gameObject;
				if (target.tag == "CreateQuad")
				{
					var createCharacter = target.GetComponentInParent<CreateCharacter>();
					var createCharacterdata = createCharacter.GetComponentInChildren<Player>();
					if (change != null)
					{
						if (createCharacter != change)
						{
							createCharacter.ChangeCharacter(changePlayer.cd);
							change.DeleteCharacter();
							if (createCharacterdata != null)
							{
								change.ChangeCharacter(createCharacterdata.cd);
							}
						}
					}
					break;
				}
			}
			if (hits.Length > 0)
			{
				if (changePlayer == null)
				{
					uIManager.OffCharacterInfo();
				}
				uIManager.OffCharacterIcon();
				changePlayer = null;
				change = null;
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
		if (!point && isStart)
		{
			press = true;

			if (targetPlayer != null && targetPlayer.curStateName == PlayerState.Move && (targetPlayer == pretargetPlayer || pretargetPlayer == null))
			{
				StartCoroutine(playerMove.Move(setPathColor, setMoveColor, targetPlayer, move));
			}
		}
	}

	private bool IsPointerOverUI()
	{
		return EventSystem.current.IsPointerOverGameObject();
	}




	public void Update()
	{
		var t = turnCount;
		uIManager.upperUIManager.turn.text = t.ToString();
		//tMPro.text = Turn.players.Count.ToString();
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

		if(targetPlayer != null && targetPlayer.handFull)
		{
			var angles = targetPlayer.handList[0].transform.rotation.eulerAngles;
			var playerAngle = targetPlayer.transform.rotation.eulerAngles;
			angles.y = playerAngle.y + 90;
			angles.z = playerAngle.z + 1;
			targetPlayer.handList[0].transform.rotation = Quaternion.Euler(angles);
		}
	}
	public void LateUpdate()
	{
		if (drag && isStart && !tutorial && !point)
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

	public void GameClear()
	{
		foreach (var player in Turn.players)
		{
			player.cd.tiredScore += turnCount * 10;
		}

		uIManager.gameclearUI.gameObject.SetActive(true);
		uIManager.gameclearUI.Init();
	}

	public void PickUp()
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
			targetPlayer.cd.oxygen -= 3;
			if (targetPlayer.cd.oxygen < 0)
			{
				targetPlayer.lung -= targetPlayer.cd.oxygen;
				targetPlayer.cd.oxygen = 0;
			}
		}
	}
	public void PutDown()
	{
		var playerTile = tilemapManager.ReturnTile(targetPlayer.gameObject);
		if (target.tag == "Ground" && playerTile.nextTileList.Contains(targetTile))
		{
			var hand = targetPlayer.handList[0];
			playerMove.moveList.Add(target.transform.position);
			uIManager.battleUiManager.putDownButton.gameObject.SetActive(false);
			playerMove.moveList.Add(targetPlayer.transform.position);
			hand.transform.position = new Vector3(target.transform.position.x, targetPlayer.handList[0].transform.position.y, target.transform.position.z);
			//hand.SetActive(true);
			if (hand.tag == "Claimant")
			{
				hand.GetComponent<Claimant>().SetState(ClaimantState.End);
			}

			targetPlayer.handList.RemoveAt(0);
			playerMove.go = true;
			targetPlayer.handFull = false;
			targetPlayer.cd.oxygen -= 3;
			if (targetPlayer.cd.oxygen < 0)
			{
				targetPlayer.lung -= targetPlayer.cd.oxygen;
				targetPlayer.cd.oxygen = 0;
			}
			//if (target.GetComponent<GroundTile>().safeArea)
			//{
			//	hand.GetComponent<Claimant>().num =4;
			//}
			//else
			//{
			//	hand.GetComponent<Claimant>().num = 3;
			//}

			if (target.GetComponent<GroundTile>().isExit)
			{
				tilemapManager.Escape(hand.GetComponent<Claimant>());
			}

		}
	}
	public void UseItemMeleeRange()
	{
		var useitem = uIManager.battleUiManager.useItemManager;
		if (useitem.listRange.Contains(targetTile))
		{
			if (useitem.itemType == UseItemType.Heal)
			{
				if (target.tag == "Claimant")
				{
					var claimant = target.GetComponent<Claimant>();
					switch (useitem.healItemType)
					{
						case HealItemType.HP:
							claimant.hp += useitem.damage;
							break;
						case HealItemType.MP:
							break;
					}
					StartCoroutine(useitem.UseItemEnd(targetPlayer));
					showMeleeRange = false;
				}
				else if (target.tag == "Player")
				{
					var player = target.GetComponent<Player>();
					if (player.handFull)
						return;
					switch (useitem.healItemType)
					{
						case HealItemType.HP:
							player.cd.hp += useitem.damage;
							break;
						case HealItemType.MP:
							break;
					}
					StartCoroutine(useitem.UseItemEnd(targetPlayer));
					showMeleeRange = false;
				}
			}
			else if (useitem.itemType == UseItemType.Damage)
			{
				var fire = GetComponentInChildren<Fire>();
				fire.fireHp -= useitem.damage;
				StartCoroutine(useitem.UseItemEnd(targetPlayer));
				showMeleeRange = false;
			}
		}
	}
	public void UseItemthrowRange()
	{
		var useitem = uIManager.battleUiManager.useItemManager;
		if (useitem.listRange.Contains(targetTile))
		{
			if (useitem.preTile == null)
			{
				useitem.throwListRange = tilemapManager.ReturnFloodFillRange(targetTile, Color.black, useitem.throwRange);
				useitem.preTile = targetTile;
			}
			else if (useitem.preTile != targetTile)
			{
				tilemapManager.ResetFloodFill(useitem.throwListRange);
				var playerTile = tilemapManager.ReturnTile(targetPlayer.gameObject);
				useitem.listRange = tilemapManager.ReturnFloodFillRange(playerTile, setMoveColor, useitem.itemRange);
				foreach (var elem in useitem.listRange)
				{
					elem.ResetExceptColor();
				}
				useitem.throwListRange = tilemapManager.ReturnFloodFillRange(targetTile, Color.black, useitem.throwRange);
				useitem.preTile = targetTile;
			}
			else if (useitem.preTile == targetTile)
			{
				StartCoroutine(targetPlayer.ThrowAnimation());
				if (useitem.itemType == UseItemType.Heal)
				{
					foreach (var elem in useitem.throwListRange)
					{
						foreach (var ob in elem.fillList)
						{
							if (ob.tag == "Player")
							{
								var player = target.GetComponent<Player>();
								switch (useitem.healItemType)
								{
									case HealItemType.HP:
										player.cd.hp += useitem.damage;
										break;
									case HealItemType.MP:
										break;
								}
								break;
							}
							else if (ob.tag == "Claimant")
							{
								var claimant = target.GetComponent<Claimant>();
								switch (useitem.healItemType)
								{
									case HealItemType.HP:
										claimant.hp += useitem.damage;
										break;
									case HealItemType.MP:
										break;
								}
								break;
							}
						}
					}
				}
				else if (useitem.itemType == UseItemType.Damage)
				{
					foreach (var elem in useitem.throwListRange)
					{
						if (elem.tileIsFire)
						{
							var fire = elem.GetComponentInChildren<Fire>();
							fire.fireHp -= (float)useitem.damage;
						}
					}
				}
				StartCoroutine(useitem.UseItemEnd(targetPlayer));
				showthrowwRange = false;
			}
		}
	}


	public void TurnSystem()
	{
		StartCoroutine(Turn.CoTurnSystem());
	}
}
