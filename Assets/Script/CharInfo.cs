using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharInfo : MonoBehaviour
{
	public GameManager gameManager;

	public Button changeCharacterInfo;
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
	public TextMeshProUGUI talentNum1;
	public TextMeshProUGUI talentNum2;
	public TextMeshProUGUI talentNum3;
	public TextMeshProUGUI talentNum4;
	public TextMeshProUGUI talentNum5;
	public TextMeshProUGUI talentNum6;
	public TextMeshProUGUI talentNum7;
	public TextMeshProUGUI talentNum8;
	public TextMeshProUGUI talentNum9;

	public void Start()
	{
		gameManager = GameManager.instance;
	}

	public void UpdateData()
	{
		if (gameManager.targetPlayer != null)
		{
			moveNum.text = gameManager.targetPlayer.cd.totalStats.move.ToString();
			hpSlider.value = (float)gameManager.targetPlayer.cd.hp / (float)gameManager.targetPlayer.cd.maxhp;
			breathSlider.value = (float)gameManager.targetPlayer.cd.oxygen / (float)gameManager.targetPlayer.cd.maxoxygen;
			powerSlider.value = (float)gameManager.targetPlayer.cd.weight / (float)gameManager.targetPlayer.cd.totalStats.str.stat;
			sightNum.text = gameManager.targetPlayer.cd.totalStats.vision.ToString();
			straightDamage.text = gameManager.targetPlayer.cd.totalStats.dmg.ToString();
			roundDamage.text = (gameManager.targetPlayer.cd.totalStats.dmg *0.6).ToString();
			armorNum.text = gameManager.targetPlayer.cd.totalStats.def.ToString();
			airgaugeNum.text = gameManager.targetPlayer.oxygentank.ToString();
			//특성수만큼
		}
	}
	public void ClickChangeCharInfoButton()
	{
		gameManager.uIManager.InfoUiScript.tileInfo.SetActive(true);
		gameObject.SetActive(false);
	}
	public void ClickChangeTileInfoButton()
	{

	}

}
