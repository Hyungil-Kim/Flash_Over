using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
	public Tilemap tilemap;
	private GameManager gameManager;
	private FloodFillAlgorism floodFill = new FloodFillAlgorism();
	private AstarAlgoritm astarAlgoritm = new AstarAlgoritm();
	private AttackRange attackRange = new AttackRange();
	private MonsterAttackRange testAttackRange = new MonsterAttackRange();
	private List<GroundTile> attackList = new List<GroundTile>();
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
	public void ShowMoveRange(GroundTile target, Player player, Player prePlayer, Color moveSetColor) /// 기본 이동 플로드필
	{
		if (prePlayer == null)
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
		gameManager.playerMove.moveList.Clear();
		targetplayer.SetState(PlayerState.Attack);

	}
	public void ResetFloodFill()
	{
		floodFill.ResetTile(tilemap);
	}
	public void SetAstar(GroundTile pretargetTile, GroundTile targetTile, Color astarSetColor, Color moveSetColor)
	{
		if (astarAlgoritm.finalList != null)//처음 예외처리
		{
			astarAlgoritm.ResetRootColor(moveSetColor);//색변화
		}
		astarAlgoritm.PathFinding(pretargetTile, targetTile, astarSetColor);
	}
	public void ChangeColorAttack(GameObject target, int weapontype, Color attackSetColor)//숫자 무기 사거리로 바꿔야함
	{
		attackRange.AttackReset(tilemap, weapontype);
		attackRange.Attack(target, this, weapontype, 5, attackSetColor);
	}
	public void FireAttack(Monster target, Color attackSetColor)
	{
		var list = testAttackRange.CrossFloodFill(this,target.gameObject, attackSetColor,target.level);
		FireDamage(target, list);
		for (int i = 0; i < list.Count; i++)
		{
			attackList.Add(list[i]);
		}
		testAttackRange.CrossResetTile(tilemap, list);
		list.Clear();
	}
	public void FireDamage(Monster target,List<GroundTile> list)
	{
		foreach(var elem in list)
		{
			var damage = target.damage / (Mathf.Pow(2f,elem.checkSum));
			damage = damage > 0 ? damage : 0;
			var iDamage = Mathf.CeilToInt(damage);
			elem.exp += iDamage;
			foreach(var defender in elem.fillList)
			{
				if(defender.tag == "Player")
				{
					Debug.Log(iDamage);
					defender.GetComponent<Player>().hp -= iDamage;
				}
			}
		}
	}
	public void EndMonsterAttack()
	{
		for(int i =0; i < attackList.Count;i++)
		{
			attackList[i].Reset();
		}
		attackList.Clear();
	}

	public void ColorPath(GroundTile targetTile, GroundTile preTargetTile, List<GroundTile> moveList, Player player, Color moveSetColor)
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

	public void DrawFloodFill(Vector3 targetPos, List<Vector3> moveList, int move, Color moveSetColor, Color pathColor)
	{
		floodFill.ResetTileExecptPath(tilemap, moveList, pathColor);
		floodFill.FloodFillExceptColor(tilemap, targetPos, moveSetColor, pathColor, move, moveList);
	}
	public void ResetAttackRange(int type)
	{
		attackRange.AttackReset(tilemap, type);
	}

	public bool CheckPlayer(GameObject moveHelper)
	{
		var helperTile = ReturnTile(moveHelper);
		if (helperTile.isPlayer)
		{
			return true;
		}
		return false;
	}



	public void DoAttack(Player attacker, int num)
	{
		switch (num)
		{
			case 0:
				break;
			case 1:
				foreach (var elemList in attackRange.LineResetQueue)
				{
					foreach (var elem in elemList.fillList)
					{
						if (elem.tag == "Monster")
						{
							var targetPos = tilemap.WorldToCell(elem.transform.position);
							var targetTile = tilemap.GetInstantiatedObject(targetPos);
							var damage = attacker.damege * (1 - (targetTile.GetComponent<GroundTile>().checkSum - 1) * 0.4);
							damage = damage > 0 ? damage : 0;
							elem.GetComponent<Monster>().hp -= Mathf.RoundToInt((float)damage);
						}
					}
				}
				break;
			case 2:
				foreach (var elemList in attackRange.TriResetQueue)
				{
					foreach (var elem in elemList.fillList)
					{
						if (elem.tag == "Monster")
						{
							var targetPos = tilemap.WorldToCell(elem.transform.position);
							var targetTile = tilemap.GetInstantiatedObject(targetPos);
							var damage = attacker.damege * 0.4;
							damage = damage > 0 ? damage : 0;
							elem.GetComponent<Monster>().hp -= Mathf.RoundToInt((float)damage);
						}
					}
				}
				break;
		}
		ResetAttackRange(num);
	}

	public void DoAttack(Monster attacker)
	{
		foreach (var elemList in attackRange.crossQueue)
		{
			foreach (var elem in elemList.fillList)
			{
				if (elem.tag == "Monster")
				{
					var targetPos = tilemap.WorldToCell(elem.transform.position);
					var targetTile = tilemap.GetInstantiatedObject(targetPos);
					var damage = attacker.GetComponent<Player>().damege * (1 - (targetTile.GetComponent<GroundTile>().checkSum - 1) * 0.4);
					damage = damage > 0 ? damage : 0;
					elem.GetComponent<Monster>().hp -= Mathf.RoundToInt((float)damage);
				}
			}
		}
		ResetAttackRange(0);
	}



}
