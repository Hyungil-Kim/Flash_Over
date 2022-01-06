using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerState
{
    Idle,
    Move,
    Attack,
    End,
}
public class Player : FSM<PlayerState>
{
    public GameManager gameManager;
    public GameObject moveHelper;
    public float damege = 10f;
    public int move = 5;
    public int hp = 20;
    public int index;
    public CharacterData cd;

    private void Awake()
	{
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        Turn.players.Add(this);
    }
	void Start()
    {
        AddState(PlayerState.Idle, new PlayerIdleState(this));                             
        AddState(PlayerState.Move, new PlayerMoveState(this));
        AddState(PlayerState.Attack, new PlayerAttackState(this));
        AddState(PlayerState.End, new PlayerEndState(this));
        SetState(PlayerState.Idle);
    }

    void Update()
    {
        FSMUpdate();
    }

}
