using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
public class Tutorial : MonoBehaviour
{
	public GameManager gameManager;
	//public List<GroundTile> groundTiles;
	public Tilemap tilemap;
	public Tilemap m_2f;
	public NewCustomRuleTile2 groundTile;
	public NewCustomRuleTile2 fireTile;
	public NewCustomRuleTile2 dummyTile;

	public Button button;
	public void Awake()
	{
		gameManager = GameManager.instance;
	}

	public void ChangeTile(Vector3 position)
	{
		var newPos = tilemap.WorldToCell(position);
		var tile = tilemap.GetTile(newPos);
		var m_2ftile = m_2f.GetTile(newPos);
		Debug.Log(tile);
		Debug.Log(m_2f);
		//if(tile == fireTile || tile == groundTile)
		//{
		//	tile = null;
		//	m_2ftile = dummyTile;
		//}
		//else if(m_2f == dummyTile)
		//{
		//	m_2ftile = null;
		//	tile = groundTile;
		//}
	}
	public void ClickButton()
	{
		ChangeTile(new Vector3(15.5f,0f,-14.5f));
	}
}
