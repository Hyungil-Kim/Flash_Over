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
    public int level;
    public int damage;
    public int smokeArea;
    public void Awake()
    {
        //GetComponentInChildren<ParticleSystem>().Stop();
    }
    void Start()
    {
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
        
    }
}
