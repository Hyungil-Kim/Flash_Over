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
				claimantState.text = "����";
			}
			else
			{
				switch (claimant.num)
				{
					case 0:
						claimantState.text = "����";
						break;
					case 1:
						claimantState.text = "�÷��̾�� �̵�";
						break;
					case 2:
						claimantState.text = "�����̵�";
						break;
					case 3:
						claimantState.text = "�ⱸ�� �̵�";
						break;
					default:
						claimantState.text = "�����̵�";
						break;
				}
			}
			sliderHp.value = (float)claimant.hp / (float)claimant.data.hp;
			sliderBreath.value = (float)claimant.ap / (float)claimant.data.lung;
		}
	}
}
