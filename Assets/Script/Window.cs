using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
	public GameManager gameManager;
	public bool open = true;
	public void Start()
	{
		gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
		Turn.windows.Add(this);
	}

	public void WindowTurn()
	{
		if (open)
		{
			var curTile = gameManager.tilemapManager.ReturnTile(this.gameObject);
			curTile.tileSaveSmokeValue -= 100;
			curTile.tileSaveSmokeValue = curTile.tileSaveSmokeValue < 0 ? 0 : curTile.tileSaveSmokeValue;
			
			foreach(var elem in curTile.nextTileList)
			{
				elem.tileSaveSmokeValue -= 50;
				elem.tileSaveSmokeValue = curTile.tileSaveSmokeValue < 0 ? 0 : curTile.tileSaveSmokeValue;
			}
			
		}
	}
}
