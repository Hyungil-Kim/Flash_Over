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
	private void OnEnable()
	{
		if (gameManager.uIManager.InfoUiScript.charaterInfo.gameObject.activeSelf)
		{
			gameManager.uIManager.InfoUiScript.smallInfo.gameObject.SetActive(false);
		}
	}
	private void OnDisable()
	{
		if (!gameManager.uIManager.InfoUiScript.charaterInfo.gameObject.activeSelf)
		{
			gameManager.uIManager.InfoUiScript.smallInfo.gameObject.SetActive(true);
		}
	}
	public void UpdateData(Player targetPlayer)
	{
		if (targetPlayer != null)
		{
			name.text = targetPlayer.name;
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
		}
	}
	public void ClickChangeTileInfoButton()
	{
		gameManager.target = gameManager.tilemapManager.ReturnTile(gameManager.targetPlayer.gameObject).gameObject;
		gameManager.uIManager.InfoUiScript.tileInfo.gameObject.SetActive(true);
		gameObject.SetActive(false);
	}

}
