using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;



public class AstarAlgoritm
{
	private GroundTile astartTarget, curTile;
	private List<GroundTile> openList;
	private List<GroundTile> closedList;
	public List<GroundTile> finalList;
	public void PathFinding(GroundTile startTile ,GroundTile endTile,Color setColor)
	{
		openList = new List<GroundTile>();
		closedList = new List<GroundTile>();
		finalList = new List<GroundTile>();
		astartTarget = endTile;

		openList.Add(startTile);
		while (openList.Count > 0)
		{
			curTile = openList[0];
			for(int i = 1; i< openList.Count;i++)
			{
				if(openList[i].F <= curTile.F && openList[i].H < curTile.H)
				{
					curTile = openList[i];
				}
			}
			openList.Remove(curTile);
			closedList.Add(curTile);

			if(curTile == endTile)
			{
				GroundTile endCurTile = endTile;
				while(endCurTile != startTile)
				{
					finalList.Add(endCurTile);
					endCurTile = endCurTile.ParentTile;
				}
				finalList.Add(startTile);
				finalList.Reverse();
				foreach (var elem in finalList)
				{
					elem.SetTileColor(setColor);
					
				}
				return;
			}

			for(int i =0; i < curTile.nextTileList.Count; i++)
			{
				OpenListAdd(curTile.nextTileList[i]);
			}

		}
		
	}

	private void OpenListAdd(GroundTile nextTile)
	{
		if((!nextTile.isFill || nextTile.isPlayer) && !closedList.Contains(nextTile) && nextTile.gameObject.activeSelf)
		{
			GroundTile neighborTile = nextTile;
			int moveCost = curTile.G + 10;

			if(moveCost < neighborTile.G || !openList.Contains(neighborTile))
			{
				neighborTile.G = moveCost;
				neighborTile.H = (Mathf.Abs(neighborTile.cellpos.x - astartTarget.cellpos.x) + Mathf.Abs(neighborTile.cellpos.y - astartTarget.cellpos.y)) * 10;
				neighborTile.ParentTile = curTile;

				openList.Add(neighborTile);
			}
		}
	}
	public void ResetRootColor(Color resetColor)
	{
		foreach (var elem in finalList)
		{
			elem.SetTileColor(resetColor);
		}
	}
}
