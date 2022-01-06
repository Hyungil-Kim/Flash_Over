using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItemManager : MonoBehaviour
{
    public Button useitemButton1;
    public Button useitemButton2;

    public Button cancleItemButton;
	private GameManager gameManager;

	public int itemRange;
	public int throwRange;
	public string itemRangeType;
	public List<GroundTile> listRange = new List<GroundTile>();
	public List<GroundTile> throwListRange = new List<GroundTile>();
	public void Start()
	{
		gameManager = GameManager.instance;
	}

	public void ClickFirstItem()
	{
		itemRange = 1;
		throwRange = 0;
		itemRangeType = "melee";

		gameObject.SetActive(false);
		var playerTile = gameManager.tilemapManager.ReturnTile(gameManager.targetPlayer.gameObject);
		listRange =	gameManager.tilemapManager.ReturnFloodFillRange(playerTile, gameManager.setMoveColor, itemRange);
		gameManager.showMeleeRange = true;
	    if (itemRangeType == "throw")
		{
			gameManager.showthrowwRange = true;
			throwListRange = gameManager.tilemapManager.ReturnFloodFillRange(gameManager.targetTile,Color.black,throwRange);
		}
	
	}
	public void ClickSecondItem()
	{
		itemRange = 2;
		throwRange = 3;
		itemRangeType = "throw";

		var playerTile = gameManager.tilemapManager.ReturnTile(gameManager.targetPlayer.gameObject);
		var listRange = gameManager.tilemapManager.ReturnFloodFillRange(playerTile, gameManager.setMoveColor, itemRange);
		if (itemRangeType == "throw")
		{

		}
	}
	public IEnumerator UseItem()
	{
		yield return new WaitForSeconds(0.2f);
		gameManager.tilemapManager.ResetFloodFill(listRange);
		listRange.Clear();
		yield return 0;
	}
	private void Update()
	{
		//if (gameManager.targetPlayer != null)
		//{
		//	if (gameManager.targetPlayer.cd.consum1 == null)
		//	{
		//		useitemButton1.gameObject.SetActive(false);
		//	}
		//	else
		//	{
		//		useitemButton1.gameObject.SetActive(true);
		//	}
		//	if (gameManager.targetPlayer.cd.consum2 == null)
		//	{
		//		useitemButton2.gameObject.SetActive(false);
		//	}
		//	else
		//	{
		//		useitemButton2.gameObject.SetActive(true);
		//	}
		//}
	}
}
