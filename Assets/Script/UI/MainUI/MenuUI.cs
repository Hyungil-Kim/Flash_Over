using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{

	public Button optionButton;
	public Button titleButton;
	public Button Cancle;
	public GameObject soundOption;

	public void Start()
	{
		optionButton.onClick.AddListener(() => OnClickOptionButton());
		titleButton.onClick.AddListener(() => OnClickTitleButton());
		Cancle.onClick.AddListener(() => OnClickEndMenu());
	}

	public void OnClickOptionButton()
	{
		soundOption.SetActive(true);
		gameObject.SetActive(false);
	}

	public void OnClickTitleButton()
	{
		Turn.OnDestroy();
		AllTile.OnDestroy();
		SceneManager.LoadScene("TitleScene");
	}

	public void OnClickEndMenu()
	{
		gameObject.SetActive(false);

	}

}
