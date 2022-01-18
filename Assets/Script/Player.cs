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

    public PlayerState playerState = PlayerState.Idle;
    public void SaveInit(PlayerSaveData sd)
    {
        gameObject.transform.position = new Vector3(sd.posx,sd.posy,sd.posz);
        playerState = StringToEnum.SToE<PlayerState>(sd.currentState);
        cd = sd.cd;
        //handList = sd.handList;
        foreach (var index in sd.handListIndex)
        {
            handList.Add(Turn.saveClaimants[index]);
            //handList.Add(Turn.claimants[index].gameObject);
        }

        eventNum = sd.eventNum;
    }
    public PlayerSaveData GetData()
    {
        var sd = new PlayerSaveData();
        //sd.pos = gameObject.transform.position;
        sd.posx = gameObject.transform.position.x;
        sd.posy = gameObject.transform.position.y;
        sd.posz = gameObject.transform.position.z;
        Debug.Log(curStateName.ToString());
        sd.currentState = curStateName.ToString();
        sd.cd = cd;
        //sd.handList = handList;
        foreach (var item in handList)
        {
            sd.handListIndex.Add(item.GetComponent<Claimant>().index);
        }
        sd.eventNum = eventNum;
        return sd;
    }
	private void Awake()
	{
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        Turn.players.Add(this);
        AddState(PlayerState.Idle, new PlayerIdleState(this));
        AddState(PlayerState.Move, new PlayerMoveState(this));
        AddState(PlayerState.Action, new PlayerAttackState(this));
        AddState(PlayerState.End, new PlayerEndState(this));
        SetState(playerState);

    }
    void Start()
    {
        //AddState(PlayerState.Idle, new PlayerIdleState(this));                             
        //AddState(PlayerState.Move, new PlayerMoveState(this));
        //AddState(PlayerState.Action, new PlayerAttackState(this));
        //AddState(PlayerState.End, new PlayerEndState(this));
        //SetState(PlayerState.Idle);
        SetState(playerState);
        moveHelper.transform.localPosition = Vector3.zero;
    }

    void Update()
    {
        //if (cd.hp <= 0 || lung <= 0)
        //{
        //	if (handFull)
        //	{
        //		handList[0].SetActive(false);
        //		handFull = false;
        //		if (handList[0].tag == "Claimant")
        //		{
        //			Turn.claimants.Remove(handList[0].GetComponent<Claimant>());
        //			handList.RemoveAt(0);
        //		}
        //	}
        //	gameObject.SetActive(false);
        //	Turn.players.Remove(this);
        //}

        //test
        //foreach (var buff in cd.buff)
        //{
        //    buff.Check();
        //}
	}
    private void OnDestroy()
    {
        Turn.players.Remove(this);
    }
}
