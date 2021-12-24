using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
	[HideInInspector]
	public Player targetPlayer;
	private Player pretargetPlayer;
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
	private GameObject target;
	public bool press;
	public GroundTile groundTile;

	/////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////
	public int num = 1;
	public int move =0;
	//////////////////////////////////////////////////////////////////////////////////


	public void Awake()
	{
		tilemapManager = GetComponent<TilemapManager>();
	}
	public void Start()
	{
		
	}
	public void GetTilePosition(Vector2 mousePosition)
	{
		mousePos = mousePosition;
	}
	public void GetClickedStartMouse()
	{
		
	}
	public void GetClickedEndMouse()
	{
		press = false;
		Debug.Log("Start");
		/////////////////////////////////////////////////////////////////////////////////////////////// 마우스 위치 저장
		Ray ray = Camera.main.ScreenPointToRay(mousePos);
		int layerMask = (1 << LayerMask.NameToLayer("GroundPanel"));
		layerMask = ~layerMask;
		if (Physics.Raycast(ray, out RaycastHit raycastHit, float.PositiveInfinity, layerMask))
		{
			target = raycastHit.transform.gameObject;// 레이 맞은 오브젝트
			targetTile = tilemapManager.ReturnTile(target);
			
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
						case PlayerState.Attack:
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
					case PlayerState.Attack:
						break;
					case PlayerState.End:
						break;
				}

				preTile = tilemapManager.ReturnTile(mousePos);	
			}
		}
		else
		{
			Debug.Log("No Target");
		}
	}
	public void GetClickingMouse()
	{
		press = true;

		if (targetPlayer != null && targetPlayer.curStateName == PlayerState.Move && (targetPlayer == pretargetPlayer || pretargetPlayer == null))
		{
			StartCoroutine(playerMove.Move(setPathColor, setMoveColor, targetPlayer,move));
		}
	}

	
}
