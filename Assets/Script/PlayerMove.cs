using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	private TilemapManager tilemapManager;
	public List<Vector3> moveList = new List<Vector3>();
	private GameManager gameManager;
	private Player moveObject;
	public float speed = 3f;
	public bool go = false;
	int num = 0;
	public int move = 0;
	private bool hit = false;
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
		foreach (var elem in moveList)      // 있어야 하나
		{
			tilemapManager.ReturnTile(elem).Reset();
		}
		moveList.Clear();
	}
	public void MovePlayer()
	{
		moveObject = gameManager.targetPlayer;
		if (!tilemapManager.CheckPlayer(moveObject.moveHelper) || tilemapManager.ReturnTile(moveObject.gameObject)== tilemapManager.ReturnTile(moveObject.moveHelper))
		{
			moveObject.moveHelper.gameObject.SetActive(false);
			go = true;
		}
	}
	public IEnumerator Move(Color setPathColor,Color setMoveColor,Player player,int movePoint)
	{
		if (!go)
		{
			var moveHelper = player.moveHelper;
			var helperTile = tilemapManager.ReturnTile(moveHelper);
			Debug.Log(helperTile.transform.position);
			while (gameManager.press)
			{
				var mousePos = gameManager.mousePos;
				Ray ray = Camera.main.ScreenPointToRay(mousePos);
				int layerMask = (1 << LayerMask.NameToLayer("GroundPanel") | 1 << LayerMask.NameToLayer("Fade"));
				layerMask = ~layerMask;
				if (Physics.Raycast(ray, out RaycastHit raycastHit, float.PositiveInfinity, layerMask))
				{
					var targetTile = tilemapManager.ReturnTile(raycastHit.transform.position);
					var tilePos = targetTile.transform.position;
					if (targetTile.nextTileList.Contains(helperTile) || targetTile)
					{
						gameManager.drag = false;
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
				ReturnMovePoint(movePoint);
				yield return 0;
			}
		}
	}
	public int ReturnMovePoint(int movePoint)
	{
		gameManager.move = movePoint;
		return move;
	}

	public void Update()
	{
		if (go)
		{
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
					if (!hit)
					{
						if (tilemapManager.ReturnTile(newPos).GetComponentInChildren<Fire>())
						{
							var fireDamage = tilemapManager.ReturnTile(newPos).GetComponentInChildren<Fire>().fireDamage;
							moveObject.hp -= fireDamage;
							if(moveObject.handList.Count != 0 && moveObject.handList[0].GetComponent<Claimant>())
							{
							moveObject.handList[0].GetComponent<Claimant>().hp -= fireDamage;
							}
							hit = true;
						}
					}
				}
			}
			else
			{
				AllTileMesh.instance.UpdateFog();
				if (num < moveList.Count - 1)
				{
					num++;
					hit = false;
				}
				else
				{
					num = 0;
					go = false;
					StartCoroutine(gameManager.tilemapManager.MoveEnd(moveObject));
				}
			}
		}
		
	}

		//public void CopyList()
		//{
		//	preRoot = gameManager.tilemapManager.ReturnFinalList();
		//	for (int i = 0; i < preRoot.Count; i++)
		//	{
		//		var nextCenterPos = gameManager.tilemapManager.ReturnPosition(preRoot[i].transform.position);
		//		var newCenterPos = new Vector3(nextCenterPos.x, moveObject.transform.position.y, nextCenterPos.z);
		//		moveList.Add(newCenterPos);//다음 타일의 중앙좌표 y는 플레이어
		//	}
		//}
		//public void Move()
		//{
		//	moveObject = gameManager.targetPlayer.gameObject;
		//	moveList = new List<Vector3>();
		//	CopyList();
		//	if (gameManager.moveHelper != null)//임시코드
		//	{
		//		gameManager.moveHelper.SetActive(false);
		//	}
		//	go = true;
		//}
		//public void Update()
		//{
		//	if (go)
		//	{
		//		if (moveObject.transform.position != moveList[num])
		//		{
		//			var dis = Vector3.Distance(moveObject.transform.position, moveList[num]);
		//			if (dis > 0)
		//			{
		//				moveObject.transform.position = Vector3.MoveTowards(moveObject.transform.position, moveList[num], speed * Time.deltaTime);
		//				moveObject.transform.LookAt(moveList[num]);
		//			}
		//		}
		//		else
		//		{
		//			if (num < moveList.Count - 1)
		//			{
		//				num++;
		//			}
		//			else
		//			{
		//				num = 0;
		//				go = false;
		//				gameManager.tilemapManager.MoveEnd();
		//			}
		//		}
		//	}
		//}

	
}