using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState
{
	Idle,
	Move,
	End,
}

public class Monster : FSM<MonsterState>
{
    public GameManager gameManager;


    public float hp = 20;
    public float plusHp;
    public float damage;
    public int level;
    public int area;

	public void Awake()
	{
		gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
	}
	public void Start()
	{
		area = gameManager.tilemapManager.ReturnTile(this.gameObject).area;
		if(this.tag == "Fire")
		{
		AddState(MonsterState.Idle, new FireIdleState(this));
		AddState(MonsterState.End, new FireIdleState(this));
		}
		else if(this.tag == "Smoke")
		{
		AddState(MonsterState.Idle, new SmokeIdleState(this));
		AddState(MonsterState.Move, new SmokeMoveState(this)); ///? 
		AddState(MonsterState.End, new SmokeEndState(this));
		}
		SetState(MonsterState.Idle);
		Turn.monsters.Add(this);
	}
	public void FireAct()
	{
		gameManager.tilemapManager.FireAttack(this, Color.grey);
	}
	public void FireEndAct()
	{
		gameManager.tilemapManager.EndMonsterAttack();
	}
	public void SmokeAct()
	{

	}

}
