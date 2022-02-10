using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckUi : MonoBehaviour
{
	public Button yesButton;
	public Button noButton;
	public bool result;
	public bool select;

	public void OnclickYesButton()
	{

		select = true;
		result = true;

		gameObject.SetActive(false);
	}
	public void OnclickNOButton()
	{

		select = true;
		result = false;

		gameObject.SetActive(false);
	}

}
