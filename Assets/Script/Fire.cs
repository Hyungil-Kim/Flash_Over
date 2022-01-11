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


    public float fireHp;
    public int fireExpGrowth;
    public int fireDamage;
    public int fireLevel;
    public int fireArea;

	public int fireMakeSmoke = 50;
	public GroundTile gt;
	public void Awake()
	{
		gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
		gt = GetComponentInParent<GroundTile>();
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
	public void Update()
	{
		if(fireHp <= 0)
		{
			gameObject.SetActive(false);
			Turn.fires.Remove(this);
		}
	}
}
