using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireState
{
	Idle,
	Move,
	End,
}

public class Fire : FSM<FireState>
{
    public GameManager gameManager;


    public float fireHp = 20;
    public int fireExpGrowth;
    public int fireDamage;
    public int fireLevel;
    public int fireArea;

	public int fireMakeSmoke = 50;

	public void Awake()
	{
		gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
		//Turn.fires.Add(this);
	}
	public void Start()
	{
		fireArea = gameManager.tilemapManager.ReturnTile(this.gameObject).tileArea;
		AddState(FireState.Idle, new FireIdleState(this));
		AddState(FireState.End, new FireEndState(this));
		SetState(FireState.Idle);
		Turn.fires.Add(this);
		GetComponentInChildren<ParticleSystem>().Stop();
	}
	public void FireAct()
	{
		gameManager.tilemapManager.FireAttack(this, Color.grey);
	}
	public void FireEndAct()
	{
		gameManager.tilemapManager.EndMonsterAttack();
	}
}
