using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClaimantState
{
	Idle,
	Meet,
	Resuce,
	Exit,
	End,
}
public enum ClaimantInjure
{
	Idle,
	little,
	hard
}


public class Claimant : FSM<ClaimantState>
{
	public GameManager gameManager;
	public Player targetPlayer;
	public int targetPlayerIndex;


	public int id;

	public bool stun;
	public bool confuse;
	public bool eventOn;
	public bool exit;
	public int claimantArea;
	//public int data.hp;
	public int hp;
	public int airGauge;//산소통 이름변경 필요
	//public int data.move;
	//public int data.weight;
	public int num = 0;
	public bool moveEnd;
	private ClaimantMove claimantMove = new ClaimantMove();
	public ClaimantInjure claimantCurInjure = ClaimantInjure.Idle;
	public int ap = 8; // 현재폐활량

	//public int data.lung = 8; // 최대폐활량
	public int lung = 0; // 폐 hp


	public int index;

	private Vector3 prevPos;

	public ClaimantData data;
	public ClaimantSaveData GetData()
    {
		
		var csd = new ClaimantSaveData();
		csd.index = index;
		csd.id = id;
		data = MyDataTableMgr.claimantTable.GetTable(id);

		csd.stun = stun;
		csd.confuse =confuse;
		csd.eventOn = eventOn;
		csd.exit = exit;
		csd.claimantArea = claimantArea;
		csd.hp = hp;

		csd.airGauge = airGauge;
		csd.speed = data.move;
		csd.weight = data.weight;
		csd.num = num;
		csd.moveEnd = moveEnd;

		csd.ap = ap;
		csd.Maxap = data.lung;
		csd.lung = lung;
		csd.posx = gameObject.transform.position.x;
		csd.posy = gameObject.transform.position.y;
		csd.posz = gameObject.transform.position.z;

		if (targetPlayer != null)
		{
			csd.targetPlayerIndex = targetPlayer.index;
		}
		return csd;
	}
	public void SaveInit(ClaimantSaveData sd)
    {
		id = sd.id;

		targetPlayerIndex = sd.targetPlayerIndex;
		stun = sd.stun;
		confuse = sd.confuse;
		eventOn = sd.eventOn;
		exit = sd.exit ;
		claimantArea = sd.claimantArea;
		data.hp = sd.hp;
		airGauge = sd.airGauge;
		data.move = sd.speed;
		data.weight = sd.weight;
		num = sd.num;
		moveEnd = sd.moveEnd;
		
		ap = sd.ap;
		data.lung = sd.Maxap; 
		lung = sd.lung;
		gameObject.transform.position = new Vector3(sd.posx, sd.posy, sd.posz);

		targetPlayer = Turn.players.Find((x) => x.index == targetPlayerIndex);
		
	}
    private void Update()
    {
		if (transform.position != prevPos)
		{
			GameManager.instance.tilemapManager.ReturnTile(gameObject).CheckParticle();
			prevPos = transform.position;
		}
    }
    public void Awake()
	{
		gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
		Turn.claimants.Add(this);
		index = Turn.claimants.Count;
		Turn.saveClaimants.Add(index, this.gameObject);
	}
	public void Start()
	{
		data = MyDataTableMgr.claimantTable.GetTable(id);
		ap = data.lung;
		hp = data.hp;

		AddState(ClaimantState.Idle, new ClaimantIdleState(this));
		AddState(ClaimantState.Meet, new ClaimantMeetState(this));
		AddState(ClaimantState.Resuce, new ClaimantResuceState(this));
		AddState(ClaimantState.End, new ClaimantEndState(this));
		SetState(ClaimantState.Idle);
	}
	public void ClaimantAct()
	{
		switch (num)
		{
			case 0:
				claimantMove.JustStay(this);
				break;
			case 1:
				if (stun)
				{
					claimantMove.JustStay(this);
				}
				else
				{
					StartCoroutine(claimantMove.MoveToPlayer(this, targetPlayer, targetPlayerIndex));
				}
				break;
			case 2:
				if (stun)
				{
					claimantMove.JustStay(this);
				}
				else
				{
					StartCoroutine(claimantMove.MoveConfuse(this));
				}
				break;
			case 3:
				if (stun)
				{
					claimantMove.JustStay(this);
				}
				else
				{
					StartCoroutine(claimantMove.MoveToExit(this));
				}
				break;
			default:
				if (stun)
				{
					claimantMove.JustStay(this);
				}
				else
				{
					StartCoroutine(claimantMove.MoveConfuse(this));
				}
				break;
		}
		
	}
	public void CheckClaimantHp()
	{
		if (data.hp / 2 >= hp)
		{
			this.stun = true;
		}
		else
		{
			this.stun = false;
		}
		if (hp <= 0)
		{
			gameObject.SetActive(false);
			Turn.claimants.Remove(this);
		}
	}
	public void CheckClaimantLung()
	{
		if (lung >= 100)
		{
			gameObject.SetActive(false);
			Turn.claimants.Remove(this);
		}
	}
}
