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
	public void Start()
	{
		var gameManager = GameManager.instance;
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
		yield break;
	}
	public IEnumerator CloseDoor()
	{
		while (transform.eulerAngles.y >= 2)
		{
			transform.Rotate(0, -2, 0);
			yield return new WaitForSeconds(0.01f);
		}
		parentTile.isWall = true;
		yield break;
	}
	public void Update()
	{
		Debug.Log(transform.eulerAngles.y);
	}
}
