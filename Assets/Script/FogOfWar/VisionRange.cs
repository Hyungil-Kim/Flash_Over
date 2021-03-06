using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionRange : MonoBehaviour
{
	public Queue<GroundTile> crossQueue;

	public List<GroundTile> crossResetQueue = new List<GroundTile>();
	private List<GroundTile> prevTileList = new List<GroundTile>();
	private List<int> visiableTileIndexList = new List<int>();
	public GameManager gameManager;
	public TilemapManager tilemapManager;
	public int vision = 5;
    private void Awake()
    {
		gameManager = GameManager.instance;
		tilemapManager = gameManager.tilemapManager;
    }
    public List<GroundTile> CheackVision()
	{
		crossQueue = new Queue<GroundTile>();
		//Debug.Log(tilemapManager);
		var tileObject = tilemapManager.ReturnTile(transform.position);
		crossQueue.Enqueue(tileObject);
		crossResetQueue.Add(tileObject);
		//visiableTileIndexList.Add(tileObject.index);

		var curQueue = crossQueue.Peek();
		var curTile = tilemapManager.tilemap.GetInstantiatedObject(new Vector3Int(curQueue.cellpos.x, curQueue.cellpos.y, 0)).GetComponent<GroundTile>();

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
			if (nextQueue.CheakVisionSum <= vision && /*!nextQueue.CheakVision &&*/ !crossResetQueue.Contains(nextQueue) /*&& !nextQueue.isWall*/)
			{
				crossResetQueue.Add(nextQueue);
				//visiableTileIndexList.Add(nextQueue.index);
				crossQueue.Enqueue(nextQueue);
			}
		}
		curTile.CheakVision = true;
		curTile.obstacleVision = true;


		while (crossQueue.Count > 0)
		{

			curQueue = crossQueue.Peek();
			curTile = tilemapManager.tilemap.GetInstantiatedObject(new Vector3Int(curQueue.cellpos.x, curQueue.cellpos.y, 0)).GetComponent<GroundTile>();
			bool isWall = false;
			//if (curTile.isWall)
			//{
			//	curTile.CheakVision = true;
			//	//isWall = true;
			//	crossQueue.Dequeue();
			//	continue;
			//}
			crossQueue.Dequeue();
			nextVisionList = curQueue.SetNextVision(tileObject, isWall);
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
				var smoke = curQueue.GetComponentInChildren<Smoke>();
				if (smoke != null)
				{
					nextQueue.CheakVisionSum = curTile.CheakVisionSum + 1 + /*curQueue.tileSmokeValue / 10*/ smoke.data.decreasevision;
				}
				else
                {
					nextQueue.CheakVisionSum = curTile.CheakVisionSum + 1;
				}
				if (nextQueue.CheakVisionSum <= vision && /*!nextQueue.CheakVision &&*/ !crossResetQueue.Contains(nextQueue)/* && !isWall*//*&& !nextQueue.isWall*/)
				{
					crossResetQueue.Add(nextQueue);
					//visiableTileIndexList.Add(nextQueue.index);
					crossQueue.Enqueue(nextQueue);
				}
				else if(nextQueue.CheakVisionSum <= vision && !crossResetQueue.Contains(nextQueue) && isWall && nextQueue.isWall)
                {
					
					crossResetQueue.Add(nextQueue);
					//visiableTileIndexList.Add(nextQueue.index);
					crossQueue.Enqueue(nextQueue);
					
				}
			}
			curTile.CheakVision = true;
		}
		CheckBuff();
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
	public void CheckBuff()
	{
		bool rangeClaimant = false;
		bool rangeFireman = false;
		bool rangeFire = false;
		List<Player> playerList = new List<Player>();

		foreach (var tile in crossResetQueue)
		{
			if (tile.isPlayer)
			{
				playerList.Add(tile.OnPlayer);
			}
		}
		foreach (var tile in crossResetQueue)
		{
			if (tile.tileIsClaimant)
			{
				rangeClaimant = true;
				break;
			}
		}
		foreach (var tile in crossResetQueue)
        {
			if(tile.isPlayer)
            {
				rangeFireman = true;
				break;
            }
        }
        foreach (var tile in crossResetQueue)
        {
			if(tile.tileIsFire)
            {
				rangeFire = true;
				break;
            }
        }
		var player = gameObject.GetComponent<Player>();
		var cd = gameObject.GetComponent<Player>().cd;
		if (cd != null)
		{
			if (cd.buff.Count > 0)
			{
				foreach (var buff in cd.buff)
				{
					if (buff.buffTiming.Check(BuffTiming.BuffTimingEnum.Move))
					{
						buff.checkingCondition.InRangeFireman = rangeFireman;
						buff.checkingCondition.InRangeClaimant = rangeClaimant;
						buff.checkingCondition.InRangeFire = rangeFire;
						buff.checkingCondition.playerList = playerList;
						buff.Check(player);
					}
				}
			}
		}
	}
}
