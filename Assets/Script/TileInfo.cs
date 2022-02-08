using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TileInfo : MonoBehaviour
{
	public GameManager gameManager;
	public TextMeshProUGUI levelNum;
	public TextMeshProUGUI fireHpNum;
	public Slider fireHpSlider;
	public TextMeshProUGUI fireMidDamage;
	public TextMeshProUGUI fireAroundDamage;
	public TextMeshProUGUI tileName;
	public TextMeshProUGUI isweat;
	public TextMeshProUGUI tileHp;
	public Slider tileHpSlider;
	public TextMeshProUGUI tileSmokeLevel;
	public TextMeshProUGUI tileSmokeValue;
	public TextMeshProUGUI tileInterruptVision;
	public List<Obstacle> obstacles;
	public GameObject[] objectsPanel;
	public TextMeshProUGUI[] obstacleName;
	public TextMeshProUGUI[] obstacleHp;

	public Image fireHpSliderImage;
	public Image fireHpSliderbackGroundImage;

	public GameObject objPanel;
	public GameObject firePanel;
	public GameObject smokePanel;
	public bool on;

	public Button changePlayerInfo;
	private void Awake()
	{
		gameManager = GameManager.instance;
	}
	public void OnEnable()
	{
		gameManager.uIManager.battleUiManager.AllButtonOff();
		on = true;
	}
	public void OnDisable()
	{
		if(gameManager.targetPlayer != null)
		{
			switch (gameManager.targetPlayer.curStateName)
			{
				case PlayerState.Idle:
					break;
				case PlayerState.Move:
					gameManager.uIManager.battleUiManager.moveButton.gameObject.SetActive(true);
					gameManager.uIManager.battleUiManager.cancleButton.gameObject.SetActive(true);
					gameManager.uIManager.battleUiManager.selectNextPlayer.gameObject.SetActive(true);
					break;
				case PlayerState.Action:
					gameManager.targetPlayer.SetState(PlayerState.Action);
					break;
				case PlayerState.End:
					break;
			}
		}
	}
	public void UpdateTileInfo()
	{
		if (gameManager.targetTile !=null)
		{
			if (gameManager.targetTile.tileIsFire)
			{
				firePanel.SetActive(true);
				var fire = gameManager.targetTile.GetComponentInChildren<Fire>();
				levelNum.text = (fire.fireLevel+1).ToString();
				fireHpNum.text = fire.fireHp.ToString();
				fireMidDamage.text = fire.data.dmg.ToString();
				fireAroundDamage.text = (fire.data.dmg * 0.5f).ToString();
				fireHpSlider.value = ((float)fire.fireHp - (float)fire.data.minhp) / ((float)fire.data.maxhp - (float)fire.data.minhp);
				if(fire.fireLevel == 0)
				{
					fireHpSliderImage.color = Color.red;
					fireHpSliderbackGroundImage.color = Color.black;
				}
				else if (fire.fireLevel == 1)
				{
					fireHpSliderImage.color = Color.yellow;
					fireHpSliderbackGroundImage.color = Color.red;
				}
				else if (fire.fireLevel == 2)
				{
					fireHpSliderImage.color = Color.green;
					fireHpSliderbackGroundImage.color = Color.yellow;
				}
			}
			else
			{
				firePanel.SetActive(false);
			}
			tileName.text = gameManager.targetTile.name.ToString();
			if(gameManager.targetTile.tileIsWeat)
			{
				isweat.text = "¡•¿Ω";
			}
			else if(gameManager.targetTile.tileIsFire)
			{
				isweat.text = "∫“≈Ω";
			}
			else 
			{
				isweat.text = "∫∏≈Î";
			}
			tileHp.text = gameManager.targetTile.tileHp.ToString();
			tileHpSlider.value = (float)gameManager.targetTile.tileHp;
			if (gameManager.targetTile.tileIsSmoke)
			{
				smokePanel.SetActive(true);
				tileSmokeLevel.text = gameManager.targetTile.smokePrefab.GetComponent<Smoke>().level.ToString();
				tileSmokeValue.text = gameManager.targetTile.tileSmokeValue.ToString();
				tileInterruptVision.text = "0";
			}
			else
			{
				smokePanel.SetActive(false);
			}
			obstacles.Clear();
			for(int i =0; i < gameManager.targetTile.fillList.Count;i++)
			{
				if(gameManager.targetTile.fillList[i].tag == "Obstacle")
				{
					obstacles.Add(gameManager.targetTile.fillList[i].GetComponent<Obstacle>());
				}
			}
			switch (obstacles.Count)
			{
				case 0:
					objPanel.SetActive(false);
					break;
				case 1:
					objPanel.SetActive(true);
					objectsPanel[1].SetActive(false);
					break;
				case 2:
					objPanel.SetActive(true);
					objectsPanel[1].SetActive(true);
					break;
				default:
					objPanel.SetActive(false);
					break;
			}

			for (int i =0; i<obstacles.Count;i++)
			{
				obstacleName[i].text = obstacles[i].name;
				obstacleHp[i].text = obstacles[i].hp.ToString();
			}
			
		}
	}
	public void ChangePlayerInfo()
	{
		foreach ( var elem in gameManager.target.GetComponent<GroundTile>().fillList)
		{
			if (elem.tag == "Player")
			{
				gameManager.targetPlayer = elem.GetComponent<Player>();
			}
		}
		gameManager.uIManager.InfoUiScript.charaterInfo.gameObject.SetActive(true);
		gameObject.SetActive(false);
	}
}
