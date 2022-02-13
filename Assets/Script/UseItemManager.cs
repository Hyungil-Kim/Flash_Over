using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UseItemManager : MonoBehaviour
{
	public Button useitemButton1;
	public Button useitemButton2;
	public TextMeshProUGUI count1;
	public TextMeshProUGUI count2;
	private GameManager gameManager;
	public TextMeshProUGUI name1;
	public TextMeshProUGUI name2;
	public TextMeshProUGUI info1;
	public TextMeshProUGUI info2;
	public Image image1;
	public Image image2;

	public Sprite tutoImage1;
	public Sprite tutoImage2;

	public int Id;
	public string icon_id;
	public string prefab_id;
	public string name;
	public string informaition;
	public UseItemType itemType;
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
	public ConsumableItemTableData consumableItemTableData;
	public void Start()
	{
		gameManager = GameManager.instance;
	}
	public void UpdateItem()
	{
		gameManager = GameManager.instance;
		var player = gameManager.targetPlayer.GetComponent<Player>();
		if (gameManager.tutorial)
		{
			name1.text = "구급키트";
			info1.text = "체력을 50 회복합니다";
			image1.sprite = tutoImage1;
			count1.text = "2";

			name2.text = "산소통";
			info2.text = "산소통을 5회 충전합니다";
			image2.sprite = tutoImage2;
			count2.text = "1";

		}
		else
		{
			if (player.cd.consum1 == null || player.cd.consum1.count <= 0)
			{
				useitemButton1.interactable = false;
				image1.sprite = null;
				count1.text = null;
				name1.text = null;
				info1.text = null;
			}
			else
			{
				useitemButton1.interactable = true;
				name1.text = player.cd.consum1.itemData.id;
				info1.text = player.cd.consum1.itemData.description;
				count1.text = player.cd.consum1.count.ToString();
				image1.sprite = player.cd.consum1.itemData.iconSprite;


			}
			if (player.cd.consum2 == null || player.cd.consum2.count <= 0)
			{
				useitemButton2.interactable = false;
				image2.sprite = null;
				count2.text = null;
				name2.text = null;
				info2.text = null;
			}
			else
			{
				useitemButton2.interactable = true;
				name2.text = player.cd.consum2.itemData.id;
				info2.text = player.cd.consum2.itemData.description;
				count2.text = player.cd.consum2.count.ToString();
				image2.sprite = player.cd.consum2.itemData.iconSprite;
			}
		}

	}
	public void ClickFirstItem()
	{
		var player = gameManager.targetPlayer.GetComponent<Player>();
		if (gameManager.tutorial)
		{
			itemRange = 3;
			throwRange = 2;
			damage = 50;
			itemType = UseItemType.Damage;
		}
		else
		{
			if (player.cd.consum1 == null || player.cd.consum1.count <= 0)
			{
				
				image1.sprite = null;
				count1.text = null;
				name1.text = null;
				info1.text = null;
			}
			else
			{
			
				icon_id = player.cd.consum1.itemData.iconID;
				prefab_id = player.cd.consum1.itemData.prefabsID;
				healItemType = player.cd.consum1.itemData.healItemType;
				itemRange = player.cd.consum1.itemData.range;
				throwRange = player.cd.consum1.itemData.throwRange;
				damage = player.cd.consum1.itemData.dmg;
				itemType = player.cd.consum1.itemData.useItemType;
			}
		}
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
		var player = gameManager.targetPlayer.GetComponent<Player>();
		if (gameManager.tutorial)
		{
			itemRange = 1;
			throwRange = 0;
			damage = 5;
			itemType = UseItemType.Heal;
			healItemType = HealItemType.MP;
		}
		else
		{
			if (player.cd.consum2 == null || player.cd.consum2.count <= 0)
			{
			
				image2.sprite = null;
				count2.text = null;
				name2.text = null;
				info2.text = null;
			}
			else
			{
				icon_id = player.cd.consum2.itemData.iconID;
				prefab_id = player.cd.consum2.itemData.prefabsID;
				healItemType = player.cd.consum2.itemData.healItemType;
				itemRange = player.cd.consum2.itemData.range;
				throwRange = player.cd.consum2.itemData.throwRange;
				damage = player.cd.consum2.itemData.dmg;
				itemType = player.cd.consum2.itemData.useItemType;
			}
		}
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
