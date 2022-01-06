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
	public Button startButton;
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
		tilemapManager.ChangeColorAttack(gameManager.targetPlayer.gameObject, gameManager.num, gameManager.setAttackColor);

	}
    public void OnClickWeapon2()
    {
		tilemapManager.ResetAttackRange(gameManager.num);
		gameManager.num = 2;
		tilemapManager.ChangeColorAttack(gameManager.targetPlayer.gameObject, gameManager.num, gameManager.setAttackColor);
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
				gameManager.num = -1;
				break;
			case PlayerState.End:
				break;
		}
	}
	public void DoAttack()
	{
		Debug.Log(gameManager.num);
		if(gameManager.num != -1)
		{ 
		tilemapManager.DoAttack(gameManager.targetPlayer, gameManager.num);
		gameManager.targetPlayer.SetState(PlayerState.End);
		}
	}
	public void EndTurn()
	{
		switch (gameManager.targetPlayer.curStateName)
		{
			case PlayerState.Idle:
				break;
			case PlayerState.Move:
			gameManager.targetPlayer.SetState(PlayerState.Attack);
				break;
			case PlayerState.Attack:
			gameManager.targetPlayer.SetState(PlayerState.End);
				break;
			case PlayerState.End:
				break;
		}
	}
	public void StartGame()
    {
		gameManager.isStart = true;
		startButton.gameObject.SetActive(false);
    }
}
