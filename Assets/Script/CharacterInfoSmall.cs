using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CharacterInfoSmall : MonoBehaviour
{
	public GameManager gameManager;
	public TextMeshProUGUI hpGauge;
	public Slider hpSlider;
	public TextMeshProUGUI breathGauge;
	public Slider breathSlider;
	public TextMeshProUGUI powerGauge;
	public Slider powerSlider;
	public TextMeshProUGUI airGauge;

	private void Update()
	{
		gameManager = GameManager.instance;
		Init();
	}
	public void Init()
	{
		if (gameManager.targetPlayer != null)
		{
			airGauge.text = gameManager.targetPlayer.cd.totalStats.sta.ToString();
			hpGauge.text = $"{gameManager.targetPlayer.cd.hp}/{gameManager.targetPlayer.cd.maxhp}";
			hpSlider.value = (float)gameManager.targetPlayer.cd.hp / (float)gameManager.targetPlayer.cd.maxhp;
			breathGauge.text = $"{gameManager.targetPlayer.cd.oxygen}/{gameManager.targetPlayer.cd.maxoxygen}";
			breathSlider.value = (float)gameManager.targetPlayer.cd.oxygen / (float)gameManager.targetPlayer.cd.maxoxygen;
			powerGauge.text = $"{gameManager.targetPlayer.cd.weight}/{gameManager.targetPlayer.cd.totalStats.str.stat}";
			powerSlider.value = (float)gameManager.targetPlayer.cd.weight / (float)gameManager.targetPlayer.cd.totalStats.str.stat;
		}
		else
		{
			gameObject.SetActive(false);
		}
	}
	public void SetOnCharaterInfoSmall()
	{
		gameObject.SetActive(true);
	}

}
