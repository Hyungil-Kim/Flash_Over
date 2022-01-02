using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;


public class FloodFillAlgorism
{
	public GroundTile tileObject;
	public Vector3Int cellPos;
	private Queue<GroundTile> objectQueue = new Queue<GroundTile>();
	private Queue<GroundTile> resetQueue = new Queue<GroundTile>();
	public List<GroundTile> returnList = new List<GroundTile>();
	

	public void FloodFill(Tilemap tilemap, Vector3 startPos, Color newColor, int speed)
	{
		cellPos = tilemap.WorldToCell(startPos);
		tileObject = tilemap.GetInstantiatedObject(cellPos).GetComponent<GroundTile>();
		objectQueue.Enqueue(tileObject);
		resetQueue.Enqueue(tileObject);
		while (objectQueue.Count > 0)
		{
			
			var curQueue = objectQueue.Peek();
			var curTile = tilemap.GetInstantiatedObject(new Vector3Int(curQueue.cellpos.x, curQueue.cellpos.y, 0)).GetComponent<GroundTile>();

			if (!curTile.isWall)
			{
				curTile.SetTileColor(newColor);
			}
			objectQueue.Dequeue();
			for (int dir = 0; dir < curQueue.nextTileList.Count; dir++)
			{
				var nextQueue = curQueue.nextTileList[dir];
				var ischeck = nextQueue.movefloodFill;
				var nextObject = nextQueue.GetComponent<GroundTile>().gameObject;
				if (!ischeck && nextObject.activeSelf)
				{
					nextQueue.checkSum = curTile.GetComponent<GroundTile>().checkSum +1;
					if (!resetQueue.Contains(nextQueue))
					{
						resetQueue.Enqueue(nextQueue);
					}
					if (nextQueue.checkSum <= speed  && !nextQueue.isWall)
					{
						objectQueue.Enqueue(nextQueue);
					}

				}
			}
				curTile.movefloodFill = true;
		}
	}
	public void FloodFillExceptColor(Tilemap tilemap, Vector3 startPos, Color newColor,Color pathColor, int speed, List<Vector3> movelist)
	{
		cellPos = tilemap.WorldToCell(startPos);
		tileObject = tilemap.GetInstantiatedObject(cellPos).GetComponent<GroundTile>();
		objectQueue.Enqueue(tileObject);
		resetQueue.Enqueue(tileObject);
		while (objectQueue.Count > 0)
		{
			var curQueue = objectQueue.Peek();
			var curTile = tilemap.GetInstantiatedObject(new Vector3Int(curQueue.cellpos.x, curQueue.cellpos.y, 0)).GetComponent<GroundTile>();

			if (!curTile.isWall)
			{
				if(movelist.Contains(curTile.transform.position))
				{
					curTile.SetTileColor(pathColor);
				}
				else
				{
					curTile.SetTileColor(newColor);
				}

			}
			objectQueue.Dequeue();
			for (int dir = 0; dir < curQueue.nextTileList.Count; dir++)
			{
				var nextQueue = curQueue.nextTileList[dir];
				var ischeck = nextQueue.movefloodFill;
				var nextObject = nextQueue.GetComponent<GroundTile>().gameObject;
				if (!ischeck && nextObject.activeSelf)
				{
					nextQueue.checkSum = curTile.GetComponent<GroundTile>().checkSum + 1;
					if (!resetQueue.Contains(nextQueue))
					{
						resetQueue.Enqueue(nextQueue);
					}
					if (nextQueue.checkSum <= speed && !nextQueue.isWall)
					{
						objectQueue.Enqueue(nextQueue);
					}
				}
			}
			curTile.movefloodFill = true;
		}
	}
	public Queue<GroundTile> ReturnResetQueue()
	{
		return resetQueue;
	}
	public void ResetTile(Tilemap tilemap)
	{
		var saveQueue = new Queue<GroundTile>(resetQueue);
		while (saveQueue.Count > 0)
		{
			var curQueue = saveQueue.Peek();
			var curTile = tilemap.GetInstantiatedObject(new Vector3Int(curQueue.cellpos.x, curQueue.cellpos.y, 0));
			curTile.GetComponent<GroundTile>().Reset();
			saveQueue.Dequeue();
		}
	}

	public void ResetTileExecptPath(Tilemap tilemap,List<Vector3> moveList,Color pathColor)
	{
		var saveQueue = new Queue<GroundTile>(resetQueue);
		while (saveQueue.Count > 0)
		{
			var curQueue = saveQueue.Peek();
			var curTile = tilemap.GetInstantiatedObject(new Vector3Int(curQueue.cellpos.x, curQueue.cellpos.y, 0)).GetComponent<GroundTile>();
			if (moveList.Contains(curTile.transform.position))
			{
				curTile.ResetExceptColor();		
			}
			else
			{
				curTile.Reset();
			}
			saveQueue.Dequeue();
		}
	}
}
