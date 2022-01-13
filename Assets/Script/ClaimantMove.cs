using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ClaimantMove
{
	private bool breath = false;
	public void JustStay(Claimant claimant)
	{
		EndMove(claimant);
	}
	public void EndMove(Claimant claimant, List<GroundTile> list)
	{
		claimant.SetState(ClaimantState.End);
		foreach (var elem in list)
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
	public IEnumerator MoveToPlayer(Claimant claimant, Player targetPlayer)
	{
		var gameManager = GameManager.instance;
		var startTile = gameManager.tilemapManager.ReturnTile(claimant.gameObject);
		var goalTile = gameManager.tilemapManager.ReturnTile(targetPlayer.gameObject);
		var path = gameManager.tilemapManager.SetAstar(startTile, goalTile);
		var targetTile = goalTile;
		for (var i = path.Count - 1; i >= 0; --i)
		{
			if (path[i].fillList.Count == 0)
			{
				targetTile = path[i];
				break;
			}

			foreach (var adjcent in path[i].nextTileList)
			{
				if (adjcent.fillList.Count == 0 )
				{
					targetTile = adjcent;
					break;
				}
			}
		}


		var pathList = gameManager.tilemapManager.SetAstar(startTile, targetTile);
		var go = true;
		var num = 0;
		var moveSpeed = 3;

		if (startTile.nextTileList.Contains(goalTile))
		{
			num = 0;
			go = false;
			EndMove(claimant, pathList);
			yield break;
		}
		while (go)
		{
			if (pathList.Count == 0) yield break;
			var newPos = new Vector3(pathList[num].transform.position.x, claimant.transform.position.y, pathList[num].transform.position.z);
			if (claimant.transform.position != newPos)
			{
				if(AllTile.visionTile.Contains(pathList[num]))
				{
					Camera.main.transform.position = new Vector3(pathList[num].transform.position.x, Camera.main.transform.position.y, pathList[num].transform.position.z - 3);
				}
				var dis = Vector3.Distance(claimant.transform.position, newPos);
				if (dis > 0)
				{
					claimant.transform.position = Vector3.MoveTowards(claimant.transform.position, newPos, moveSpeed * Time.deltaTime);
					claimant.transform.LookAt(newPos);
					BreathCheck(newPos, claimant);
				}
				yield return 0;
			}
			else
			{
				if (num < pathList.Count - 1 && num <= claimant.speed)
				{
					num++;
					breath = false;
				}
				else
				{
					num = 0;
					go = false;
					EndMove(claimant, pathList);
				}
			}
		}
	}
	public IEnumerator MoveConfuse(Claimant claimant)
	{
		var gameManager = GameManager.instance;
		var preTile = gameManager.tilemapManager.ReturnTile(claimant.gameObject);
		var go = true;
		var num = 0;
		var moveSpeed = 3;
		var path = Random.Range(0, preTile.nextTileList.Count);
		claimant.transform.position = new Vector3(preTile.transform.position.x, claimant.transform.position.y, preTile.transform.position.z);
		while (go)
		{
			var newPos = new Vector3(preTile.nextTileList[path].transform.position.x, claimant.transform.position.y, preTile.nextTileList[path].transform.position.z);
			var newTile = gameManager.tilemapManager.ReturnTile(newPos);

			if (newTile.fillList.Count != 0 || newTile.tileIsFire)
			{
				if (num <= claimant.speed)
				{
			if (AllTile.visionTile.Contains(preTile))
			{
				Camera.main.transform.position = new Vector3(preTile.transform.position.x, Camera.main.transform.position.y, preTile.transform.position.z - 3);
			}
					num++;
					preTile = GameManager.instance.tilemapManager.ReturnTile(claimant.gameObject);
					path = Random.Range(0, preTile.nextTileList.Count);
				}
				else
				{
					while (claimant.transform.position != preTile.transform.position)
					{
						var prePos = new Vector3(preTile.transform.position.x, claimant.transform.position.y, preTile.transform.position.z);
						var dis = Vector3.Distance(claimant.transform.position, prePos);
						if (dis > 0)
						{
							claimant.transform.position = Vector3.MoveTowards(claimant.transform.position, prePos, moveSpeed * Time.deltaTime);
							claimant.transform.LookAt(prePos);
						}
						else
						{
							num = 0;
							go = false;
							EndMove(claimant);
							yield break;
						}
						yield return 0;
					}
				}
				continue;
			}

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
				if (num <= claimant.speed)
				{
					num++;
					preTile = GameManager.instance.tilemapManager.ReturnTile(claimant.gameObject);
					path = Random.Range(0, preTile.nextTileList.Count);
				}
				else
				{
					num = 0;
					go = false;
					EndMove(claimant);
					yield break;
				}
			}
		}
	}
	public void BreathCheck(Vector3 newPos, Claimant claimant)
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
