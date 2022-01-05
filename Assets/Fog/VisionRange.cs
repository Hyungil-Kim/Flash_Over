using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionRange : MonoBehaviour
{
	public Queue<GroundTile> crossQueue;

	public List<GroundTile> crossResetQueue = new List<GroundTile>();
	private List<GroundTile> prevTileList = new List<GroundTile>();
	public TilemapManager tilemapManager;
	public int vision = 5;

    public List<GroundTile> CheackVision()
	{
		crossQueue = new Queue<GroundTile>();
		var tileObject = tilemapManager.ReturnTile(transform.position);
		crossQueue.Enqueue(tileObject);
		crossResetQueue.Add(tileObject);


		var curQueue = crossQueue.Peek();
		var curTile = tilemapManager.tilemap.GetInstantiatedObject(new Vector3Int(curQueue.cellpos.x, curQueue.cellpos.y, 0)).GetComponent<GroundTile>();

		if (!curTile.isWall)
		{

		}
		crossQueue.Dequeue();
		var nextVisionList = curQueue.SetNextVision(tileObject);
		var listCount = nextVisionList.Count;
		for (int dir = 0; dir < listCount; dir++)
		{
			var nextQueue = nextVisionList[dir];
			var ischeck = nextQueue.CheakVision;

			if (!ischeck)
			{
				if (nextQueue.CheakVisionSum <= vision && !nextQueue.CheakVision && !crossResetQueue.Contains(nextQueue))
				{
					crossResetQueue.Add(nextQueue);
					crossQueue.Enqueue(nextQueue);
				}
			}
		}
		curTile.CheakVision = true;



		while (crossQueue.Count > 0)
		{

			curQueue = crossQueue.Peek();
			curTile = tilemapManager.tilemap.GetInstantiatedObject(new Vector3Int(curQueue.cellpos.x, curQueue.cellpos.y, 0)).GetComponent<GroundTile>();

			if (!curTile.isWall)
			{
			}
			crossQueue.Dequeue();
			nextVisionList = curQueue.SetNextVision(tileObject);
			listCount = nextVisionList.Count;
			for (int dir = 0; dir < listCount; dir++)
			{
				var nextQueue = nextVisionList[dir];
				var ischeck = nextQueue.CheakVision;

				if (!ischeck)
				{
					nextQueue.CheakVisionSum = curTile.CheakVisionSum + 1 + curQueue.tileSmokeValue / 10;
					if (nextQueue.CheakVisionSum <= vision && !nextQueue.CheakVision && !crossResetQueue.Contains(nextQueue))
					{
						crossResetQueue.Add(nextQueue);
						crossQueue.Enqueue(nextQueue);
					}
				}
			}
			curTile.CheakVision = true;
		}
		return crossResetQueue;
	}
	public void ResetTile()
	{
		for (int i = 0; i < crossResetQueue.Count; i++)
		{
			crossResetQueue[i].CheakVision = false;
			crossResetQueue[i].CheakVisionSum = 0;
			prevTileList.Add(crossResetQueue[i]);
		}
		crossResetQueue.Clear();
	}
	public void CheckPrevTile()
    {
        for (int i = 0; i < prevTileList.Count; i++)
        {
			prevTileList[i].CheckParticle();
		}
		prevTileList.Clear();
	}
}