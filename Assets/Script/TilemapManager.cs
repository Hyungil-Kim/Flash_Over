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
	private Claimant saveClaimant;
	public Claimant SaveClaimant { get { return saveClaimant; } }
	private Claimant rescueClaimant;
	public Claimant RescueClaimant { get { return rescueClaimant; } }
	private bool action;
	public void Awake()
	{
		gameManager = GetComponent<GameManager>();
	}
	public void Start()
	{
	}
	//타일맵관련
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
	//floodfill 관련
	public void ShowMoveRange(GroundTile target, Player player, Player prePlayer, Color moveSetColor) /// 기본 이동 플로드필
	{
		if (prePlayer == null)
		{
			floodFill.FloodFill(tilemap, target.transform.position, moveSetColor, player.cd.totalStats.move);//숫자에 범위크기
		}
		else
		{
			floodFill.ResetTile(tilemap);
			floodFill.FloodFill(tilemap, target.transform.position, moveSetColor, player.cd.totalStats.move);

		}
	}
	public void ShowFloodFillRange(GroundTile target, Color moveSetColor,int range)
	{
		floodFill.FloodFill(tilemap, target.transform.position, moveSetColor, range);
	}
	public List<GroundTile> ReturnFloodFillRange(GroundTile target,Color color, int range)
	{
		return floodFill.ReturnFloodFill(tilemap, target.transform.position, color, range);
	}
	public IEnumerator MoveEnd(Player targetPlayer)
	{
		floodFill.ResetTile(tilemap);
		gameManager.playerMove.moveList.Clear();
		var visionList = targetPlayer.GetComponent<VisionRange>().CheackVision();
		foreach(var elem in visionList)
		{
			if (elem.isClaimant)
			{
				foreach(var listElem in elem.fillList)
				{
					if (listElem.tag == "Claimant")
					{
						saveClaimant = listElem.GetComponent<Claimant>();
						if (saveClaimant.num == -1 && !saveClaimant.stun)
						{
							yield return StartCoroutine(meetClaimant());
						}
					}
				}
			}
		}
			CheckClaimant(targetPlayer);
			targetPlayer.SetState(PlayerState.Action);
	}
	public IEnumerator meetClaimant()
	{
		saveClaimant.SetState(ClaimantState.Meet);
		gameManager.uIManager.meetEventManager.gameObject.SetActive(true);

		while (saveClaimant.num == -1)
			yield return 0;

		gameManager.uIManager.meetEventManager.gameObject.SetActive(false);
		yield return new WaitForSeconds(0.2f);

	}
	public void ResetFloodFill()
	{
		floodFill.ResetTile(tilemap);
	}
	public void ResetFloodFill(List<GroundTile> list)
	{
		floodFill.ResetTile(list);
	}
	public void DrawFloodFill(Vector3 targetPos, List<Vector3> moveList, int move, Color moveSetColor, Color pathColor)
	{
		floodFill.ResetTileExecptPath(tilemap, moveList, pathColor);
		floodFill.FloodFillExceptColor(tilemap, targetPos, moveSetColor, pathColor, move, moveList);
	}
	// astar관련
	public List<GroundTile> SetAstar(GroundTile pretargetTile, GroundTile targetTile)
	{
		return astarAlgoritm.PathFinding(pretargetTile, targetTile);
	}
	public List<GroundTile> ReturnFinalList()
	{
		return astarAlgoritm.finalList;
	}

	//attack관련
	public void ChangeColorAttack(GameObject target, int weapontype, Color attackSetColor)//숫자 무기 사거리로 바꿔야함
	{
		ResetAttackRange(weapontype);
		attackRange.Attack(target, this, weapontype, 5, attackSetColor);
	}
	public List<GroundTile> AttackFloodFill(GameObject target,Color attackSetColor,int range)
	{
		var list = testAttackRange.CrossFloodFill(this,target,attackSetColor, range);
		return list;
	}
	public void FireAttack(Fire target, Color attackSetColor)
	{
		var list = testAttackRange.CrossFloodFill(this, target.gameObject, attackSetColor, target.fireLevel);
		FireDamage(target, list);
		for (int i = 0; i < list.Count; i++) //확인? 왜 있어야하는지
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
					defender.GetComponent<Player>().cd.hp -= iDamage;
					defender.GetComponent<Player>().CheckPlayerHp();
				}
				if(defender.tag == "Claimant")
				{
					defender.GetComponent<Claimant>().hp -= iDamage;
					defender.GetComponent<Claimant>().CheckClaimantHp();
				}
				if(defender.tag == "Obstacle")
				{
					defender.GetComponent<Obstacle>().hp -= iDamage;
					defender.GetComponent<Obstacle>().CheckObstacleHp();					
				}
			}
			elem.ChangeTileState(elem, iDamage);
			elem.CheckParticleOn(elem);
		}
	}
	public void ExplotionDamage(Obstacle target, List<GroundTile> list)
	{
		foreach (var elem in list)
		{
			var damage = target.exploseDamage / (Mathf.Pow(2f, elem.checkSum));
			damage = damage > 0 ? damage : 0;
			var iDamage = Mathf.CeilToInt(damage);
			foreach (var defender in elem.fillList)
			{
				if (defender.tag == "Player")
				{
					defender.GetComponent<Player>().cd.hp -= iDamage;
				}
				if (defender.tag == "Claimant")
				{
					defender.GetComponent<Claimant>().hp -= iDamage;
				}
			}
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
						var damage = attacker.cd.totalStats.dmg * (1 - (targetTile.GetComponent<GroundTile>().checkSum - 1) * 0.4);
						damage = damage > 0 ? damage : 0;
						elem.GetComponentInChildren<Fire>().fireHp -= Mathf.RoundToInt((float)damage);
						elem.GetComponentInChildren<Fire>().CheckFireHp();
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
						var damage = attacker.cd.totalStats.dmg * 0.4;
						damage = damage > 0 ? damage : 0;
						elem.GetComponentInChildren<Fire>().fireHp -= Mathf.RoundToInt((float)damage);
						elem.GetComponentInChildren<Fire>().CheckFireHp();

					}

				}
				break;
		}
		ResetAttackRange(num);
		attacker.ap -= 5;
		if(attacker.ap <0)
		{
			attacker.lung -= attacker.ap;
			attacker.ap = 0;
		}
	}
	//smoke
	public int CheckDivideSmoke(Smoke smoke)//꺼져있어서 못찾을수도있음 실행부분 예외필요
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
			if (elem.tag != "Wall")
				elem.tileSmokeValue += elem.tileSaveSmokeValue;
		}
		ground.tileSmokeValue += ground.tileSaveSmokeValue;
	}
	public void ResetSmokeValue(Smoke smoke)
	{
		var ground = smoke.GetComponentInParent<GroundTile>();
		//초기화
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

	//claimant

	public void CheckClaimant(Player targetPlayer)
	{
		if (targetPlayer.handFull)
		{
			gameManager.uIManager.battleUiManager.putDownButton.gameObject.SetActive(true);
			return;
		}
		var targetTile = ReturnTile(targetPlayer.gameObject);
		foreach (var elem in targetTile.nextTileList)
		{
			foreach (var listElem in elem.fillList)
			{
				if (listElem.tag == "Claimant")
				{
					rescueClaimant = listElem.GetComponent<Claimant>();
					gameManager.uIManager.battleUiManager.rescueButton.gameObject.SetActive(true);
					break;
				}
			}
		}
	}


	public IEnumerator WithPlayer(Claimant claimant, Player targetPlayer)
	{
		var preTile = GameManager.instance.tilemapManager.ReturnTile(claimant.gameObject);
		var targetTile = GameManager.instance.tilemapManager.ReturnTile(targetPlayer.gameObject);
		var moveSpeed = 5;
		var newPos = new Vector3(targetTile.transform.position.x, claimant.transform.position.y, targetTile.transform.position.z);
		if (claimant.transform.position != newPos)
		{
			var dis = Vector3.Distance(claimant.transform.position, newPos);
			if (dis > 0)
			{
				claimant.transform.position = Vector3.MoveTowards(claimant.transform.position, newPos, moveSpeed * Time.deltaTime);
				claimant.transform.LookAt(newPos);
			}
			yield return 0;
		}
	}
}
