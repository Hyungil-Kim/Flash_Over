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

	//public int data.increaseHp;//�� ü�� ���ġ
    //public int data.increaseExp;//�ֺ�Ÿ��(1ĭ����) ü�� ���ġ

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
		gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
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
			gameObject.SetActive(false);
			gt.tileIsFire = false;
			Turn.fires.Remove(this);
		}
	}
	public void OnFire()
    {
		if (fire != null)
		{
			fire.SetActive(true);
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
