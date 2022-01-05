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
	//Ÿ�ϸʰ���
	public GroundTile ReturnTile(GameObject gameObject)
	{
		var pos = gameObject.transform.position;
		var newPos = tilemap.WorldToCell(pos);
		return tilemap.GetInstantiatedObject(newPos).GetComponent<GroundTile>();// Ÿ������
	}
	public GroundTile ReturnTile(Vector3 position)
	{
		var newPos = tilemap.WorldToCell(position);
		return tilemap.GetInstantiatedObject(newPos).GetComponent<GroundTile>();// Ÿ������
	}
	public Vector3 ReturnPosition(Vector3 position)
	{
		var newPos = tilemap.WorldToCell(position);
		return tilemap.GetInstantiatedObject(newPos).transform.position;// Ÿ������
	}
	//floodfill ����
	public void ShowMoveRange(GroundTile target, Player player, Player prePlayer, Color moveSetColor) /// �⺻ �̵� �÷ε���
	{
		if (prePlayer == null)
		{
			floodFill.FloodFill(tilemap, target.transform.position, moveSetColor, player.move);//���ڿ� ����ũ��
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
	public void DrawFloodFill(Vector3 targetPos, List<Vector3> moveList, int move, Color moveSetColor, Color pathColor)
	{
		floodFill.ResetTileExecptPath(tilemap, moveList, pathColor);
		floodFill.FloodFillExceptColor(tilemap, targetPos, moveSetColor, pathColor, move, moveList);
	}
	// astar����
	public List<GroundTile> SetAstar(GroundTile pretargetTile, GroundTile targetTile)
	{
		return astarAlgoritm.PathFinding(pretargetTile, targetTile);
	}
	public List<GroundTile> ReturnFinalList()
	{
		return astarAlgoritm.finalList;
	}

	//attack����
	public void ChangeColorAttack(GameObject target, int weapontype, Color attackSetColor)//���� ���� ��Ÿ��� �ٲ����
	{
		ResetAttackRange(weapontype);
		attackRange.Attack(target, this, weapontype, 5, attackSetColor);
	}
	public void FireAttack(Fire target, Color attackSetColor)
	{
		var list = testAttackRange.CrossFloodFill(this, target.gameObject, attackSetColor, target.fireLevel);
		FireDamage(target, list);
		for (int i = 0; i < list.Count; i++) //Ȯ��? �� �־���ϴ���
		{
			attackList.Add(list[i]);
		}
		testAttackRange.CrossResetTile(tilemap, list);
		list.Clear();
	}
	public void FireDamage(Fire target, List<GroundTile> list)
	{
		foreach (var elem in list)
		{
			var damage = target.fireDamage / (Mathf.Pow(2f, elem.checkSum));
			damage = damage > 0 ? damage : 0;
			var iDamage = Mathf.CeilToInt(damage);
			foreach (var defender in elem.fillList)
			{
				if (defender.tag == "Player")
				{
					defender.GetComponent<Player>().hp -= iDamage;
				}
				//������Ʈ �߰�
			}
			elem.ChangeTileState(elem, iDamage);
			elem.CheckParticleOn(elem);
		}
	}
	public void EndMonsterAttack()
	{
		for (int i = 0; i < attackList.Count; i++)
		{
			attackList[i].Reset();
		}
		attackList.Clear();
	}
	public void ResetAttackRange(int type)
	{
		attackRange.AttackReset(tilemap, type);
	}
	public void DoAttack(Player attacker, int num)
	{
		switch (num)
		{
			case 0:
				break;
			case 1:
				foreach (var elem in attackRange.LineResetQueue)
				{
					if (elem.tileIsFire)
					{
						var targetPos = tilemap.WorldToCell(elem.transform.position);
						var targetTile = tilemap.GetInstantiatedObject(targetPos);
						var damage = attacker.damege * (1 - (targetTile.GetComponent<GroundTile>().checkSum - 1) * 0.4);
						damage = damage > 0 ? damage : 0;
						elem.GetComponentInChildren<Fire>().fireHp -= Mathf.RoundToInt((float)damage);
					}
				}
				break;
			case 2:
				foreach (var elem in attackRange.TriResetQueue)
				{

					if (elem.tileIsFire)
					{
						var targetPos = tilemap.WorldToCell(elem.transform.position);
						var targetTile = tilemap.GetInstantiatedObject(targetPos);
						var damage = attacker.damege * 0.4;
						damage = damage > 0 ? damage : 0;
						elem.GetComponentInChildren<Fire>().fireHp -= Mathf.RoundToInt((float)damage);
					}

				}
				break;
		}
		ResetAttackRange(num);
	}
	//smoke
	public int CheckDivideSmoke(Smoke smoke)//�����־ ��ã���������� ����κ� �����ʿ�
	{
		var ground = smoke.GetComponentInParent<GroundTile>();
		var divideTile = 1;
		var divideSmoke = 0;
		for (int i = 0; i < ground.nextTileList.Count; i++)
		{
			if (ground.nextTileList[i].tileSmokeValue < ground.tileSmokeValue)
			{
				divideTile++;
			}
		}
		divideSmoke = Mathf.FloorToInt(ground.tileSmokeValue / divideTile);
		return divideSmoke;
	}
	public void SaveSmokeValue(Smoke smoke)
	{
		var ground = smoke.GetComponentInParent<GroundTile>();
		var divideSmoke = CheckDivideSmoke(smoke);

		for (int i = 0; i < ground.nextTileList.Count; i++)
		{
			if (ground.nextTileList[i].tileSmokeValue < ground.tileSmokeValue)
			{
				ground.nextTileList[i].tileSaveSmokeValue += divideSmoke;
			}
		}
		ground.tileSaveSmokeValue += divideSmoke;

	}
	public void SpreadSmoke(Smoke smoke)
	{
		var ground = smoke.GetComponentInParent<GroundTile>();

		foreach (var elem in ground.nextTileList)
		{
			elem.tileSmokeValue += elem.tileSaveSmokeValue;
		}
		ground.tileSmokeValue += ground.tileSaveSmokeValue;
	}
	public void ResetSmokeValue(Smoke smoke)
	{
		var ground = smoke.GetComponentInParent<GroundTile>();
		//�ʱ�ȭ
		foreach (var elem in ground.nextTileList)
		{
			elem.tileSaveSmokeValue = 0;
		}
		ground.tileSaveSmokeValue = 0;
	}
	//ex
	public bool CheckPlayer(GameObject moveHelper)
	{
		var helperTile = ReturnTile(moveHelper);
		if (helperTile.isPlayer)
		{
			return true;
		}
		return false;
	}

}
