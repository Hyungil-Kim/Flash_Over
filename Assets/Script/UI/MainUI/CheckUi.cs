using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class CheckUi : MonoBehaviour
{
	private GameManager gameManager;
	public Button yesButton;
	public Button noButton;
	public bool result;
	public bool select;

	public void Start()
	{
		gameManager = GameManager.instance;
	}

	public void OnclickYesButton()
	{
		if(gameManager.uIManager.restart)
		{
			Restart();
		}
		else if(gameManager.uIManager.title)
		{
			Tiltle();
		}
	}
	public void OnclickNOButton()
	{
		gameObject.SetActive(false);
	}
	public void Restart()
	{
		Turn.OnDestroy();
		AllTile.OnDestroy();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	public void Tiltle() 
	{
		SceneManager.LoadScene("TitleScene");
	}
	public void Off()
	{
		gameObject.SetActive(false);
	}
}
