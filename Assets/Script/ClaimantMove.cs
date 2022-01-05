using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaimantMove
{
	public void JustStay(Claimant claimant)
	{
		claimant.SetState(ClaimantState.End);
	}
	public void EndMove(Claimant claimant,List<GroundTile>list)
	{
		claimant.SetState(ClaimantState.End);
		foreach(var elem in list)
		{
			elem.Reset();
		}
	}
	public IEnumerator MoveToPlayer(Claimant claimant,Player targetPlayer)
	{
		var preTile = GameManager.instance.tilemapManager.ReturnTile(claimant.gameObject);
		var targetTile = GameManager.instance.tilemapManager.ReturnTile(targetPlayer.gameObject);
		var pathList = new List<GroundTile>();
		var go = true;
		var num = 0;
		var moveSpeed = 5;
		pathList = GameManager.instance.tilemapManager.SetAstar(preTile, targetTile);
		Debug.Log(pathList.Count);

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
				}
				yield return 0;
			}
			else
			{
				if (num < pathList.Count - 1 && num <= claimant.speed)
				{
					num++;
				}
				else
				{
					num = 0;
					go = false;
					
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
			if(preTile.nextTileList[root].tileIsFire && num <=claimant.speed)
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
				}
			}
		}
	}
}
