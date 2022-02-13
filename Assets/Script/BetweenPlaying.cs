using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BetweenPlaying : MonoBehaviour
{
	private GameManager gameManager;
	public GameObject startPanel;
	public GameObject startPlayerTurn;
	public GameObject winPanel;
	public GameObject falsePanel;
	public TextMeshProUGUI startPanelWorldName;
	public TextMeshProUGUI startPanelQuset1Text;
	public TextMeshProUGUI startPanelQuset2Text;
	

	public bool playerTurn;
	public void Awake()
	{

	}
	public void Start()
	{
		gameManager = GameManager.instance;
	}

	public void UpdateStartPanel()
	{
		
	}

	public void ShowStartPanel()
	{
		StartCoroutine(StartPanel());
	}


	public void ShowStartPlayerTurn()
	{
		StartCoroutine(StartPlayerTurn());
	}

	public void ShowWinPanel()
	{
		StartCoroutine(WinPanel());
	}

	public void ShowFalsePanel()
	{
		StartCoroutine(FalsePanel());
	}

	public IEnumerator StartPanel()
	{
		UpdateStartPanel();
		startPanel.SetActive(true);
		yield return new WaitForSeconds(1f);
		startPanel.SetActive(false);
		ShowStartPlayerTurn();
	}


	public IEnumerator StartPlayerTurn()
	{
		startPlayerTurn.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		startPlayerTurn.SetActive(false);
		playerTurn = true;
	}

	public IEnumerator WinPanel()
	{
		winPanel.SetActive(true);
		yield return new WaitForSeconds(1f);
		winPanel.SetActive(false);

		yield return new WaitForSeconds(1f);
		Turn.win = true;
		SceneManager.LoadScene("EndScene");
	}

	public IEnumerator FalsePanel()
	{
		falsePanel.SetActive(true);
		yield return new WaitForSeconds(1f);
		falsePanel.SetActive(false);

		yield return new WaitForSeconds(1f);
		Turn.lose = true;
		SceneManager.LoadScene("EndScene");
	}
	public void StopTurn()
	{
		StopCoroutine(Turn.CoTurnSystem());
	}
}
