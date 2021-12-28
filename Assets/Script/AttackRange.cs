using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public enum Move
{
	right = 90,
	up = 0,
	left = 270,
	down = 180,
}
public class AttackRange
{
	public Queue<GroundTile> crossQueue;
	public Queue<GroundTile> crossResetQueue = new Queue<GroundTile>();
	public Queue<GroundTile> LineQueue;
	public Queue<GroundTile> LineResetQueue = new Queue<GroundTile>();
	public Queue<GroundTile> TriQueue;
	public Queue<GroundTile> TriResetQueue = new Queue<GroundTile>();

	public void CrossFloodFill(TilemapManager tilemapManager, GameObject targetObject, Color newColor, int range)
	{
		crossQueue = new Queue<GroundTile>();
		var tileObject =tilemapManager.ReturnTile(targetObject.transform.position);
		crossQueue.Enqueue(tileObject);
		crossResetQueue.Enqueue(tileObject);
		while (crossQueue.Count > 0)
		{

			var curQueue = crossQueue.Peek();
			var curTile = tilemapManager.tilemap.GetInstantiatedObject(new Vector3Int(curQueue.cellpos.x, curQueue.cellpos.y, 0)).GetComponent<GroundTile>();

			if (!curTile.isWall)
			{
				curTile.SetTileColor(newColor);
			}
			crossQueue.Dequeue();
			for (int dir = 0; dir < curQueue.nextTileList.Count; dir++)
			{
				var nextQueue = curQueue.nextTileList[dir];
				var ischeck = nextQueue.attackfloodFill;

				if (!ischeck)
				{
					nextQueue.checkSum = curTile.checkSum + 1;
					if (nextQueue.checkSum <= range && !nextQueue.attackfloodFill)
					{
						crossResetQueue.Enqueue(nextQueue);
						crossQueue.Enqueue(nextQueue);
					}
				}
			}
			curTile.attackfloodFill = true;
		}
	}
	public void CrossResetTile(Tilemap tilemap)
	{
		while (crossResetQueue.Count > 0)
		{
			var curQueue = crossResetQueue.Peek();
			var curTile = tilemap.GetInstantiatedObject(new Vector3Int(curQueue.cellpos.x, curQueue.cellpos.y, 0)).GetComponent<GroundTile>();
			curTile.Reset();
			crossResetQueue.Dequeue();
		}
		crossResetQueue.Clear();
	}

	public void LineFloodFill(TilemapManager tilemapManager, GameObject targetObject, Color newColor, int range)
	{
		LineQueue = new Queue<GroundTile>();
		var tileObject = tilemapManager.ReturnTile(targetObject.transform.position);
		LineQueue.Enqueue(tileObject);
		LineResetQueue.Enqueue(tileObject);
		while (LineQueue.Count > 0)
		{
			var curQueue = LineQueue.Peek();
			var curTile = tilemapManager.tilemap.GetInstantiatedObject(new Vector3Int(curQueue.cellpos.x, curQueue.cellpos.y, 0)).GetComponent<GroundTile>();

			if (!curTile.isWall)
			{
				curTile.SetTileColor(newColor);
			}
			LineQueue.Dequeue();
			for (int dir = 0; dir < curQueue.nextTileList.Count; dir++)
			{
				var nextQueue = curQueue.nextTileList[dir];
				var ischeck = nextQueue.attackfloodFill;

				if (!ischeck)
				{
					nextQueue.checkSum = curTile.checkSum + 1;
					if (nextQueue.checkSum <= range &&
					   (tileObject.transform.position.x == nextQueue.transform.position.x ^ tileObject.transform.position.z == nextQueue.transform.position.z) &&
						!nextQueue.attackfloodFill)
					{
						LineCheckLookPos(curQueue, nextQueue, (Move)targetObject.transform.rotation.eulerAngles.y); 
					}
				}
			}
			curTile.attackfloodFill = true;
		}
	}
	public void LineResetTile(Tilemap tilemap)
	{
			while (LineResetQueue.Count > 0)
			{
				var curQueue = LineResetQueue.Peek();
				var curTile = tilemap.GetInstantiatedObject(new Vector3Int(curQueue.cellpos.x, curQueue.cellpos.y, 0)).GetComponent<GroundTile>();
				curTile.Reset();
				LineResetQueue.Dequeue();
			}
	}

