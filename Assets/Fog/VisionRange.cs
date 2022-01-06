using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionRange : MonoBehaviour
{
	public Queue<GroundTile> crossQueue;

	public List<GroundTile> crossResetQueue = new List<GroundTile>();
	private List<GroundTile> prevTileList = new List<GroundTile>();
	private List<int> visiableTileIndexList = new List<int>();

	public TilemapManager tilemapManager;
	public int vision = 5;
    private void Start()
    {
		tilemapManager = GameManager.instance.tilemapManager;
    }
    public List<GroundTile> CheackVision()
	{
		crossQueue = new Queue<GroundTile>();
		var tileObject = tilemapManager.ReturnTile(transform.position);
		crossQueue.Enqueue(tileObject);
		crossResetQueue.Add(tileObject);
		//visiableTileIndexList.Add(tileObject.index);

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

			//if (!ischeck)
			//{
			//	if (nextQueue.CheakVisionSum <= vision && /*!nextQueue.CheakVision &&*/ !crossResetQueue.Contains(nextQueue))
			//	{
			//		crossResetQueue.Add(nextQueue);
			//		//visiableTileIndexList.Add(nextQueue.index);
			//		crossQueue.Enqueue(nextQueue);
			//	}
			//}
			if (nextQueue.CheakVisionSum <= vision && /*!nextQueue.CheakVision &&*/ !crossResetQueue.Contains(nextQueue))
			{
				crossResetQueue.Add(nextQueue);
				//visiableTileIndexList.Add(nextQueue.index);
				crossQueue.Enqueue(nextQueue);
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

				//if (!ischeck)
				//{
				//	nextQueue.CheakVisionSum = curTile.CheakVisionSum + 1 + curQueue.tileSmokeValue / 10;
				//	if (nextQueue.CheakVisionSum <= vision && /*!nextQueue.CheakVision &&*/ !crossResetQueue.Contains(nextQueue))
				//	{
				//		crossResetQueue.Add(nextQueue);
				//		//visiableTileIndexList.Add(nextQueue.index);
				//		crossQueue.Enqueue(nextQueue);
				//	}
				//}
				nextQueue.CheakVisionSum = curTile.CheakVisionSum + 1 + curQueue.tileSmokeValue / 10;
				if (nextQueue.CheakVisionSum <= vision && /*!nextQueue.CheakVision &&*/ !crossResetQueue.Contains(nextQueue))
				{
					crossResetQueue.Add(nextQueue);
					//visiableTileIndexList.Add(nextQueue.index);
					crossQueue.Enqueue(nextQueue);
				}
			}
			curTile.CheakVision = true;
		}
		return crossResetQueue;
	}
	public void ResetTile()
	{
        foreach (var tile in crossResetQueue)
        {
			tile.CheakVision = false;
			tile.CheakVisionSum = 0;
        }
		crossResetQueue.Clear();
	}
	//public void VisionReset()
	//{
	//	for (int i = 0; i < crossResetQueue.Count; i++)
	//	{
	//		crossResetQueue[i].CheakVisionSum = 0;
	//		prevTileList.Add(crossResetQueue[i]);
	//	}
	//	crossResetQueue.Clear();
	//}
	//public void ResetTile()
	//{
	//	foreach (var tile in crossResetQueue)
	//	{
	//		if (prevTileList.Contains(tile))
	//		{
	//			prevTileList.Remove(tile);
	//		}
	//		tile.CheakVisionSum = 0;
	//	}
	//	foreach (var tile in prevTileList)
	//	{
	//		tile.CheakVision = false;
	//	}
	//}
	public void CheckPrevTile()
    {
        for (int i = 0; i < prevTileList.Count; i++)
        {
			prevTileList[i].CheckParticle();
		}
		prevTileList.Clear();
	}
}
