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
	public Transform parentPos;
	public GroundTile parentTile;
	private GameManager gameManager;
	private int Count = 0;
	public void Start()
	{
		gameManager = GameManager.instance;
		CheckDoor();
	}
	public IEnumerator OpenDoor()
	{
		parentTile.isWall = false;
		parentTile.isDoor = false;
		while (Count <= 90)
		{
			transform.Rotate(0, 2, 0);
			Count += 2;
			yield return new WaitForSeconds(0.01f);
		}
		CheckDoor();
		parentTile.isDoor = true;
		parentTile.isWall = true;
		this.curDoorState = DoorState.Open;
		AllTileMesh.instance.UpdateFog();
		yield break;
	}
	public IEnumerator CloseDoor()
	{
		var gameManager = GameManager.instance;
		parentTile.isWall = false;
		parentTile.isDoor = false;
		while (Count >= 2)
		{
			transform.Rotate(0, -2, 0);
			Count -= 2;
			yield return new WaitForSeconds(0.01f);
		}
		CheckDoor();
		parentTile.isDoor = true;
		parentTile.isWall = true;
		this.curDoorState = DoorState.Close;
		gameManager.targetPlayer.SetState(PlayerState.End);
		yield break;
	}
	public void CheckDoor()
	{
		parentTile = gameManager.tilemapManager.ReturnTile(parentPos.position);
		if (curDoorState == DoorState.Open)
		{
			parentTile.isWall = false;
		}
		else
		{
			parentTile.isWall = true;
		}
	}
}
