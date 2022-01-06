using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClaimantState
{
	Idle,
	Meet,
	Resuce,
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
	public int claimantArea;
	public int hp;
	public int airGauge;//����� �̸����� �ʿ�
	public int speed =5;
	public int weight;
	public int num = -1;
	public bool moveEnd;
	private ClaimantMove claimantMove = new ClaimantMove();
	public void Awake()
	{
		gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
	}
	public void Start()
	{
		AddState(ClaimantState.Idle, new ClaimantIdleState(this));
		AddState(ClaimantState.Meet, new ClaimantMeetState(this));
		AddState(ClaimantState.Resuce, new ClaimantResuceState(this));
		AddState(ClaimantState.End, new ClaimantEndState(this));
		SetState(ClaimantState.Idle);
		Turn.claimants.Add(this);
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
}
