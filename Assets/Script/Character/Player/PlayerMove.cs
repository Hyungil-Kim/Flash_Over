using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	private GameManager gameManager;
	private TilemapManager tilemapManager;
	public List<Vector3> moveList = new List<Vector3>();
	private Player moveObject;
	public float speed = 3f;
	public bool go = false;
	private int num = 0;
	private bool hit = false;
	private bool breath = false;
	public void Awake()
	{
		gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
	}
	public void Start()
	{
		tilemapManager = gameManager.tilemapManager;
	}
	public void AddMoveList(Vector3 curPos, Color setPathColor)
	{
		moveList.Add(curPos);
		var tile = tilemapManager.ReturnTile(curPos);
		tile.SetTileColor(setPathColor);

	}
	public void RemoveMoveList(List<Vector3> moveList, Color setMoveColor)
	{
		var pretile = moveList[moveList.Count - 1];
		moveList.RemoveAt(moveList.Count - 1);

		if (!moveList.Contains(pretile))
		{
			var tile = tilemapManager.ReturnTile(pretile);
			tile.SetTileColor(setMoveColor);

		}

	}
	public void ResetMoveList()
	{
		moveList.Clear();
	}
	public void MovePlayer()
	{
		moveObject = gameManager.targetPlayer;
		if (!tilemapManager.CheckPlayer(moveObject.moveHelper) || tilemapManager.ReturnTile(moveObject.gameObject) == tilemapManager.ReturnTile(moveObject.moveHelper))
		{
			if (tilemapManager.ReturnTile(moveObject.moveHelper).isClaimant && !moveObject.handFull) return;
			moveObject.moveHelper.transform.localPosition = Vector3.zero;
			moveObject.moveHelper.gameObject.SetActive(false);
			moveObject.animator.SetBool("walk", true);
			go = true;
		}
	}
	public IEnumerator Move(Color setPathColor, Color setMoveColor, Player player, int movePoint)
	{
		if (!go)
		{
			var moveHelper = player.moveHelper;
			var helperTile = tilemapManager.ReturnTile(moveHelper);

			while (gameManager.press)
			{
				var mousePos = gameManager.mousePos;
				Ray ray = Camera.main.ScreenPointToRay(mousePos);
				int layerMask = (1 << LayerMask.NameToLayer("GroundPanel") | 1 << LayerMask.NameToLayer("Fade") | 1 << LayerMask.NameToLayer("Obstacle"));
				layerMask = ~layerMask;
				if (Physics.Raycast(ray, out RaycastHit raycastHit, float.PositiveInfinity, layerMask))
				{
					var targetTile = tilemapManager.ReturnTile(raycastHit.transform.position);
					var tilePos = targetTile.transform.position;
					if (targetTile.movefloodFill || gameManager.playerMove.moveList.Contains(tilePos))
					{
						gameManager.drag = false;
					}
					else
					{
						gameManager.drag = true;
					}
					//var curTile = tilemapManager.ReturnTile(moveList[moveList.Count - 1]);
					if (moveList.Count == 0) yield break;
					//현재타일인지 // (이동범위 안 인지 || 이전타일인지) //다음 이동리스트에 있는지
					if (tilePos != moveList[moveList.Count - 1] && (targetTile.movefloodFill || (moveList.Count > 1 && tilePos == moveList[moveList.Count - 2])) && targetTile.nextTileList.Contains(tilemapManager.ReturnTile(moveList[moveList.Count - 1])))
					{
						if ((moveList.Count > 1 && tilePos == moveList[moveList.Count - 2]))
						{
							var preTile = tilemapManager.ReturnTile(moveList[moveList.Count - 2]);
							RemoveMoveList(moveList, setMoveColor);
							movePoint += 1;
						}
						else
						{
							AddMoveList(tilePos, setPathColor);
							movePoint -= 1;
						}
						moveHelper.transform.position = new Vector3(tilePos.x, player.transform.position.y, tilePos.z);
						//moveHelper.transform.LookAt();--분신 보는방향
						tilemapManager.DrawFloodFill(tilePos, moveList, movePoint, setMoveColor, setPathColor);
					}
				}
				gameManager.move = movePoint;
				yield return 0;
			}
		}
	}


	public void Update()
	{
		if (go)
		{
			gameManager.uIManager.battleUiManager.moveButton.gameObject.SetActive(false);
			gameManager.uIManager.battleUiManager.cancleButton.gameObject.SetActive(false);
			gameManager.uIManager.battleUiManager.selectNextPlayer.gameObject.SetActive(false);
			//움직일때 따라가는거?
			Camera.main.transform.position = new Vector3(moveObject.transform.position.x, Camera.main.transform.position.y, moveObject.transform.position.z - 3);
			if (moveList.Count == 0)
			{
				num = 0;
				go = false;
				StartCoroutine(gameManager.tilemapManager.MoveEnd(moveObject));
				return;
			}
			var newPos = new Vector3(moveList[num].x, moveObject.transform.position.y, moveList[num].z);
			if (moveObject.transform.position != newPos)
			{
				var dis = Vector3.Distance(moveObject.transform.position, newPos);
				if (dis > 0)
				{
					moveObject.transform.position = Vector3.MoveTowards(moveObject.transform.position, newPos, speed * Time.deltaTime);
					moveObject.transform.LookAt(newPos);
					if (moveObject.handList.Count != 0 && moveObject.handList[0].tag == "Claimant")
					{
						moveObject.handList[0].transform.position = new Vector3(moveObject.transform.position.x, moveObject.handList[0].transform.position.y, moveObject.transform.position.z);
						var handObPos = new Vector3(moveObject.handList[0].transform.position.x, moveObject.handList[0].transform.position.y, moveObject.handList[0].transform.position.z);
						moveObject.handList[0].transform.LookAt(handObPos);
					}
					hitCheck(newPos);
					BreathCheck(newPos);
				}
			}
			else
			{
				AllTileMesh.instance.UpdateFog();
				if (num < moveList.Count - 1)
				{
					num++;
					hit = false;
					breath = false;
				}
				else
				{
					num = 0;
					hit = false;
					breath = false;
					go = false;
					moveObject.animator.SetBool("walk", false);
					StartCoroutine(gameManager.tilemapManager.MoveEnd(moveObject));
				}
			}
		}
	}
	public void BreathCheck(Vector3 newPos)
	{
		if (!breath)
		{
			if (tilemapManager.ReturnTile(newPos).GetComponentInChildren<Smoke>())
			{
				moveObject.cd.oxygen -= 1;
			}
			moveObject.cd.oxygen -= 1;

			if (moveObject.cd.oxygen < 0)
			{
				moveObject.lung -= moveObject.cd.oxygen;
				moveObject.cd.oxygen = 0;
			}
			moveObject.CheckPlayerLung();
			breath = true;
		}
	}
	public void hitCheck(Vector3 newPos)
	{
		if (!hit)
		{
			if (tilemapManager.ReturnTile(newPos).GetComponentInChildren<Fire>())
			{
				var fireDamage = tilemapManager.ReturnTile(newPos).GetComponentInChildren<Fire>().data.dmg;
				moveObject.cd.hp -= fireDamage;
				if (moveObject.handList.Count != 0 && moveObject.handList[0].GetComponent<Claimant>())
				{
					moveObject.handList[0].GetComponent<Claimant>().hp -= fireDamage;
				}
				moveObject.CheckPlayerHp();
				hit = true;
				if(moveObject.cd.hp <= 0)
				{
					num = 0;
					hit = false;
					breath = false;
					go = false;
					moveObject.animator.SetBool("walk", false);
					tilemapManager.DeadEnd();
					moveObject.SetState(PlayerState.End);
				}
			}
		}
	}
}