	public void TriFloodFill(TilemapManager tilemapManager, GameObject targetObject, Color newColor, int range)
	{
		TriQueue = new Queue<GroundTile>();
		var tileObject = tilemapManager.ReturnTile(targetObject.transform.position);
		TriQueue.Enqueue(tileObject);
		TriResetQueue.Enqueue(tileObject);
		while (TriQueue.Count > 0)
		{
			var curQueue = TriQueue.Peek();
			var curTile = tilemapManager.tilemap.GetInstantiatedObject(new Vector3Int(curQueue.cellpos.x, curQueue.cellpos.y, 0)).GetComponent<GroundTile>();

			if (!curTile.isWall)
			{
				curTile.SetTileColor(newColor);
			}
			TriQueue.Dequeue();
			for (int dir = 0; dir < curQueue.nextTileList.Count; dir++)
			{
				var nextQueue = curQueue.nextTileList[dir];
				var ischeck = nextQueue.attackfloodFill;

				if (!ischeck)
				{
					nextQueue.checkSum = curTile.checkSum + 1;
					var squrCondition = nextQueue.transform.position.x <= tileObject.transform.position.x + range && nextQueue.transform.position.x >= tileObject.transform.position.x - range &&
						 nextQueue.transform.position.z <= tileObject.transform.position.z + range && nextQueue.transform.position.z >= tileObject.transform.position.z - range;
					var curLineCondition = tileObject.transform.position.x == curQueue.transform.position.x ^ tileObject.transform.position.z == curQueue.transform.position.z;
					var nextLineCondition = tileObject.transform.position.x == nextQueue.transform.position.x ^ tileObject.transform.position.z == nextQueue.transform.position.z;

					if (squrCondition && (curLineCondition || curQueue.checkSum == 0) && !nextQueue.attackfloodFill)
					{
						if (!(curQueue.checkSum == 1 && !nextLineCondition))
						{
							TriCheckLookPos(curQueue, nextQueue, (Move)targetObject.transform.rotation.eulerAngles.y);
						}
					}
				}
			}

			curTile.attackfloodFill = true;
		}
	}
	public void TriResetTile(Tilemap tilemap)
	{
			while (TriResetQueue.Count > 0)
			{
				var curQueue = TriResetQueue.Peek();
				var curTile = tilemap.GetInstantiatedObject(new Vector3Int(curQueue.cellpos.x, curQueue.cellpos.y, 0)).GetComponent<GroundTile>();
				curTile.Reset();
				TriResetQueue.Dequeue();
			}
	}
	private void LineCheckLookPos(GroundTile curQueue, GroundTile nextQueue, Move lookPos)
	{
		if (curQueue.checkSum == 0)
		{
			switch (lookPos)
			{
				case Move.up:
					if (curQueue.transform.position.z + 1 == nextQueue.transform.position.z)
					{
						LineQueue.Enqueue(nextQueue);
						LineResetQueue.Enqueue(nextQueue);
					}
					break;
				case Move.right:
					if (curQueue.transform.position.x + 1 == nextQueue.transform.position.x)
					{
						LineQueue.Enqueue(nextQueue);
						LineResetQueue.Enqueue(nextQueue);
					}
					break;
				case Move.down:
					if (curQueue.transform.position.z - 1 == nextQueue.transform.position.z)
					{
						LineQueue.Enqueue(nextQueue);
						LineResetQueue.Enqueue(nextQueue);
					}
					break;
				case Move.left:
					if (curQueue.transform.position.x - 1 == nextQueue.transform.position.x)
					{
						LineQueue.Enqueue(nextQueue);
						LineResetQueue.Enqueue(nextQueue);
					}
					break;
			}
		}
		else
		{
			LineQueue.Enqueue(nextQueue);
			LineResetQueue.Enqueue(nextQueue);
		}
	}
	private void TriCheckLookPos(GroundTile curQueue, GroundTile nextQueue, Move lookPos)
	{
		if (curQueue.checkSum == 0)
		{
			switch (lookPos)
			{
				case Move.up:
					if (curQueue.transform.position.z + 1 == nextQueue.transform.position.z)
					{
						TriQueue.Enqueue(nextQueue);
						TriResetQueue.Enqueue(nextQueue);
					}
					break;
				case Move.right:
					if (curQueue.transform.position.x + 1 == nextQueue.transform.position.x)
					{
						TriQueue.Enqueue(nextQueue);
						TriResetQueue.Enqueue(nextQueue);
					}
					break;
				case Move.down:
					if (curQueue.transform.position.z - 1 == nextQueue.transform.position.z)
					{
						TriQueue.Enqueue(nextQueue);
						TriResetQueue.Enqueue(nextQueue);
					}
					break;
				case Move.left:
					if (curQueue.transform.position.x - 1 == nextQueue.transform.position.x)
					{
						TriQueue.Enqueue(nextQueue);
						TriResetQueue.Enqueue(nextQueue);
					}
					break;
			}
		}
		else
		{
			TriQueue.Enqueue(nextQueue);
			TriResetQueue.Enqueue(nextQueue);
		}
	}


	public void Attack(GameObject attackObject,TilemapManager tilemapManager,int type, int range, Color color)
	{
		switch (type)
		{
			case 0:
				CrossFloodFill(tilemapManager, attackObject, color, range);
				break;
			case 1:
				LineFloodFill(tilemapManager, attackObject, color, range);
				break;
			case 2:
				TriFloodFill(tilemapManager, attackObject, color, range);
				break;
		}
	}
	public void AttackReset(Tilemap tilemap, int type)
	{
		switch (type)
		{
			case 0:
				CrossResetTile(tilemap);
				break;
			case 1:
				LineResetTile(tilemap);
				break;
			case 2:
				TriResetTile(tilemap);
				break;
		}
	}
}
