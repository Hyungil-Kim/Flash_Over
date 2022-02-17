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
		if (fsm.fireHp >= fsm.data.maxhp)
		{
			fsm.fireLevel++;
		}
		else if (fsm.fireHp < fsm.data.minhp)
		{
			fsm.fireLevel--;
		}
		fsm.fireLevel = Mathf.Clamp(fsm.fireLevel, 0, 3);
		fsm.data = MyDataTableMgr.fireTable.GetTable(fsm.fireLevel);
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
		for (int i = 0; i < ground.fillList.Count; i++)//������Ʈ���� �˻�
		{
			if (ground.fillList[i].GetComponent<Obstacle>() != null)
			{
				objectsMesh = ground.fillList[i].GetComponent<Obstacle>().obstacleMesh;
				break;
			}
		}
		var weat = 0f;
		if (ground.tileIsWeat)
		{
			weat = ground.tileWeatValue;
		}
		else
		{
			weat = 0;
		}

		fsm.fireHp += fsm.data.increaseExp * (ground.tileMesh - weat + objectsMesh);

	}
	public override void Update()
	{
		
	}
	public override void Exit()
	{
		
	}
}
