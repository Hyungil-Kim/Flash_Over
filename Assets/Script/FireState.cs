using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireIdleState : State
{
	Fire fsm;
	public FireIdleState(Fire _fsm)
	{
		fsm = _fsm;
	}
	public override void Enter()
	{
		if (fsm.fireHp >= fsm.fireMaxExp)
		{
			fsm.fireLevel++;
		}
		else if (fsm.fireHp < fsm.fireMinExp)
		{
			fsm.fireLevel--;
		}
	}
	public override void Update()
	{
		
	}
	public override void Exit()
	{
		
	}
}

public class FireEndState : State
{
	Fire fsm;
	public FireEndState(Fire _fsm)
	{
		fsm = _fsm;
	}
	public override void Enter()
	{
		var ground = fsm.GetComponentInParent<GroundTile>();
		var objectsMesh = 0f;
		for (int i = 0; i < ground.fillList.Count; i++)//오브젝트매질 검사
		{
			if (ground.fillList[i].GetComponent<Obstacle>() != null)
			{
				objectsMesh = ground.fillList[i].GetComponent<Obstacle>().obstacleMesh;
				break;
			}
		}
		var weat = 0;
		if (ground.tileIsWeat)
		{
			weat = ground.tileWeatValue;
		}
		else
		{
			weat = 0;
		}
		fsm.fireHp = fsm.fireExpGrowth * (ground.tileMesh - weat + objectsMesh);
		if(fsm.fireHp > 0 && fsm.fireHp <= 10)
		{
			fsm.fireLevel = 1;
		}
		else if(fsm.fireHp > 10 && fsm.fireHp <= 20)
		{
			fsm.fireLevel = 2;
		}


	}
	public override void Update()
	{
		
	}
	public override void Exit()
	{
		
	}
}
