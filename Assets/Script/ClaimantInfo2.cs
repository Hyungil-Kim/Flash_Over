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
				claimantState.text = "����";
			}
			else
			{
				switch (gameManager.target.GetComponent<Claimant>().num)
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
		}
	}
}
