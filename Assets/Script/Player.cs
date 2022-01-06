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
    
	private void Awake()
	{
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        Turn.players.Add(this);
    }
	void Start()
    {
       // cd = GameData.userData.fireManList[0];
        AddState(PlayerState.Idle, new PlayerIdleState(this));                             
        AddState(PlayerState.Move, new PlayerMoveState(this));
        AddState(PlayerState.Action, new PlayerAttackState(this));
        AddState(PlayerState.End, new PlayerEndState(this));
        SetState(PlayerState.Idle);
    }

    void Update()
    {
        //FSMUpdate();
        if(cd.hp <= 0)
		{
            if(handList != null)
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

}
