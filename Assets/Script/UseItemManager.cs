using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ConsumItemType
{
	Heal,
	Damage
}
public enum HealItemType
{
	HpHeal,
	StaminaHeal
}

public class UseItemManager : MonoBehaviour
{
	public Button useitemButton1;
	public Button useitemButton2;

	public Button cancleItemButton;
	private GameManager gameManager;

	public int Id;
	public int icon_id;
	public int prefab_id;
	public int name;
	public int informaition;
	public ConsumItemType itemType;
	public HealItemType healItemType;
	public int damage;
	public string effect;
	public int itemRange;
	public int throwRange;
	public int count;
	public int price;
	public int weight;

	public List<GroundTile> listRange = new List<GroundTile>();
	public List<GroundTile> throwListRange = new List<GroundTile>();
	public GroundTile preTile;
	public void Start()
	{
		gameManager = GameManager.instance;
	}

	public void ClickFirstItem()
	{
		itemRange = 1;
		throwRange = 0;
		damage = 50;
		itemType = ConsumItemType.Heal;
		healItemType = HealItemType.HpHeal;

		gameObject.SetActive(false);
		var playerTile = gameManager.tilemapManager.ReturnTile(gameManager.targetPlayer.gameObject);
		listRange = gameManager.tilemapManager.ReturnFloodFillRange(playerTile, gameManager.setMoveColor, itemRange);
		gameManager.showMeleeRange = true;
		if (throwRange != 0)
		{
			foreach (var elem in listRange)
			{
				elem.ResetExceptColor();
			}
			gameManager.showMeleeRange = false;
			gameManager.showthrowwRange = true;
		}

	}
	public void ClickSecondItem()
	{
		itemRange = 3;
		throwRange = 2;
		damage = 50;
		itemType = ConsumItemType.Damage;
		gameObject.SetActive(false);
		var playerTile = gameManager.tilemapManager.ReturnTile(gameManager.targetPlayer.gameObject);
		listRange = gameManager.tilemapManager.ReturnFloodFillRange(playerTile, gameManager.setMoveColor, itemRange);
		gameManager.showMeleeRange = true;
		if (throwRange != 0)
		{
			foreach (var elem in listRange)
			{
				elem.ResetExceptColor();
			}
			gameManager.showMeleeRange = false;
			gameManager.showthrowwRange = true;
		}
	}
	public IEnumerator UseItemEnd(Player targetPlayer)
	{
		yield return new WaitForSeconds(0.2f);
		if (listRange.Count > 0)
		{
			gameManager.tilemapManager.ResetFloodFill(listRange);
			listRange.Clear();
			if (throwListRange.Count > 0)
			{
				gameManager.tilemapManager.ResetFloodFill(throwListRange);
				throwListRange.Clear();
			}
		}
		targetPlayer.SetState(PlayerState.End);
		preTile = null;
		yield return 0;
	}
	public IEnumerator Cancle()
	{
		yield return new WaitForSeconds(0.2f);
		if (listRange.Count > 0)
		{
			gameManager.tilemapManager.ResetFloodFill(listRange);
			listRange.Clear();
			if (throwListRange.Count > 0)
			{
				gameManager.tilemapManager.ResetFloodFill(throwListRange);
				throwListRange.Clear();
			}
		}
		preTile = null;
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
