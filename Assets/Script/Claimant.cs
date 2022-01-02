using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClaimantState
{
	Idle,
	Move,
	End,

}

public class Claimant : FSM<ClaimantState>
{
	public GameManager gameManager;
	public void Awake()
	{
		gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
	}
	public void Start()
	{
		AddState(ClaimantState.Idle, new ClaimantIdleState(this));
		AddState(ClaimantState.Idle, new ClaimantMoveState(this));
		AddState(ClaimantState.End, new ClaimantEndState(this));
		SetState(ClaimantState.Idle);
	}
}
