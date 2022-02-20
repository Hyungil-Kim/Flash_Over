using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ClaimantInfo2 : MonoBehaviour
{
	public GameManager gameManager;
    public TextMeshProUGUI claimantState;
    public Slider sliderHp;
    public Slider sliderBreath;
	private void Awake()
	{
		gameManager = GameManager.instance;
	}
	private void OnEnable()
	{
			gameManager.uIManager.InfoUiScript.smallInfo.gameObject.SetActive(false);
			gameManager.uIManager.InfoUiScript.charaterInfo.gameObject.SetActive(false);
	}
	private void OnDisable()
	{
			gameManager.uIManager.InfoUiScript.smallInfo.gameObject.SetActive(true);
	}
	public void UpdateClaimantInfo()
	{
		if(gameManager.target.tag == "Claimant")
		{
			var claimant = gameManager.target.GetComponent<Claimant>();
			if (claimant.stun)
			{
				claimantState.text = "기절";
			}
			else
			{
				switch (claimant.num)
				{
					case 0:
						claimantState.text = "멈춤";
						break;
					case 1:
						claimantState.text = "플레이어에게 이동";
						break;
					case 2:
						claimantState.text = "랜덤이동";
						break;
					case 3:
						claimantState.text = "출구로 이동";
						break;
					default:
						claimantState.text = "랜덤이동";
						break;
				}
			}
			sliderHp.value = (float)claimant.hp / (float)claimant.data.hp;
			sliderBreath.value = (float)claimant.ap / (float)claimant.data.lung;
		}
	}
}
