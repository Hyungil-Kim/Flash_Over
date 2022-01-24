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
	public void Start()
	{
		gameManager = GameManager.instance;
		CheckDoor();
	}
	public IEnumerator OpenDoor()
	{
		while (transform.eulerAngles.y < 90)
		{
			transform.Rotate(0, 2, 0);
			yield return new WaitForSeconds(0.01f);
		}
		parentTile.isWall = false;
		CheckDoor();
		gameManager.targetPlayer.SetState(PlayerState.End);
		yield break;
	}
	public IEnumerator CloseDoor()
	{
		var gameManager = GameManager.instance;
		while (transform.eulerAngles.y > 2)
		{
			transform.Rotate(0, -2, 0);
			yield return new WaitForSeconds(0.01f);
		}
		parentTile.isWall = true;
		CheckDoor();
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
