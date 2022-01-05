using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : State
{
    Player fsm;
	public PlayerIdleState(Player _fsm)
	{
		fsm = _fsm;
	}
	public override void Enter()
	{
		fsm.moveHelper.transform.position = fsm.transform.position;
		fsm.moveHelper.SetActive(false);
		fsm.gameManager.uIManager.battleUiManager.moveButton.gameObject.SetActive(false);
		fsm.gameManager.uIManager.battleUiManager.cancleButton.gameObject.SetActive(false);
		fsm.gameManager.uIManager.battleUiManager.attack.gameObject.SetActive(false);
	}

	public override void Exit()
	{
		
	}

	public override void Update()
	{
		
	}
	
}
public class PlayerMoveState : State
{
	Player fsm;
	public PlayerMoveState(Player _fsm)
	{
		fsm = _fsm;
	}
	public override void Enter()
	{
		fsm.gameManager.move = fsm.move;
		fsm.moveHelper.SetActive(true);
		fsm.gameManager.uIManager.battleUiManager.moveButton.gameObject.SetActive(true);
		fsm.gameManager.uIManager.battleUiManager.cancleButton.gameObject.SetActive(true);
	}

	public override void Exit()
	{
		
	}

	public override void Update()
	{
	}

}
public class PlayerAttackState : State
{
	Player fsm;
	public PlayerAttackState(Player _fsm)
	{
		fsm = _fsm;
	}
	public override void Enter()
	{
		fsm.gameManager.uIManager.battleUiManager.moveButton.gameObject.SetActive(false);
		fsm.gameManager.uIManager.battleUiManager.attack.SetActive(true);
		fsm.gameManager.uIManager.battleUiManager.cancleButton.gameObject.SetActive(true);
	}

	public override void Exit()
	{
		
	}

	public override void Update()
	{

	}

}
public class PlayerEndState : State
{
	Player fsm;
	public PlayerEndState(Player _fsm)
	{
		fsm = _fsm;
	}
	public override void Enter()
	{
		fsm.gameManager.uIManager.battleUiManager.attack.SetActive(false);
		fsm.gameManager.uIManager.battleUiManager.cancleButton.gameObject.SetActive(false);
		fsm.gameManager.targetPlayer = null;
		fsm.StartCoroutine(Turn.CoTurnSystem());
	}

	public override void Exit()
	{
	}

	public override void Update()
	{
	}

}
