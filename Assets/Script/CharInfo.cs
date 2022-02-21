using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharInfo : MonoBehaviour
{
	public GameManager gameManager;

	public Button changeTileInfo;

	public Image image;
	public TextMeshProUGUI name;
	public Slider hpSlider;
	public Slider breathSlider;
	public Slider powerSlider;
	public TextMeshProUGUI sightNum;
	public TextMeshProUGUI moveNum;
	public TextMeshProUGUI straightDamage;
	public TextMeshProUGUI roundDamage;
	public TextMeshProUGUI armorNum;
	public TextMeshProUGUI airgaugeNum;
	public TextMeshProUGUI[] talentNum;


	public void Awake()
	{
		gameManager = GameManager.instance;
	}
	public void UpdateData(Player targetPlayer)
	{
		if (targetPlayer != null)
		{
			name.text = targetPlayer.cd.characterName;
			image.sprite = targetPlayer.cd.portrait;
			moveNum.text = targetPlayer.cd.totalStats.move.ToString();
			hpSlider.value = (float)targetPlayer.cd.hp / (float)targetPlayer.cd.maxhp;
			breathSlider.value = (float)targetPlayer.cd.oxygen / (float)targetPlayer.cd.maxoxygen;
			powerSlider.value = (float)targetPlayer.cd.weight / (float)targetPlayer.cd.totalStats.str.stat;
			sightNum.text = targetPlayer.cd.totalStats.vision.ToString();
			straightDamage.text = targetPlayer.cd.totalStats.dmg.ToString();
			roundDamage.text = (targetPlayer.cd.totalStats.dmg *0.6).ToString();
			armorNum.text = targetPlayer.cd.totalStats.def.ToString();
			airgaugeNum.text = targetPlayer.oxygentank.ToString();
			//특성수만큼
			for(int i =0; i < 9;i++)
			{
				if (i < targetPlayer.cd.characteristics.Count)
				{
					talentNum[i].text = targetPlayer.cd.characteristics[i].name;
				}
				else
				{
					talentNum[i].text = "";				
				}
			}
		}
	}
	public void ClickChangeTileInfoButton()
	{
		gameManager.target = gameManager.tilemapManager.ReturnTile(gameManager.targetPlayer.gameObject).gameObject;
		gameManager.uIManager.InfoUiScript.ChangeInfoUi(1);
		gameObject.SetActive(false);
	}

}
