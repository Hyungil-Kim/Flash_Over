using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClaimantState
{
	Idle,
	Move,
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
	public int airGauge;//산소통 이름변경 필요
	public int speed =5;
	public int weight;
	private ClaimantMove claimantMove = new ClaimantMove();
	public void Awake()
	{
		gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
	}
	public void Start()
	{
		AddState(ClaimantState.Idle, new ClaimantIdleState(this));
		AddState(ClaimantState.Move, new ClaimantMoveState(this));
		AddState(ClaimantState.End, new ClaimantEndState(this));
		SetState(ClaimantState.Idle);
		Turn.claimants.Add(this);
	}
	public void ClimantAct(int num)
	{
		switch (num)
		{
			case 0:
				StartCoroutine(claimantMove.MoveToPlayer(this, targetPlayer));
				break;
			case 1:
				StartCoroutine(claimantMove.MoveConfuse(this));
				break;
			case 2:
				claimantMove.JustStay(this);
				break;
		}

	}
}
