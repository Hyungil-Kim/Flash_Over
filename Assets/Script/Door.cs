using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DoorState
{
	Close,
	Open
}
public class Door : MonoBehaviour
{
	public DoorState curDoorState;
	public GroundTile parentTile;
	private GameManager gameManager;
	public void Start()
	{
		gameManager = GameManager.instance;
		parentTile = gameManager.tilemapManager.ReturnTile(this.gameObject);
		if (curDoorState == DoorState.Open)
		{
			parentTile.isWall = false;
		}
		else
		{
			parentTile.isWall = true;
		}
	}
	public IEnumerator OpenDoor()
	{
		while (transform.eulerAngles.y <= 90)
		{
			transform.Rotate(0, 2, 0);
			yield return new WaitForSeconds(0.01f);
		}
		parentTile.isWall = false;
		gameManager.targetPlayer.curStateName = PlayerState.End;
		yield break;
	}
	public IEnumerator CloseDoor()
	{
		var gameManager = GameManager.instance;
		while (transform.eulerAngles.y >= 2)
		{
			transform.Rotate(0, -2, 0);
			yield return new WaitForSeconds(0.01f);
		}
		parentTile.isWall = true;
		gameManager.targetPlayer.curStateName = PlayerState.End;
		yield break;
	}
}
