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
	public Button changeObj;
	public TextMeshProUGUI tileName;
	public TextMeshProUGUI isweat;
	public TextMeshProUGUI tileHp;
	public TextMeshProUGUI tileSmokeLevel;
	public TextMeshProUGUI tileSmokeValue;
	public TextMeshProUGUI tileInterruptVision;
	public List<Obstacle> obstacles;
	public GameObject[] objectsPanel;
	public TextMeshProUGUI[] obstacleName;
	public TextMeshProUGUI[] obstacleHp;

	public GameObject objPanel;
	public GameObject tilePanel;

	public void Start()
	{
		gameManager = GameManager.instance;
	}
	public void UpdateTileInfo()
	{
		if (gameManager.targetTile !=null)
		{
			if (gameManager.targetTile.tileIsFire)
			{
				levelNum.text = gameManager.targetTile.GetComponentInChildren<Fire>().fireLevel.ToString();
				fireHpNum.text = gameManager.targetTile.GetComponentInChildren<Fire>().fireHp.ToString();
				fireMidDamage.text = gameManager.targetTile.GetComponentInChildren<Fire>().data.dmg.ToString();
				fireAroundDamage.text = (gameManager.targetTile.GetComponentInChildren<Fire>().data.dmg * 0.5f).ToString();
			}
			tileName.text = gameManager.targetTile.name.ToString();
			if(gameManager.targetTile.tileIsWeat)
			{
				isweat.text = "¡•¿Ω";
			}
			else
			{
				isweat.text = "∫∏≈Î";
			}
			tileHp.text = gameManager.targetTile.tileHp.ToString();
			if (gameManager.targetTile.tileIsSmoke)
			{
				tileSmokeLevel.text = gameManager.targetTile.smokePrefab.GetComponent<Smoke>().level.ToString();
				tileSmokeValue.text = gameManager.targetTile.tileSmokeValue.ToString();
				tileInterruptVision.text = "0";
			}
			obstacles.Clear();
			for(int i =0; i < gameManager.targetTile.fillList.Count;i++)
			{
				if(gameManager.targetTile.fillList[i].tag == "Obstacle")
				{
					obstacles.Add(gameManager.targetTile.fillList[i].GetComponent<Obstacle>());
				}
			}

			for(int i =0; i<obstacles.Count;i++)
			{
				obstacleName[i].text = obstacles[i].name;
				obstacleHp[i].text = obstacles[i].hp.ToString();
			}
		}
	}
	public void ClickObjButton()
	{
		objPanel.SetActive(false);
		tilePanel.SetActive(true);
	}
	public void ClickTileButton()
	{
		objPanel.SetActive(true);
		tilePanel.SetActive(false);
	}
}
