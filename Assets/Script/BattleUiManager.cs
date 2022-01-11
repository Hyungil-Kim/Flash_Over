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

	public UseItemManager useItemManager;
	

	// Start is called before the first frame update

	public void Start()
	{
       gameManager = uIManager.gameManager;
		tilemapManager = gameManager.tilemapManager;
	}
	public void OnClickWeapon1()
	{
		tilemapManager.ResetAttackRange(gameManager.num);
		gameManager.num = 1;
		tilemapManager.ChangeColorAttack(gameManager.targetPlayer.gameObject, gameManager.num, gameManager.setAttackColor);

	}
    public void OnClickWeapon2()
    {
		tilemapManager.ResetAttackRange(gameManager.num);
		gameManager.num = 2;
		tilemapManager.ChangeColorAttack(gameManager.targetPlayer.gameObject, gameManager.num, gameManager.setAttackColor);
	}
	public void OnClickAttackButton()
	{
		shootButton.gameObject.SetActive(true);
		weapon1Button.gameObject.SetActive(true);
		weapon2Button.gameObject.SetActive(true);

		itemButton.gameObject.SetActive(false);
		rescueButton.gameObject.SetActive(false);
		putDownButton.gameObject.SetActive(false);
		waitButton.gameObject.SetActive(false);
		attackButton.gameObject.SetActive(false);
	}
	public void Cancle()
	{
		switch (gameManager.targetPlayer.curStateName)
		{
			case PlayerState.Idle:
				break;
			case PlayerState.Move:
				tilemapManager.ResetFloodFill();
				gameManager.targetPlayer.curStateName = PlayerState.Idle;
				gameManager.targetPlayer.moveHelper.transform.localPosition = Vector3.zero;

				//gameManager.cameraController.CameraForObjectsCenter(gameManager.pretargetPlayer.gameObject);
				gameManager.targetPlayer = null;
				gameManager.playerMove.moveList.Clear();
				break;
			case PlayerState.Action:
				
				gameManager.pickup = false;
				gameManager.putdown = false;

				if(!attackButton.gameObject.activeSelf)
				{
					tilemapManager.ResetAttackRange(gameManager.num);
					gameManager.num = -1;
					shootButton.gameObject.SetActive(false);
					weapon1Button.gameObject.SetActive(false);
					weapon2Button.gameObject.SetActive(false);
				}
				if(useItemManager.gameObject.activeSelf)
				{
					useItemManager.gameObject.SetActive(false);
				}
					itemButton.gameObject.SetActive(true);
					waitButton.gameObject.SetActive(true);
					attackButton.gameObject.SetActive(true);
				StartCoroutine(useItemManager.UseItemEnd());
				break;
			case PlayerState.End:
				break;
		}
	}
	public void DoAttack()
	{
		if(gameManager.num != -1)
		{ 
			tilemapManager.DoAttack(gameManager.targetPlayer, gameManager.num);
			gameManager.targetPlayer.SetState(PlayerState.End);
		}
	}
	public void DoPickClaimant()
	{
		Cancle();
		if (gameManager.targetPlayer.handFull)
			return;
		var playerTile = gameManager.tilemapManager.ReturnTile(gameManager.targetPlayer.gameObject);
		gameManager.tilemapManager.ShowFloodFillRange(playerTile, gameManager.setMoveColor, 1);
		gameManager.pickup = true;
	}
	public void DoPutDownClaimant()
	{
		Cancle();
		if (!gameManager.targetPlayer.handFull)
			return;
		var playerTile = gameManager.tilemapManager.ReturnTile(gameManager.targetPlayer.gameObject);
		gameManager.tilemapManager.ShowFloodFillRange(playerTile, gameManager.setMoveColor, 1);
		gameManager.putdown = true;
	}
	public void UseItem()
	{
		Cancle();
		uIManager.battleUiManager.waitButton.gameObject.SetActive(false);
		uIManager.battleUiManager.attackButton.gameObject.SetActive(false);
		useItemManager.gameObject.SetActive(true);
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
					gameManager.num = -1;
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

}
