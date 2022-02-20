using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Linq;
using UnityEngine.SceneManagement;
public class Tutorial : MonoBehaviour
{
	public static Tutorial instance;
	public GameManager gameManager;
	//public List<GroundTile> groundTiles;
	public Tilemap tilemap;
	public Tilemap m_3f;
	public GameObject groundTile;
	public GameObject fireTile;
	public GameObject dummyTile;
	public List<GroundTile> groundTiles;
	public GameObject marker;
	public TutorialUiManager tutorialUiManager;

	private bool once;
	private int number = 1;
	private bool tuto2finish;
	private bool tuto5finish_1;
	private bool tuto5finish_2;
	private bool finishWeaponTuto;
	private bool finishWeaponTuto2;
	private bool tuto5_5finish;
	private bool tuto6finish;
	private bool tuto6finish2;
	private bool tuto6finish3;
	private bool tuto7finish3;
	private bool tuto7finish4;
	private bool tuto8finish;
	private bool check;
	private bool check2;
	private bool check3;
	private bool check4;
	private bool check5;
	private bool check6;
	public Button button;
	public void Awake()
	{
		instance = GetComponent<Tutorial>();
		groundTiles = tilemap.GetComponentsInChildren<GroundTile>().ToList();
	}

	public void ChangeTile(Vector3 position)
	{
		var newPos = tilemap.WorldToCell(position);
		var tile = tilemap.GetInstantiatedObject(newPos);
		var m_2ftile = m_3f.GetInstantiatedObject(newPos);
		if (tile.tag == "Ground" && tile.activeSelf)
		{
			tile.SetActive(false);
			m_2ftile.SetActive(true);
		}
		else
		{
			tile.SetActive(true);
			m_2ftile.SetActive(false);
		}
	}
	public void StartTutorial()
	{
		var offset = new Vector3(0, 6, -3);
		var pos = Turn.players[0].transform.position + offset;
		gameManager.mousePos = pos;
		gameManager.prevPos = pos;
		StartCoroutine(TutorialTurn());
	}
	public void HoldPlayerRotation(Player player, int rot)
	{
		var angles = player.transform.rotation.eulerAngles;
		angles.y = rot;
		player.transform.rotation = Quaternion.Euler(angles);
	}
	public IEnumerator TutorialTurn()
	{
		var player = Turn.players[0];
		foreach (var elem in m_3f.GetComponentsInChildren<MeshRenderer>())
		{
			elem.material.color = new Color(0.5f, 0.5f, 0.5f);
		}
		for (int i = number; i < 8;)
		{
			if (!once)
			{
				foreach (var elem in groundTiles)
				{
					if (elem.tileArea != 8)
						elem.gameObject.SetActive(false);
					else { }
						elem.GetComponent<MeshRenderer>().material.color = new Color(0.5f, 0.5f, 0.5f);
				}
				once = true;
			}
			var sortTiles = groundTiles.Where((x) => x.tileArea == i).ToList();
			foreach (var elem in sortTiles)
			{
				var newPos = tilemap.WorldToCell(elem.transform.position);
				elem.gameObject.SetActive(true);

				var m_3ftile = m_3f.GetInstantiatedObject(newPos);
				if (m_3f.GetInstantiatedObject(newPos))
				{
					m_3ftile.gameObject.SetActive(false);
				}
			}
			if (i == 1)
			{
				gameManager.uIManager.battleUiManager.selectNextPlayer.interactable = false;
				gameManager.uIManager.battleUiManager.cancleButton.interactable = false;
				if (gameManager.targetPlayer.curStateName == PlayerState.Move)
				{
					gameManager.uIManager.battleUiManager.moveButton.interactable = false;
					if (!tuto2finish)
					{
						yield return new WaitForSeconds(1.8f);
						tutorialUiManager.tuto1.SetActive(true);
						if (gameManager.tilemapManager.ReturnTile(gameManager.targetPlayer.moveHelper.gameObject) == gameManager.tilemapManager.ReturnTile(new Vector3(16.5f, 0f, -16.5f)))
						{
							marker.SetActive(false);
							tutorialUiManager.tuto1.SetActive(false);
							tutorialUiManager.tuto2.SetActive(true);
							yield return new WaitForSeconds(1.5f);
							tutorialUiManager.tuto2_Text.SetActive(true);
							while (!tuto2finish)
							{
								if (gameManager.point)
								{
									tutorialUiManager.tuto2.SetActive(false);
									gameManager.uIManager.battleUiManager.moveButton.interactable = true;
									i = 2;
									number = 2;
									once = false;
									tuto2finish = true;
								}
								yield return 0;
							}
							tutorialUiManager.tuto3_Text.SetActive(true);
							foreach (var elem in sortTiles)
							{
								var newPos = tilemap.WorldToCell(elem.transform.position);
								var m_3ftile = m_3f.GetInstantiatedObject(newPos);
								if (m_3f.GetInstantiatedObject(newPos))
								{
									m_3ftile.gameObject.SetActive(true);
								}
							}
						}
					}
				}
			}
			else if (i == 2 && !finishWeaponTuto)
			{
				HoldPlayerRotation(player, 0);
				if (player.curStateName == PlayerState.Action)
				{
					tutorialUiManager.tuto3_Text.SetActive(false);
					tutorialUiManager.tuto4_Text.SetActive(true);
					gameManager.uIManager.battleUiManager.itemButton.interactable = false;
					gameManager.uIManager.battleUiManager.shootButton.interactable = false;
					gameManager.uIManager.battleUiManager.weapon1Button.interactable = false;
					gameManager.uIManager.battleUiManager.weapon2Button.interactable = false;
					gameManager.uIManager.battleUiManager.waitButton.interactable = false;
					while (tutorialUiManager.tuto4_Text.activeSelf)
					{
						if (gameManager.num != -1)
						{
							tutorialUiManager.tuto4_Text.SetActive(false);
							tutorialUiManager.tuto5.SetActive(true);
							gameManager.uIManager.battleUiManager.weapon2Button.interactable = true;
						}
						yield return 0;
					}
					while (!tuto5finish_1)
					{
						if (gameManager.point && tutorialUiManager.tuto5_image1.activeSelf)
						{
							tutorialUiManager.tuto5_image1.SetActive(false);
							tutorialUiManager.tuto5_image2.SetActive(true);
						}
						else if (gameManager.point && tutorialUiManager.tuto5_image2.activeSelf && !tutorialUiManager.tuto5_Text.activeSelf)
						{
							yield return new WaitForSeconds(1.5f);
							tutorialUiManager.tuto5_Text.SetActive(true);
						}
						else if (gameManager.point && tutorialUiManager.tuto5_Text.activeSelf)
						{
							tutorialUiManager.tuto5_Text.SetActive(false);
							tutorialUiManager.tuto5_image2.SetActive(false);
							tutorialUiManager.tuto5.SetActive(false);

							tuto5finish_1 = true;
						}

						yield return 0;
					}
					while (!tuto5finish_2)
					{
						HoldPlayerRotation(player, 0);
						if (gameManager.num == 1)
						{
							tutorialUiManager.tuto5_Text2.SetActive(true);
							HoldPlayerRotation(player, 0);
							gameManager.uIManager.battleUiManager.weapon2Button.interactable = true;
						}
						else if (gameManager.num == 2 && !tutorialUiManager.tuto5_Text3.activeSelf)
						{
							tutorialUiManager.tuto5_Text2.SetActive(false);
							tutorialUiManager.tuto5_Text3.SetActive(true);
							HoldPlayerRotation(player, 0);
							gameManager.uIManager.battleUiManager.shootButton.interactable = true;
							gameManager.uIManager.battleUiManager.attackButton.interactable = false;
						}
						else if (player.curStateName == PlayerState.End)
						{
							tutorialUiManager.tuto5_Text3.SetActive(false);
							HoldPlayerRotation(player, 0);
							tuto5finish_2 = true;
						}
						yield return 0;
					}
				}
				else if (player.curStateName == PlayerState.End)
				{
					tutorialUiManager.tuto5_5.SetActive(true);
					yield return new WaitForSeconds(1.5f);
					tutorialUiManager.tuto5_5_Text.SetActive(true);
					while (!tuto5_5finish)
					{
						if (gameManager.point)
						{
							finishWeaponTuto = true;
							tuto5_5finish = true;
							tutorialUiManager.tuto5_5.SetActive(false);
							Turn.ChangeStateIdle();
						}
						yield return 0;
					}
				}
				yield return 0;
				//gameManager.uIManager.battleUiManager.cancleButton.interactable = true;
			}
			else if (i == 2 && !finishWeaponTuto2)
			{
				if (!check)
				{
					ChangeTile(new Vector3(15.5f, 0, -14.5f));
					marker.SetActive(true);
					marker.transform.position = new Vector3(17.5f, 1, -14.5f);
					gameManager.uIManager.battleUiManager.moveButton.interactable = false;
					check = true;
				}
				if (player.curStateName == PlayerState.Idle)
				{
					gameManager.ChangeTargetPlayer(player.gameObject);
				}
				else if (player.curStateName == PlayerState.Move)
				{
					while (!tuto6finish)
					{
						tutorialUiManager.tuto6_Text.SetActive(true);
						gameManager.uIManager.battleUiManager.moveButton.interactable = false;
						if (gameManager.tilemapManager.ReturnTile(player.moveHelper.gameObject) == gameManager.tilemapManager.ReturnTile(new Vector3(17.5f, 0, -14.5f)))
						{
							gameManager.uIManager.battleUiManager.moveButton.interactable = true;
							if (player.curStateName == PlayerState.Action)
							{
								tuto6finish = true;
							}
							yield return 0;
						}
						yield return 0;
					}
				}
				else if (player.curStateName == PlayerState.Action)
				{
					marker.SetActive(false);
					HoldPlayerRotation(player, 0);
					tutorialUiManager.tuto6_Text.SetActive(false);
					tutorialUiManager.tuto6_Text_1.SetActive(true);

					gameManager.uIManager.battleUiManager.openDoorButton.interactable = true;
					if (gameManager.uIManager.battleUiManager.openDoorButton.interactable)
					{
						var tiles = groundTiles.Where((x) => x.tileArea == 3).ToList();
						foreach (var elem in tiles)
						{
							elem.gameObject.SetActive(true);
							var newPos = tilemap.WorldToCell(elem.transform.position);
							var m_3ftile = m_3f.GetInstantiatedObject(newPos);
							m_3ftile.SetActive(false);
						}
					}

				}
				else if (player.curStateName == PlayerState.End)
				{
					while (!tuto6finish2)
					{
						tutorialUiManager.tuto6_Text_1.SetActive(false);
						tutorialUiManager.tuto6_Text_2.SetActive(true);
						yield return new WaitForSeconds(2.5f);
						tutorialUiManager.tuto6_Text_2.SetActive(false);
						tutorialUiManager.tuto6.SetActive(true);
						yield return new WaitForSeconds(2.5f);
						tuto6finish2 = true;
						yield return 0;
					}
					while (!tuto6finish3)
					{
						if (tutorialUiManager.tuto6_image1.activeSelf)
						{
							tutorialUiManager.tuto6_image1.SetActive(false);
							tutorialUiManager.tuto6_image2.SetActive(true);
							yield return new WaitForSeconds(1.5f);
							tutorialUiManager.tuto6_Text_3.SetActive(true);
						}
						else if (tutorialUiManager.tuto6_Text_3.activeSelf)
						{
							while (gameManager.point)
							{
								var tiles = groundTiles.Where((x) => x.tileArea == 2).ToList();
								foreach (var elem in tiles)
								{
									var newPos = tilemap.WorldToCell(elem.transform.position);
									var m_3ftile = m_3f.GetInstantiatedObject(newPos);
									m_3ftile.SetActive(true);
								}
								tutorialUiManager.tuto6.SetActive(false);
								tutorialUiManager.tuto6_image2.SetActive(false);
								tutorialUiManager.tuto6_Text_3.SetActive(false);
								i = 3;
								number = 3;
								once = false;
								tuto6finish3 = true;
								finishWeaponTuto2 = true;
								Turn.ChangeStateIdle();
								yield return 0;
							}
						}
						yield return 0;
					}
				}
			}
			while (i == 3 && !tuto7finish3)
			{
				if (!check4)
				{
					//ChangeTile(new Vector3(16.5f, 0, -12.5f));
					marker.SetActive(true);
					marker.transform.position = new Vector3(17.5f, 1, -13.5f);
					gameManager.uIManager.battleUiManager.moveButton.interactable = false;
					gameManager.uIManager.battleUiManager.rescueButton.interactable = false;

					check4 = true;
					tutorialUiManager.turto7_Text.SetActive(true);
				}
				if (player.curStateName == PlayerState.Idle)
				{
					gameManager.ChangeTargetPlayer(player.gameObject);
				}
				else if (player.curStateName == PlayerState.Move)
				{
					if (gameManager.tilemapManager.ReturnTile(gameManager.targetPlayer.moveHelper.gameObject) == gameManager.tilemapManager.ReturnTile(new Vector3(17.5f, 0, -13.5f)))
					{
						gameManager.uIManager.battleUiManager.moveButton.interactable = true;
					}
					else
					{
						gameManager.uIManager.battleUiManager.moveButton.interactable = false;
					}
				}
				else if (player.curStateName == PlayerState.Action)
				{
					marker.SetActive(false);
					gameManager.uIManager.battleUiManager.moveButton.interactable = false;
					gameManager.uIManager.battleUiManager.attackButton.interactable = true;
					gameManager.uIManager.battleUiManager.weapon2Button.interactable = false;
					gameManager.uIManager.battleUiManager.attackButton.interactable = true;
					HoldPlayerRotation(player, 0);
				}
				else if(player.curStateName == PlayerState.End)
				{
					tutorialUiManager.turto7_Text.SetActive(false);
					tutorialUiManager.turto7_Text2.SetActive(true);
					while (tutorialUiManager.turto7_Text2.activeSelf )
					{
						while(!check3 && gameManager.uIManager.InfoUiScript.claimantInfo.gameObject.activeSelf)
						{
							//gameManager.uIManager.InfoUiScript.smallInfo.gameObject.SetActive(false);
							tutorialUiManager.turto7_Text2.SetActive(false);
							tutorialUiManager.turto7_Text3.SetActive(true);
							yield return new WaitForSeconds(3f);
							check3 = true;
							tuto7finish3 = true;
							check2 = false;
							Turn.ChangeStateIdle();
							yield return 0; 
						}
						yield return 0;
					}
				}
				yield return 0;
			}
			while (i ==3 && !tuto7finish4 && tuto7finish3)
			{
				if (!check5)
				{
					
					marker.SetActive(true);
					marker.transform.position = new Vector3(17.5f, 1, -12.5f);
					gameManager.uIManager.battleUiManager.moveButton.interactable = false;
					gameManager.uIManager.battleUiManager.rescueButton.interactable = false;

					check5 = true;
					tutorialUiManager.turto7_Text3.SetActive(false);
				}
				if (player.curStateName == PlayerState.Idle)
				{
					gameManager.ChangeTargetPlayer(player.gameObject);
				}
				else if (player.curStateName == PlayerState.Move)
				{
					tutorialUiManager.tuto_add1.SetActive(true);
					if (gameManager.tilemapManager.ReturnTile(gameManager.targetPlayer.moveHelper.gameObject) == gameManager.tilemapManager.ReturnTile(new Vector3(17.5f, 0, -12.5f)))
					{
						gameManager.uIManager.battleUiManager.moveButton.interactable = true;
					}
					else
					{
						gameManager.uIManager.battleUiManager.moveButton.interactable = false;
					}
				}
				else if (player.curStateName == PlayerState.Action)
				{
					if (!check6)
					{
						tutorialUiManager.tuto_add1.SetActive(false);
						tutorialUiManager.tuto_add2.SetActive(true);
						marker.SetActive(false);
						gameManager.uIManager.battleUiManager.moveButton.interactable = false;
						gameManager.uIManager.battleUiManager.itemButton.interactable = true;
						gameManager.uIManager.battleUiManager.attackButton.interactable = false;
						gameManager.uIManager.battleUiManager.useItemManager.useitemButton1.interactable = false;
						check6 = true;
					}
					while(gameManager.uIManager.battleUiManager.useItemManager.gameObject.activeSelf)
					{
						gameManager.uIManager.battleUiManager.itemButton.interactable = false;
						tutorialUiManager.tuto_add2.SetActive(false);
						tutorialUiManager.tuto_add3.SetActive(true);
						yield return 0;
					}
					//gameManager.uIManager.battleUiManager.useItemManager.cancleItemButton.interactable = false;
					while (gameManager.showMeleeRange)
					{
						tutorialUiManager.tuto_add3.SetActive(false);
						tutorialUiManager.turto8_Text.SetActive(true);
						yield return 0;
					}
					while (tutorialUiManager.turto8_Text.activeSelf && !gameManager.showMeleeRange)
					{
						tutorialUiManager.turto8_Text.SetActive(false);
						tutorialUiManager.turto8_Text2.SetActive(true);
						yield return 0;
					}
				}
				else if (player.curStateName == PlayerState.End)
				{
					tutorialUiManager.turto8_Text2.SetActive(false);
					i++;
					number++;
					Turn.ChangeStateIdle();
					tuto7finish4 = true;
				}
				yield return 0;
			}
			while (i == 4 && !tuto8finish)
			{
				if (player.curStateName == PlayerState.Idle)
				{
					gameManager.ChangeTargetPlayer(player.gameObject);
					player.SetState(PlayerState.Action);
				}
				else if (player.curStateName == PlayerState.Action)
				{
					tutorialUiManager.turto9_Text.SetActive(false);
					tutorialUiManager.turto9_Text2.SetActive(true);
					gameManager.uIManager.battleUiManager.rescueButton.interactable = true;
				}
				else if (player.curStateName == PlayerState.End)
				{
					tutorialUiManager.turto9_Text2.SetActive(false);
					tutorialUiManager.turto9_Text3.SetActive(true);
					yield return new WaitForSeconds(1f);

					AllTile.OnDestroy();
					Turn.OnDestroy();
					GameManager.instance.tutorial = false;

					SceneManager.LoadScene("TutorialEnd");
					tuto8finish = true;
				}
				yield return 0;
			}
			yield return 0;
		}
	}
}
