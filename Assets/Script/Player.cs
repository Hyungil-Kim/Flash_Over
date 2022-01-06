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
    public FogProjector fogProjector;
    public MyMeshCreate meshCreate;
    public GameObject moveHelper;
    public float damege = 10f;
    public int move = 5;
    public int hp = 20;
    public bool handFull;
    public List<GameObject> handList = new List<GameObject>();
    public int eventNum = 0;
    public CharacterData cd;
    
	private void Awake()
	{
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
	}
	void Start()
    {
       // cd = GameData.userData.fireManList[0];
        AddState(PlayerState.Idle, new PlayerIdleState(this));                             
        AddState(PlayerState.Move, new PlayerMoveState(this));
        AddState(PlayerState.Action, new PlayerAttackState(this));
        AddState(PlayerState.End, new PlayerEndState(this));
        SetState(PlayerState.Idle);
        Turn.players.Add(this);
        
    }

    void Update()
    {
        FSMUpdate();
    }
}
