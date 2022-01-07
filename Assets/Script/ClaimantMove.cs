using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaimantMove
{
	private bool breath = false;
	public void JustStay(Claimant claimant)
	{
		EndMove(claimant);
	}
	public void EndMove(Claimant claimant,List<GroundTile>list)
	{
		claimant.SetState(ClaimantState.End);
		foreach(var elem in list)
		{
			elem.Reset();
		}
		claimant.moveEnd = true;
	}
	public void EndMove(Claimant claimant)
	{
		claimant.SetState(ClaimantState.End);
		claimant.moveEnd = true;
	}
	public IEnumerator MoveToPlayer(Claimant claimant,Player targetPlayer)
	{
		var gameManager = GameManager.instance;
		var preTile = gameManager.tilemapManager.ReturnTile(claimant.gameObject);
		var targetTile = gameManager.tilemapManager.ReturnTile(targetPlayer.gameObject);
		var pathList = new List<GroundTile>();
		var go = true;
		var num = 0;
		var moveSpeed = 5;
		pathList = gameManager.tilemapManager.SetAstar(preTile, targetTile);

		while (go)
		{
			if (pathList.Count ==0) yield break;
			var newPos = new Vector3(pathList[num].transform.position.x, claimant.transform.position.y, pathList[num].transform.position.z);
			if (claimant.transform.position != newPos)
			{
				var dis = Vector3.Distance(claimant.transform.position, newPos);
				if (dis > 0)
				{
					claimant.transform.position = Vector3.MoveTowards(claimant.transform.position, newPos, moveSpeed * Time.deltaTime);
					claimant.transform.LookAt(newPos);
					BreathCheck(newPos,claimant);
				}
				yield return 0;
			}
			else
			{
				if (num < pathList.Count - 1 && num <= claimant.speed && !targetTile.nextTileList.Contains(pathList[num]))
				{
					num++;
					breath = false;
				}
				else
				{
					num = 0;
					go = false;
					EndMove(claimant,pathList);
				}
			}
		}
	}
	public IEnumerator MoveConfuse(Claimant claimant)
	{
		var preTile = GameManager.instance.tilemapManager.ReturnTile(claimant.gameObject);
		var go = true;
		var num = 0;
		var moveSpeed = 5;
		var root = Random.Range(0, preTile.nextTileList.Count);
		while (go)
		{
			var newPos =new Vector3(preTile.nextTileList[root].transform.position.x , claimant.transform.position.y, preTile.nextTileList[root].transform.position.z);
			if(preTile.nextTileList[root].tileIsFire && num <=claimant.speed && preTile.nextTileList[root].isWall)
			{
				num++;
				continue;
			}
			if (claimant.transform.position != newPos && num <= claimant.speed)
			{
				var dis = Vector3.Distance(claimant.transform.position, newPos);
				if (dis > 0)
				{
					claimant.transform.position = Vector3.MoveTowards(claimant.transform.position, newPos, moveSpeed * Time.deltaTime);
					claimant.transform.LookAt(newPos);
				}
				yield return 0;
			}
			else
			{
				if (num <= claimant.speed)
				{
					num++;
					preTile = GameManager.instance.tilemapManager.ReturnTile(claimant.gameObject);
					root = Random.Range(0, preTile.nextTileList.Count);
				}
				else
				{
					num = 0;
					go = false;
					EndMove(claimant);
				}
			}
		}
	}
	public void BreathCheck(Vector3 newPos,Claimant claimant)
	{
		var gameManager = GameManager.instance;
		if (!breath)
		{
			if (gameManager.tilemapManager.ReturnTile(newPos).GetComponentInChildren<Smoke>())
			{
				claimant.ap -= 1;
			}
			claimant.ap -= 1;

			if (claimant.ap < 0)
			{
				claimant.lung += claimant.ap;
				claimant.ap = 0;
			}
			breath = true;
		}
	}
}
