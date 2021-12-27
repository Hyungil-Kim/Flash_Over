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
    public Button attackButton;
    public Button moveButton;
    public Button cancleButton;
    public GameObject attack;
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
	}
    public void OnClickWeapon2()
    {
		tilemapManager.ResetAttackRange(gameManager.num);
		gameManager.num = 2;
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
				gameManager.targetPlayer.moveHelper.transform.position = gameManager.targetPlayer.transform.position;
				gameManager.targetPlayer = null;
				gameManager.playerMove.moveList.Clear();
				break;
			case PlayerState.Attack:
				tilemapManager.ResetAttackRange(gameManager.num);
				break;
			case PlayerState.End:
				break;
		}
	}
	public void DoAttack()
	{
		tilemapManager.DoAttack(gameManager.num,gameManager.targetPlayer);
		gameManager.targetPlayer.SetState(PlayerState.End);
	}
	public void EndTurn()
	{
		gameManager.targetPlayer.SetState(PlayerState.End);
	}

}
