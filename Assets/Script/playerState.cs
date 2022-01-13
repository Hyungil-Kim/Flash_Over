using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : State
{
    Player fsm;
	CameraController cameraController;

	public PlayerIdleState(Player _fsm)
	{
		fsm = _fsm;
	CameraController cameraController;

	}
	public override void Enter()
	{

		fsm.moveHelper.transform.localPosition = Vector3.zero;


		//이거 여기 두면 계속 꺼져 안돼..
		//fsm.moveHelper.SetActive(false);
		//fsm.gameManager.uIManager.battleUiManager.moveButton.gameObject.SetActive(false);
		//fsm.gameManager.uIManager.battleUiManager.cancleButton.gameObject.SetActive(false);
		//fsm.gameManager.uIManager.battleUiManager.attackButton.gameObject.SetActive(false);
		//fsm.gameManager.uIManager.battleUiManager.itemButton.gameObject.SetActive(false);
		//fsm.gameManager.uIManager.battleUiManager.shootButton.gameObject.SetActive(false);
		//fsm.gameManager.uIManager.battleUiManager.waitButton.gameObject.SetActive(false);
		//fsm.gameManager.uIManager.battleUiManager.putDownButton.gameObject.SetActive(false);
		//fsm.gameManager.uIManager.battleUiManager.rescueButton.gameObject.SetActive(false);
		//fsm.gameManager.uIManager.battleUiManager.weapon1Button.gameObject.SetActive(false);
		//fsm.gameManager.uIManager.battleUiManager.weapon2Button.gameObject.SetActive(false);

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
		fsm.moveHelper.SetActive(true);
		fsm.gameManager.uIManager.battleUiManager.moveButton.gameObject.SetActive(true);
		fsm.gameManager.uIManager.battleUiManager.cancleButton.gameObject.SetActive(true);
		fsm.gameManager.uIManager.battleUiManager.putDownButton.gameObject.SetActive(false);
		fsm.gameManager.uIManager.battleUiManager.rescueButton.gameObject.SetActive(false);
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
		fsm.gameManager.uIManager.battleUiManager.shootButton.gameObject.SetActive(false);

		fsm.gameManager.uIManager.battleUiManager.cancleButton.gameObject.SetActive(true);

		fsm.gameManager.uIManager.battleUiManager.attackButton.gameObject.SetActive(true);
		fsm.gameManager.uIManager.battleUiManager.itemButton.gameObject.SetActive(true);
		fsm.gameManager.uIManager.battleUiManager.waitButton.gameObject.SetActive(true);
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
		//마찬가지로 .. 이걸 어떻게하나 

		if (fsm.gameManager.targetPlayer == fsm)
		{
			fsm.gameManager.uIManager.battleUiManager.attackButton.gameObject.SetActive(false);
			fsm.gameManager.uIManager.battleUiManager.itemButton.gameObject.SetActive(false);
			fsm.gameManager.uIManager.battleUiManager.waitButton.gameObject.SetActive(false);
			fsm.gameManager.uIManager.battleUiManager.putDownButton.gameObject.SetActive(false);
			fsm.gameManager.uIManager.battleUiManager.rescueButton.gameObject.SetActive(false);

			fsm.gameManager.uIManager.battleUiManager.shootButton.gameObject.SetActive(false);
			fsm.gameManager.uIManager.battleUiManager.weapon1Button.gameObject.SetActive(false);
			fsm.gameManager.uIManager.battleUiManager.weapon2Button.gameObject.SetActive(false);
			fsm.gameManager.uIManager.battleUiManager.cancleButton.gameObject.SetActive(false);
			fsm.gameManager.targetPlayer = null;
		}



		//var nextPlayer = Turn.players.Find((x) => x.index == fsm.index + 1);
		//fsm.gameManager.GetClickedEndMouse(nextPlayer.gameObject);
		//fsm.gameManager.ChangeTargetPlayer(nextPlayer.gameObject);
		//Debug.Log($"다음 플레이어 인덱스 {nextPlayer.index}");
		//fsm.gameManager.GetClickedEndMouse(Turn.players[fsm.index + 1].gameObject);

		fsm.StartCoroutine(Turn.CoTurnSystem());
	}

	public override void Exit()
	{
	}

	public override void Update()
	{
	}

}
