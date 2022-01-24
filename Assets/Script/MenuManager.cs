using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
	public Button restartButton;
	public Button saveButton;
	public Button loadButton;
	public Button optionButton;
	public Button mainButton;
	public Button titleButton;
	public Button Cancle;

	public void  OnclickRestartButton()
	{
		Turn.OnDestroy();
		AllTile.SaveTile.Clear();
		AllTile.allTile.Clear();
		AllTile.visionTile.Clear();
		AllTile.prevVisionTile.Clear();
		AllTile.wallTile.Clear();

		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	
	public void OnClickOptionButton()
	{

	}

	public void OnClickTitleButton()
	{

	}

	public void OnClickEndMenu()
	{
		gameObject.SetActive(false);
	}
}
