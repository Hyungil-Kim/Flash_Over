using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUiManager : MonoBehaviour
{
    private GameManager gameManager;
	private TilemapManager tilemapManager;
	public UIManager uIManager;
    public Button weapon1Button;
    public Button weapon2Button;
    public Button itemButton;
    public Button waitButton;
    public Button shootButton;
    public Button moveButton;
    public Button cancleButton;
    public Button attackButton;

	public Button rescueButton;
	public Button putDownButton;

	public Button openDoorButton;
	public Button closeDoorButton;

	public Button fuseOffButton;
	public Button selectNextPlayer;

	public UseItemManager useItemManager;

	public Door findDoor;
	// Start is called before the first frame update

	public void Start()
	{
		gameManager = GameManager.instance;
	   tilemapManager = gameManager.tilemapManager;
	}
	public void OnClickWeapon1()
	{
		tilemapManager.ResetAttackRange(gameManager.num);
		gameManager.num = 1;
		tilemapManager.ChangeColorAttack(gameManager.targetPlayer.gameObject, gameManager.num, gameManager.setAttackColor);
		weapon1Button.gameObject.SetActive(false);
		weapon2Button.gameObject.SetActive(true);
	}
    public void OnClickWeapon2()
    {
		tilemapManager.ResetAttackRange(gameManager.num);
		gameManager.num = 2;
		tilemapManager.ChangeColorAttack(gameManager.targetPlayer.gameObject, gameManager.num, gameManager.setAttackColor);
		weapon1Button.gameObject.SetActive(true);
		weapon2Button.gameObject.SetActive(false);
	}
	public void OnClickAttackButton()
	{
		shootButton.gameObject.SetActive(true);
		gameManager.num = 1;
		if (gameManager.num == 1)
		{
			OnClickWeapon1();
		}
		else if (gameManager.num ==2)
		{
			OnClickWeapon2();
		}
		itemButton.gameObject.SetActive(false);
		rescueButton.gameObject.SetActive(false);
		putDownButton.gameObject.SetActive(false);
		waitButton.gameObject.SetActive(false);
		attackButton.gameObject.SetActive(false);
		gameManager.readyPlayerAction = false;

	}
	public void Cancle()
	{
		switch (gameManager.targetPlayer.curStateName)
		{
			case PlayerState.Idle:
				break;
			case PlayerState.Move:
				if (gameManager.playerMove.moveList.Count <= 1)
				{
					tilemapManager.ResetFloodFill();
					gameManager.targetPlayer.curStateName = PlayerState.Idle;
					gameManager.targetPlayer.moveHelper.transform.localPosition = Vector3.zero;

					//gameManager.cameraController.CameraForObjectsCenter(gameManager.pretargetPlayer.gameObject);
					AllButtonOff();
					gameManager.targetPlayer = null;
					gameManager.playerMove.moveList.Clear();
				}
				else
				{
					tilemapManager.ResetFloodFill();
					gameManager.targetPlayer.curStateName = PlayerState.Idle;
					gameManager.targetPlayer.moveHelper.transform.localPosition = Vector3.zero;

					//gameManager.cameraController.CameraForObjectsCenter(gameManager.pretargetPlayer.gameObject);
					gameManager.playerMove.moveList.Clear();
					gameManager.ChangeTargetPlayer(gameManager.targetPlayer.gameObject);
					
				}
				break;
			case PlayerState.Action:
				
				gameManager.pickup = false;
				gameManager.putdown = false;
					tilemapManager.ResetAttackRange(gameManager.num);
				gameManager.num = -1;
					shootButton.gameObject.SetActive(false);
					weapon1Button.gameObject.SetActive(false);
					weapon2Button.gameObject.SetActive(false);

				if(useItemManager.gameObject.activeSelf)
				{
					useItemManager.gameObject.SetActive(false);
				}
					itemButton.gameObject.SetActive(true);
					waitButton.gameObject.SetActive(true);
					attackButton.gameObject.SetActive(true);
					StartCoroutine(useItemManager.Cancle());
				gameManager.readyPlayerAction = true;
				break;
			case PlayerState.End:
				break;
		}
	}

	public void DoAttack()
	{
		if(gameManager.num != -1)
		{
			shootButton.gameObject.SetActive(false);
			weapon1Button.gameObject.SetActive(false);
			weapon2Button.gameObject.SetActive(false);
			cancleButton.gameObject.SetActive(false);
			tilemapManager.DoAttack(gameManager.targetPlayer, gameManager.num);
		}
	}
	public void DoPickClaimant()
	{
		Cancle();
		if (gameManager.targetPlayer.handFull)
			return;
		var playerTile = gameManager.tilemapManager.ReturnTile(gameManager.targetPlayer.gameObject);
		gameManager.tilemapManager.ShowFloodFillRange(playerTile, gameManager.setMoveColor, 1);
		gameManager.readyPlayerAction = false;
		gameManager.pickup = true;
	}
	public void DoPutDownClaimant()
	{
		Cancle();
		if (!gameManager.targetPlayer.handFull)
			return;
		var playerTile = gameManager.tilemapManager.ReturnTile(gameManager.targetPlayer.gameObject);
		gameManager.tilemapManager.ShowFloodFillRange(playerTile, gameManager.setMoveColor, 1);
		gameManager.readyPlayerAction = false;
		gameManager.putdown = true;
	}
	public void UseItem()
	{
		Cancle();
		uIManager.battleUiManager.waitButton.gameObject.SetActive(false);
		uIManager.battleUiManager.attackButton.gameObject.SetActive(false);
		gameManager.readyPlayerAction = false;
		useItemManager.gameObject.SetActive(true);
	}
	public void DoorInteractionOpen()
	{
		Cancle();
		gameManager.targetPlayer.animator.SetBool("openDoor", true);
		StartCoroutine(findDoor.OpenDoor());
		uIManager.battleUiManager.openDoorButton.gameObject.SetActive(false);
	}
	public void DoorInteractionClose()
	{
		Cancle();
		//StartCoroutine(findDoor.CloseDoor());
	}
	public void FuseBoxInteractionOff()
	{
		Cancle();
		FuseBox.FuseOFF = true;
		gameManager.targetPlayer.curStateName = PlayerState.End;
	}
	public void SelectNextPlayer()
	{
		gameManager.tilemapManager.SelectNextPlayer();
	}
	
	public void EndTurn()
	{
		switch (gameManager.targetPlayer.curStateName)
		{
			case PlayerState.Idle:
				break;
			case PlayerState.Move:
				gameManager.targetPlayer.SetState(PlayerState.Action);
				break;
			case PlayerState.Action:
				gameManager.pickup = false;
				gameManager.putdown = false;

				if (!attackButton.gameObject.activeSelf)
				{
					tilemapManager.ResetAttackRange(gameManager.num);
					shootButton.gameObject.SetActive(false);
					weapon1Button.gameObject.SetActive(false);
					weapon2Button.gameObject.SetActive(false);
				}
				if (useItemManager.gameObject.activeSelf)
				{
					useItemManager.gameObject.SetActive(false);
				}
				itemButton.gameObject.SetActive(true);
				waitButton.gameObject.SetActive(true);
				attackButton.gameObject.SetActive(true);
				gameManager.targetPlayer.SetState(PlayerState.End);
				break;
			case PlayerState.End:
				break;
		}
	}
	public void AllButtonOff()
	{
		weapon1Button.gameObject.SetActive(false);
		weapon2Button.gameObject.SetActive(false);
		itemButton.gameObject.SetActive(false);
		waitButton.gameObject.SetActive(false);
		shootButton.gameObject.SetActive(false);
		moveButton.gameObject.SetActive(false);
		cancleButton.gameObject.SetActive(false);
		attackButton.gameObject.SetActive(false);
		rescueButton.gameObject.SetActive(false);
		putDownButton.gameObject.SetActive(false);
		openDoorButton.gameObject.SetActive(false);
		closeDoorButton.gameObject.SetActive(false);
		gameManager.num = -1;
		fuseOffButton.gameObject.SetActive(false);
		selectNextPlayer.gameObject.SetActive(false);
	}
}
