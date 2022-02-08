using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Turn
{
	public static List<Player> players = new List<Player>();
	public static List<Fire> fires = new List<Fire>();
	public static List<Smoke> smokes = new List<Smoke>();
	public static List<Claimant> claimants = new List<Claimant>();
	public static Dictionary<int, GameObject> saveClaimants = new Dictionary<int, GameObject>();

	public static List<Window> windows = new List<Window>();
	public static List<CameraForFireZone> areaCamera = new List<CameraForFireZone>();
	public static int maxArea = 4;
	public static List<Smoke> copylist = new List<Smoke>();

	public delegate void turnOff();
	public static turnOff TurnOff;
	public static CameraController cameraController;


	public static void OnDestroy()
    {
		players.Clear();
		fires.Clear();
		smokes.Clear();
		claimants.Clear();
		windows.Clear();
		areaCamera.Clear();
		copylist.Clear();
		saveClaimants.Clear();
    }
	public static IEnumerator CoTurnSystem()
	{
		foreach (var player in players)
		{
			if (player.curStateName==PlayerState.Idle)
            {
				GameManager.instance.ChangeTargetPlayer(player.gameObject);
				GameManager.instance.move = player.cd.totalStats.move;

			}
			if(player.curStateName != PlayerState.End)
			{
				yield break;
			}
			if(player.curStateName == PlayerState.End)
            {
				Debug.Log("들어옴");
				GameManager.ChangeLayersRecursively(player.transform, "Player");
            }
		}
		///
		/// setActive UI( 적턴)
		/// yield return secind
		///
		if (fires.Count != 0)
		{
			for (int i = 0; i <= maxArea; i++)
			{
				var sortMonster = fires.Where((x) => x.fireArea == i);	
				foreach (var monster in sortMonster)
				{
					monster.FireAct();
					monster.ChangeState(FireState.End);
				}

				foreach(var all in AllTile.visionTile)
				{
					if (all.tileIsFire && all.tileArea == i)
					{
						//Camera.main.transform.position = areaCamera[i].transform.position;
						yield return new WaitForSeconds(0.5f);
						break;
					}
				}

				fires[0].FireEndAct();

				//TurnOff();
				foreach (var all in AllTile.visionTile)
				{
					if (all.tileIsFire && all.tileArea == i)
					{
						yield return new WaitForSeconds(2f);
						break;
					}
				}
			}
		}
		if (fires.Count != 0)
		{
			foreach (var fire in fires)
			{
				fire.CheckFireHp();
				break;
			}
			Debug.Log(fires.Count);
		}
		else
		{

			//일단 주석!
			//test
			var gameClearUI = GameManager.instance.uIManager.gameclearUI;
			gameClearUI.gameObject.SetActive(true);
		}
		
		if (smokes.Count != 0)
		{
			foreach (var smoke in smokes)
			{
				var ground = smoke.GetComponentInParent<GroundTile>();
				for (int i = 0; i < ground.nextTileList.Count; i++)
				{
					if (!smokes.Contains(ground.nextTileList[i].gameObject.GetComponentInChildren<Smoke>()) && ground.nextTileList[i].tileSaveSmokeValue != 0)
					{
						copylist.Add(ground.nextTileList[i].GetComponentInChildren<Smoke>(true));
					}
				}
			}
			foreach (var elem in copylist)
			{
				smokes.Add(elem);
			}
			foreach (var smoke in smokes)
			{
				smoke.ResetSmokeValue();
			}
			foreach (var smoke in smokes)
			{
				smoke.SaveSmoke();
			}
			foreach (var window in windows)
			{
				window.WindowTurn();
			}
		}
		//
		// setactive(true) 요구조자턴 ui
		//
		if (claimants.Count != 0)
		{
			for (int i = 0; i <= maxArea; i++)
			{
				var sorClaimant = claimants.Where((x) => x.claimantArea == i);
				foreach (var claimant in sorClaimant)
				{
					if (claimant.curStateName != ClaimantState.Resuce && claimant.curStateName != ClaimantState.End)
					{
						claimant.moveEnd = false;
						claimant.ClaimantAct();
						claimant.oxygentank -= 1;
						if (claimant.oxygentank <= 0)
						{
							claimant.ap = claimant.Maxap;
						}
						yield return new WaitUntil(() => claimant.moveEnd == true);
					}

					yield return new WaitForSeconds(0.5f);

				}
			}
		}
		ChangeStateIdle();
	}
	public static void ChangeStateIdle()
	{
		if (players.Count != 0)
		{
			foreach (var player in players)
			{
				player.ChangeState(PlayerState.Idle);
			}
		}
		if (fires.Count != 0)
		{
			foreach (var fire in fires)
			{
				fire.ChangeState(FireState.Idle);
			}
		}
		if (claimants.Count != 0)
		{
			foreach (var claimant in claimants)
			{
				if (claimant.curStateName != ClaimantState.Resuce)
					claimant.ChangeState(ClaimantState.Idle);
			}
		}
		GameManager.instance.targetPlayer = null;
		GameManager.instance.turnCount++;
		GameManager.instance.TurnSystem();
	}
	public static void OutOfSight(List<Fire> fireList, float sec)
	{

		if (players.Count != 0)
		{
			GameManager.instance.ChangeTargetPlayer(players[0].gameObject);
		}
	}
	public static void EndGame()
	{
	}

	public static void SortCameraArea()
	{
		var sortedAreaCamera = from elem in areaCamera
							   orderby elem.areaNum
							   select elem;
		areaCamera = sortedAreaCamera.ToList();
	}
}
