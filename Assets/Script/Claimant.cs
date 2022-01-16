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
	public bool stun;
	public bool confuse;
	public bool eventOn;
	public bool exit;
	public int claimantArea;
	public int hp;
	public int airGauge;//����� �̸����� �ʿ�
	public int speed;
	public int weight;
	public int num = -1;
	public bool moveEnd;
	private ClaimantMove claimantMove = new ClaimantMove();
	//
	public int oxygentank = 5;//�����ũ
	public int ap = 8; // ������Ȱ��
	public int Maxap = 8; // �ִ���Ȱ��
	public int lung = 100; // �� hp

	public int index;
	public ClaimantSaveData GetData()
    {
		var csd = new ClaimantSaveData();
		csd.index = index;

		csd.stun = stun;
		csd.confuse =confuse;
		csd.eventOn = eventOn;
		csd.exit = exit;
		csd.claimantArea = claimantArea;
		csd.hp = hp;
		csd.airGauge = airGauge;
		csd.speed = speed;
		csd.weight = weight;
		csd.num = num;
		csd.moveEnd = moveEnd;

		csd.oxygentank = oxygentank;
		csd.ap = ap;
		csd.Maxap = Maxap;
		csd.lung = lung;

		return csd;
	}
	public void SaveInit(ClaimantSaveData sd)
    {

		stun = sd.stun;
		confuse = sd.confuse;
		eventOn = sd.eventOn;
		exit = sd.exit ;
		claimantArea = sd.claimantArea;
		hp = sd.hp;
		airGauge = sd.airGauge;
		speed = sd.speed;
		weight = sd.weight;
		num = sd.num;
		moveEnd = sd.moveEnd;
		
		oxygentank = sd.oxygentank;
		ap = sd.ap;
		Maxap = sd.Maxap; 
		lung = sd.lung; 
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
				StartCoroutine(claimantMove.MoveToPlayer(this,targetPlayer));
				break;
			case 1:
				StartCoroutine(claimantMove.MoveConfuse(this));
				break;
			case 2:
				claimantMove.JustStay(this);
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
		if (hp <= 0)
		{
			gameObject.SetActive(false);
			Turn.claimants.Remove(this);
		}
	}
	public void CheckClaimantLung()
	{
		if (lung < 0)
		{
			gameObject.SetActive(false);
			Turn.claimants.Remove(this);
		}
	}
}
