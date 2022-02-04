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

	public void UpdateClaimantInfo()
	{
		if(gameManager.target.tag == "Claimant")
		{
			if (gameManager.target.GetComponent<Claimant>().stun)
			{
				claimantState.text = "기절";
			}
			else
			{
				switch (gameManager.target.GetComponent<Claimant>().num)
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
		}
	}
}
