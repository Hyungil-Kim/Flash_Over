using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerState
{
    Idle,
    Move,
    Action,
    End,
}
public class Player : FSM<PlayerState>
{
    public GameManager gameManager;
    public GameObject moveHelper;

    public int index;
    public CharacterData cd;

    public bool handFull;
    public List<GameObject> handList = new List<GameObject>();
    public int eventNum = 0;
    /// 
    public int oxygentank = 5;//ªÍº“≈ ≈©
    public int ap = 8; // «ˆ¿Á∆Û»∞∑Æ
    public int Maxap = 8; // √÷¥Î∆Û»∞∑Æ
    public int lung = 100; // ∆Û hp
	private void Awake()
	{
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        Turn.players.Add(this);
    }
	void Start()
    { 
        AddState(PlayerState.Idle, new PlayerIdleState(this));                             
        AddState(PlayerState.Move, new PlayerMoveState(this));
        AddState(PlayerState.Action, new PlayerAttackState(this));
        AddState(PlayerState.End, new PlayerEndState(this));
        SetState(PlayerState.Idle);
        moveHelper.transform.localPosition = Vector3.zero;
    }

    void Update()
    {
		if (cd.hp <= 0 || lung <= 0)
		{
			if (handFull)
			{
				handList[0].SetActive(false);
				handFull = false;
				if (handList[0].tag == "Claimant")
				{
					Turn.claimants.Remove(handList[0].GetComponent<Claimant>());
					handList.RemoveAt(0);
				}
			}
			gameObject.SetActive(false);
			Turn.players.Remove(this);
		}
	}
    private void OnDestroy()
    {
        Turn.players.Remove(this);
    }
}
