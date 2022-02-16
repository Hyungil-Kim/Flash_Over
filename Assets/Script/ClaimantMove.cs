using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ClaimantMove
{
	private bool breath = false;
	private bool hit = false;
	public void JustStay(Claimant claimant)
	{
		EndMove(claimant);
	}
	public void EndMove(Claimant claimant, List<GroundTile> list)
	{
		claimant.SetState(ClaimantState.End);
		var animator = claimant.GetComponent<Animator>();
		animator.SetBool("walk", false);
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
	public IEnumerator MoveToPlayer(Claimant claimant, Player targetPlayer, int playerIndex)
	{
		var gameManager = GameManager.instance;
		var startTile = gameManager.tilemapManager.ReturnTile(claimant.gameObject);
		GroundTile goalTile;
		if (targetPlayer != null)
		{
			goalTile = gameManager.tilemapManager.ReturnTile(targetPlayer.gameObject);
		}
		else
		{
			goalTile = gameManager.tilemapManager.ReturnTile(Turn.players[playerIndex].gameObject);
		}
		var path = gameManager.tilemapManager.SetAstar(startTile, goalTile);
		var targetTile = goalTile;
		var animator = claimant.GetComponent<Animator>();
		for (var i = path.Count - 1; i >= 0; --i)
		{
			if (path[i].fillList.Count == 0)
			{
				targetTile = path[i];
				break;
			}

			foreach (var adjcent in path[i].nextTileList)
			{
				if (adjcent.fillList.Count == 0)
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
		animator.SetBool("walk", true);

		if (startTile.nextTileList.Contains(goalTile))
		{
			num = 0;
			go = false;
			//animator.SetBool("walk", false);
			EndMove(claimant, pathList);
			yield break;
		}
		while (go)
		{
			if (pathList.Count == 0) yield break;
			var newPos = new Vector3(pathList[num].transform.position.x, claimant.transform.position.y, pathList[num].transform.position.z);
			if (claimant.transform.position != newPos)
			{
				if (AllTile.visionTile.Contains(pathList[num]))
				{
					Camera.main.transform.position = new Vector3(pathList[num].transform.position.x, Camera.main.transform.position.y, pathList[num].transform.position.z - 3);
				}
				var dis = Vector3.Distance(claimant.transform.position, newPos);
				if (dis > 0)
				{
					claimant.transform.position = Vector3.MoveTowards(claimant.transform.position, newPos, moveSpeed * Time.deltaTime);
					claimant.transform.LookAt(newPos);
					hitCheck(newPos, claimant);
					BreathCheck(newPos, claimant);
				}
				yield return 0;
			}
			else
			{
				if (num < pathList.Count - 1 && num <= claimant.data.move)
				{
					num++;
					breath = false;
					hit = false;
				}
				else
				{
					num = 0;
					go = false;
					animator.SetBool("walk", false);
					EndMove(claimant, pathList);
				}
			}
		}
	}
	public List<GroundTile> FindShortPath(Claimant claimant)
	{
		var gameManager = GameManager.instance;
		var startTile = gameManager.tilemapManager.ReturnTile(claimant.gameObject);
		var dummyList = new List<List<GroundTile>>();
		foreach (var exit in gameManager.exitTiles)
		{
			dummyList.Add(gameManager.tilemapManager.SetAstar(startTile, exit));
		}
		var path = dummyList.OrderBy((x) => x.Count).ToList();

		return path[0];

	}
	public IEnumerator MoveToExit(Claimant claimant)
	{
		var gameManager = GameManager.instance;
		var path = FindShortPath(claimant);
		var startTile = gameManager.tilemapManager.ReturnTile(claimant.gameObject);
		GroundTile targetTile = null;
		foreach(var elem in path)
		{
			if(elem.tag == "Exit")
			{
				targetTile = elem;
			} 
		}
		var animator = claimant.GetComponent<Animator>();
		for (var i = path.Count - 1; i >= 0; --i)
		{
			if (path[i].fillList.Count == 0)
			{
				targetTile = path[i];
				break;
			}

			foreach (var adjcent in path[i].nextTileList)
			{
				if (adjcent.fillList.Count == 0 && adjcent)
				{
					targetTile = adjcent;
					break;
				}
			}
		}
		var go = true;
		var num = 0;
		var moveSpeed = 3;
		animator.SetBool("walk", true);

		if (startTile.nextTileList.Contains(targetTile))
		{
			num = 0;
			go = false;
			//animator.SetBool("walk", false);
			EndMove(claimant, path);
			yield break;
		}
		while (go)
		{
			if (path.Count == 0) yield break;
			var newPos = new Vector3(path[num].transform.position.x, claimant.transform.position.y, path[num].transform.position.z);
			if (claimant.transform.position != newPos)
			{
				if (AllTile.visionTile.Contains(path[num]))
				{
					Camera.main.transform.position = new Vector3(path[num].transform.position.x, Camera.main.transform.position.y, path[num].transform.position.z - 3);
				}
				var dis = Vector3.Distance(claimant.transform.position, newPos);
				if (dis > 0)
				{
					claimant.transform.position = Vector3.MoveTowards(claimant.transform.position, newPos, moveSpeed * Time.deltaTime);
					claimant.transform.LookAt(newPos);
					hitCheck(newPos, claimant);
					BreathCheck(newPos, claimant);
				}
				yield return 0;
			}
			else
			{
				if (num < path.Count - 1 && num <= claimant.data.move)
				{
					if(num != path.Count-1)
					{
						if(!path[num + 1].safeArea)
						{
							num = 0;
							go = false;
							claimant.claimantState = 0;
							EndMove(claimant, path);
							yield break;
						}
					}
					num++;
					breath = false;
					hit = false;
				}
				else
				{
					num = 0;
					go = false;
					EndMove(claimant, path);
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
		var animator = claimant.GetComponent<Animator>();
			animator.SetBool("walk", true);
		claimant.transform.position = new Vector3(preTile.transform.position.x, claimant.transform.position.y, preTile.transform.position.z);
		while (go)
		{
			var newPos = new Vector3(preTile.nextTileList[path].transform.position.x, claimant.transform.position.y, preTile.nextTileList[path].transform.position.z);
			var newTile = gameManager.tilemapManager.ReturnTile(newPos);
			if (newTile.fillList.Count != 0 || newTile.tileIsFire || newTile.isObstacle)
			{
				if (num <= claimant.data.move)
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
							breath = false;
							hit = false;
						}
						else
						{
							num = 0;
							go = false;
							animator.SetBool("walk", false);
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
				if (num <= claimant.data.move)
				{
					num++;
					breath = false;
					hit = false;
					preTile = GameManager.instance.tilemapManager.ReturnTile(claimant.gameObject);
					path = Random.Range(0, preTile.nextTileList.Count);
				}
				else
				{
					num = 0;
					go = false;
					animator.SetBool("walk", false);
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
				claimant.lung -= claimant.ap;
				claimant.ap = 0;
			}
			claimant.CheckClaimantLung();
			breath = true;
		}
	}
	public void hitCheck(Vector3 newPos, Claimant claimant)
	{
		var gameManager = GameManager.instance;
		var tilemapManager = gameManager.tilemapManager;
		if (!hit)
		{
			if (tilemapManager.ReturnTile(newPos).GetComponentInChildren<Fire>())
			{
				var fireDamage = tilemapManager.ReturnTile(newPos).GetComponentInChildren<Fire>().data.dmg;
				claimant.hp -= fireDamage;
				claimant.CheckClaimantHp();
				hit = true;
			}
		}
	}
}
