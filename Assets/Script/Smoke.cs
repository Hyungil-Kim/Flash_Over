using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SmokeState
{
    Idle,
    Move,
    End,
}



public class Smoke : FSM<SmokeState>
{
    private GameManager gameManager;
    private GroundTile gt;
    public int level;
    public int minValue;
    public int maxValue;
    public int decreaseVision;
    public int smokeArea;
    public SmokeData data;
    
    public void Awake()
    {
        //GetComponentInChildren<ParticleSystem>().Stop();
        
    }
    void Start()
    {
        data = MyDataTableMgr.smokeTable.GetTable(level);
        NullGameManager();
            smokeArea = gameManager.tilemapManager.ReturnTile(this.gameObject).tileArea;
            AddState(SmokeState.Idle, new SmokeIdleState(this));
            AddState(SmokeState.Move, new SmokeMoveState(this)); ///? 
            AddState(SmokeState.End, new SmokeEndState(this));
            SetState(SmokeState.Idle);
        if(!GetComponentInParent<GroundTile>().CheakVision)
        {
            GetComponentInChildren<ParticleSystem>().Stop();
        }
        gt = GetComponentInParent<GroundTile>();
        //GetComponentInChildren<ParticleSystem>().Stop();
    }
    public void SaveSmoke()
    {
        NullGameManager();
        gameManager.tilemapManager.SaveSmokeValue(this);
    }
    public void SpreadSmoke()
	{
        NullGameManager();
        gameManager.tilemapManager.SpreadSmoke(this);
    }
    public void ResetSmokeValue()
    {
        NullGameManager();
        gameManager.tilemapManager.ResetSmokeValue(this);
    } 
    public void NullGameManager()
	{
        if (gameManager == null)
        {
            gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        }
    }
    void Update()
    {
        if(data.min > gt.tileSmokeValue)
        {
            level--;
        }
        else if(data.max < gt.tileSmokeValue)
        {
            level++;
        }
        Mathf.Clamp(level, 0, 3);
        data = MyDataTableMgr.smokeTable.GetTable(level);
    }
}
