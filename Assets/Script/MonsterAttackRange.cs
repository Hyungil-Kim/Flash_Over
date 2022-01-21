using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MonsterAttackRange
{
	public Queue<GroundTile> crossQueue;
	public List<GroundTile> crossResetQueue = new List<GroundTile>();
	public List<GroundTile> CrossFloodFill(TilemapManager tilemapManager, GameObject targetObject, Color newColor, int range)
	{
		crossQueue = new Queue<GroundTile>();
		var tileObject = tilemapManager.ReturnTile(targetObject.transform.position);
		crossQueue.Enqueue(tileObject);
		crossResetQueue.Add(tileObject);
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
					if (nextQueue.checkSum <= range && !nextQueue.attackfloodFill && !crossResetQueue.Contains(nextQueue))
					{
						crossResetQueue.Add(nextQueue);
						crossQueue.Enqueue(nextQueue);
					}
				}
			}
			curTile.attackfloodFill = true;
		}
		return crossResetQueue;
	}
	public void CrossResetTile(Tilemap tilemap,List<GroundTile> resetList)
	{
		for(int i =0; i < resetList.Count;i++)
		{
			var curQueue = resetList[i];
			var curTile = tilemap.GetInstantiatedObject(new Vector3Int(curQueue.cellpos.x, curQueue.cellpos.y, 0)).GetComponent<GroundTile>();
			for(int k =0; k < curQueue.nextTileList.Count;k++)
			{
				curTile.nextTileList[k].ResetExceptColor();//º¸·ù
			}
			curTile.ResetExceptColor();
		}
	}

}
