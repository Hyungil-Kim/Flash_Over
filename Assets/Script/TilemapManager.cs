using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
	public Tilemap tilemap;
	private GameManager gameManager;
	private Queue<GroundTile> resetQueue = new Queue<GroundTile>();
	private FloodFillAlgorism floodFill = new FloodFillAlgorism();
	private AstarAlgoritm astarAlgoritm = new AstarAlgoritm();
	private PlayerAtackRange playerAtackRange = new PlayerAtackRange();
	public void Awake()
	{
		gameManager = GetComponent<GameManager>();
	}
	public void Start()
	{
	}
	public GroundTile ReturnTile(GameObject gameObject)
	{
		var pos = gameObject.transform.position;
		var newPos = tilemap.WorldToCell(pos);
		return tilemap.GetInstantiatedObject(newPos).GetComponent<GroundTile>();// 타일저장
	}
	public GroundTile ReturnTile(Vector3 position)
	{
		var newPos = tilemap.WorldToCell(position);
		return tilemap.GetInstantiatedObject(newPos).GetComponent<GroundTile>();// 타일저장
	}
	public Vector3 ReturnPosition(Vector3 position)
	{
		var newPos = tilemap.WorldToCell(position);
		return tilemap.GetInstantiatedObject(newPos).transform.position;// 타일저장
	}
	public void ShowMoveRange(GroundTile target,Player player,Player prePlayer,Color moveSetColor) /// 기본 이동 플로드필
	{
		if (prePlayer==null)
		{
			floodFill.FloodFill(tilemap, target.transform.position, moveSetColor, player.move);//숫자에 범위크기
		}
		else
		{
			floodFill.ResetTile(tilemap);
			floodFill.FloodFill(tilemap, target.transform.position, moveSetColor, player.move);

		}
	}
	public void MoveEnd(Player targetplayer)
	{
		floodFill.ResetTile(tilemap);
		targetplayer.SetState(PlayerState.Attack);

	}
	public void ResetFloodFill()
	{
		floodFill.ResetTile(tilemap);
	}
	public void SetAstar(GroundTile pretargetTile,GroundTile targetTile,Color astarSetColor,Color moveSetColor)
	{
		if (astarAlgoritm.finalList != null)//처음 예외처리
		{
			astarAlgoritm.ResetRootColor(moveSetColor);//색변화
		}
		astarAlgoritm.PathFinding(pretargetTile, targetTile, astarSetColor);
	}
	public void ChangeColorAttack(GameObject target,int weapontype,Color attackSetColor)
	{
		playerAtackRange.AttackReset(tilemap, weapontype);
		playerAtackRange.Attack(target,this, weapontype, 5, attackSetColor);
	}

	public void ColorPath(GroundTile targetTile,GroundTile preTargetTile,List<GroundTile> moveList,Player player,Color moveSetColor)
	{
		if (preTargetTile != targetTile)
		{
			player.move--;
			targetTile.SetTileColor(moveSetColor);
			moveList.Add(targetTile);
		}
	}

	public List<GroundTile> ReturnFinalList()
	{
		return astarAlgoritm.finalList;
	}

	public void DrawFloodFill(Vector3 targetPos, List<Vector3> moveList, int move, Color moveSetColor,Color pathColor)
	{
		floodFill.ResetTileExecptPath(tilemap,moveList, pathColor);
		floodFill.FloodFillExceptColor(tilemap, targetPos, moveSetColor,pathColor,move,moveList);
	}
	public void ResetAttackRange(int type)
	{
		playerAtackRange.AttackReset(tilemap, type);
	}

	public bool CheckPlayer(GameObject moveHelper)
	{
		var helperTile = ReturnTile(moveHelper);
		if(helperTile.isPlayer)
		{
			return true;
		}
		return false;
	}



	public void DoAttack(int num, Player targetPlayer)
	{
		switch (num)
		{
			case 0:
				break;
			case 1:
				foreach (var elemList in playerAtackRange.LineResetQueue)
				{
					foreach (var elem in elemList.fillList)
					{
						if (elem.tag == "Monster")
						{
							var targetPos = tilemap.WorldToCell(elem.transform.position);
							var targetTile = tilemap.GetInstantiatedObject(targetPos);
							var damage = targetPlayer.GetComponent<Player>().damege * (1 - (targetTile.GetComponent<GroundTile>().checkSum - 1) * 0.4);
							damage = damage > 0 ? damage : 0;
							elem.GetComponent<TestMon>().hp -= Mathf.RoundToInt((float)damage);
						}
					}
				}
				break;
			case 2:
				foreach (var elemList in playerAtackRange.TriResetQueue)
				{
					foreach (var elem in elemList.fillList)
					{
						if (elem.tag == "Monster")
						{
							var targetPos = tilemap.WorldToCell(elem.transform.position);
							var targetTile = tilemap.GetInstantiatedObject(targetPos);
							var damage = targetPlayer.GetComponent<Player>().damege * 0.4;
							damage = damage > 0 ? damage : 0;
							elem.GetComponent<TestMon>().hp -= Mathf.RoundToInt((float)damage);
						}
					}
				}
				break;
		}
		ResetAttackRange(num);
	}





}
