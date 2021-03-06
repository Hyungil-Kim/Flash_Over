using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
	private GameManager gameManager;
	public Button restartButton;
	public Button saveButton;
	public Button loadButton;
	public Button optionButton;
	public Button titleButton;
	public Button Cancle;

	public void Start()
	{
		gameManager = GameManager.instance;
	}
	public void  OnclickRestartButton()
	{

		//Turn.OnDestroy();
		//AllTile.OnDestroy();

		//SceneManager.LoadScene(SceneManager.GetActiveScene().name);

		var checkUi = gameManager.uIManager.checkUI;
		checkUi.gameObject.SetActive(true);
		gameManager.uIManager.restart = true;
	}
	public void OnClickOptionButton()
	{
		gameManager.uIManager.option.SetActive(true);
		gameObject.SetActive(false);
	}

	public void OnClickTitleButton()
	{
		var checkUi = gameManager.uIManager.checkUI;
		checkUi.gameObject.SetActive(true);
		gameManager.uIManager.title = true;

		Turn.OnDestroy();
		AllTile.OnDestroy();
	}

	public void OnClickEndMenu()
	{
		gameObject.SetActive(false);
	}

}
