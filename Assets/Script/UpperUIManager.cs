using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UpperUIManager : MonoBehaviour
{
	public MenuManager menuManager;

	public Button menuButton;
	public TMP_Dropdown dropdown;
	public TextMeshProUGUI turn;
	public TextMeshProUGUI rescueClaimant;

	public void OnclickMenuButton()
	{
		menuButton.gameObject.SetActive(true);
	}
	
}
