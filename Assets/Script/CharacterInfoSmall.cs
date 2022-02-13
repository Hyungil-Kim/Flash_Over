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

	public GameObject claimantPanel;
	public Button claimantPanelButton;
	public TextMeshProUGUI claimantHpGauge;
	public Slider claimantHpSlider;
	public TextMeshProUGUI claimantBreathGauge;
	public Slider claimantBreathSlider;
	private void Awake()
	{
		gameManager = GameManager.instance;
	}
	private void OnEnable()
	{
		if(gameManager.uIManager.InfoUiScript.smallInfo.gameObject.activeSelf)
		{
			gameManager.uIManager.InfoUiScript.charaterInfo.gameObject.SetActive(false);
		}
		if (gameManager.targetPlayer != null)
		{
			if (gameManager.targetPlayer.handFull)
			{
				claimantPanelButton.gameObject.SetActive(true);
				claimantPanel.SetActive(true);
			}
			else
			{
				claimantPanelButton.gameObject.SetActive(false);
				claimantPanel.SetActive(false);
			}
		}
		
	}
	private void OnDisable()
	{
		if (!gameManager.uIManager.InfoUiScript.smallInfo.gameObject.activeSelf)
		{
			gameManager.uIManager.InfoUiScript.charaterInfo.gameObject.SetActive(true);
		}
	}
	private void Update()
	{
		Init();
	}
	
	public void Init()
	{
		var player = gameManager.targetPlayer;
		if (player != null)
		{
			airGauge.text = player.oxygentank.ToString();
			hpGauge.text = $"{player.cd.hp}/{player.cd.maxhp}";
			hpSlider.value = (float)player.cd.hp / (float)player.cd.maxhp;
			breathGauge.text = $"{player.cd.oxygen}/{player.cd.maxoxygen}";
			breathSlider.value = (float)player.cd.oxygen / (float)player.cd.maxoxygen;
			powerGauge.text = $"{player.cd.weight}/{player.cd.totalStats.str.stat}";
			powerSlider.value = (float)player.cd.weight / (float)player.cd.totalStats.str.stat;
			if(player.handFull)
			{
				
				claimantPanelButton.gameObject.SetActive(true);
				var claimant = player.handList[0].GetComponent<Claimant>();
				claimantHpGauge.text = $"{claimant.hp}/{claimant.data.hp}";
				claimantHpSlider.value = (float)claimant.hp / (float)claimant.data.hp;
				claimantBreathGauge.text = $"{claimant.ap}/{claimant.data.lung}"; 
				claimantBreathSlider.value = (float)claimant.ap / (float)claimant.data.lung;
			}
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
	public void ClaimantInfoOnOff()
	{
		Debug.Log(claimantPanel.activeSelf);
		if(claimantPanel.activeSelf)
		{
			claimantPanel.SetActive(false);
		}
		else
		{
			if(gameManager.targetPlayer != null && gameManager.targetPlayer.handFull)
				claimantPanel.SetActive(true);
		}

	}
}
