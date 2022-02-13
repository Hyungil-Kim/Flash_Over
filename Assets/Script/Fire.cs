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
    public int fireLevel;

	//public int data.increaseHp;//내 체력 상승치
    //public int data.increaseExp;//주변타일(1칸범위) 체력 상승치

    //public int data.dmg;
    public int fireArea;
	//public int data.minhp;
	//public int data.maxhp;

	//public int data.makeSmoke = 50;
	public GroundTile gt;

	public GameObject fire;


	public FireData data ;

	public void Awake()
	{
		gameManager = GameManager.instance;
		gt = GetComponentInParent<GroundTile>();
		//Turn.fires.Add(this);
	}
	public void Start()
	{
		data = MyDataTableMgr.fireTable.GetTable(fireLevel);
		fireArea = gameManager.tilemapManager.ReturnTile(this.gameObject).tileArea;
		AddState(FireState.Idle, new FireIdleState(this));
		AddState(FireState.End, new FireEndState(this));
		SetState(FireState.Idle);
		Turn.fires.Add(this);
		var particle = GetComponentInChildren<ParticleSystem>();
		if(particle !=null)
        {
			particle.Stop();
        }
	}
	public void OnEnable()
	{
		if (gameManager.tilemapManager.ReturnTile(this.gameObject).safeArea)
		{
			Turn.SafeAreaFalse(fireArea);
		}
	}
	public void FireAct()
	{
		gameManager.tilemapManager.FireAttack(this, Color.grey);
	}
	public void FireEndAct()
	{
		gameManager.tilemapManager.EndMonsterAttack();
	}
	public void CheckFireHp()
	{

		if(fireHp <= 0)
		{
			GameManager.instance.tilemapManager.ReturnTile(this.gameObject).tileExp = 0;
			gameObject.SetActive(false);
			gt.tileIsFire = false;
			Turn.CheckSafeArea(fireArea);
			Turn.fires.Remove(this);
		}
		
	}
	public void OnFire()
    {
		if (fire != null)
		{
			fire.SetActive(true);
			if(gt.tileIsWeat)
			{
				gt.tileIsWeat = false;
			}
		}
	}
	public void OffFire()
    {
		if (fire != null)
		{
			
			fire.SetActive(false);

		}
    }
}
