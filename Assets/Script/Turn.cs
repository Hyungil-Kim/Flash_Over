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
	public static List<Window> windows = new List<Window>();
	public static List<CameraForFireZone> fireCamera = new List<CameraForFireZone>();

	public static int maxArea = 4;
	public static List<Smoke> copylist = new List<Smoke>();

	public delegate void turnOff();
	public static turnOff TurnOff;
	public static CameraController cameraController;

	public static IEnumerator CoTurnSystem()
	{
		foreach (var player in players)
		{
			if(player.curStateName==PlayerState.Idle)
            {
				cameraController.CameraMoving(player);

			}
			//if (player.curStateName == PlayerState.Move)
			//{
			//	Debug.Log("µé¾î¿Í?");
			//	cameraController.CameraMoving(player);

			//}
			if(player.curStateName != PlayerState.End)
			{
				yield break;
			}
		}
	
		if (fires.Count != 0)
		{
			for (int i = 0; i <= maxArea; i++)
			{
     //           foreach (var forFire in fireCamera)
     //           {
					//if(forFire.areaNum==i)
     //               {
					//	//Camera.main.transform.position = forFire.transform.position;
					//	yield return new WaitForSeconds(1.0f);
     //               }
					
     //           }
				var sortMonster = fires.Where((x) => x.fireArea == i);
				foreach (var monster in sortMonster)
				{
					
					monster.FireAct();
					monster.ChangeState(FireState.End);
				}
			
				foreach (var elem in sortMonster)
				{
					if (AllTile.visionTile.Contains(GameManager.instance.tilemapManager.ReturnTile(elem.gameObject)))
					{
						yield return new WaitForSeconds(0.5f);
						break;
					}
				}
				fires[0].FireEndAct();


				//TurnOff();
				foreach (var elem in sortMonster)
				{
					if (AllTile.visionTile.Contains(GameManager.instance.tilemapManager.ReturnTile(elem.gameObject)))
					{

						yield return new WaitForSeconds(2f);
						break;
					}
				}
			}
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
		foreach(var elem in copylist)
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

		if (claimants.Count != 0)
		{
			for (int i = 0; i <= maxArea; i++)
			{
				var sorClaimant = claimants.Where((x) => x.claimantArea == i);
				foreach (var claimant in sorClaimant)
				{
					if (claimant.curStateName != ClaimantState.Resuce && claimant.curStateName != ClaimantState.End)
					{
						//Camera.main.transform.position = new Vector3(claimant.transform.position.x, Camera.main.transform.position.y, claimant.transform.position.z - 3);

						claimant.moveEnd = false;
						claimant.ClaimantAct();
						claimant.oxygentank -= 1;
						if(claimant.oxygentank <= 0)
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
				if (player.curStateName != PlayerState.Idle)
					player.ChangeState(PlayerState.Idle);
			}
		}
		if (fires.Count != 0)
		{
			foreach (var fire in fires)
			{
				if (fire.curStateName != FireState.Idle)
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
	}
	public static void OutOfSight(List<Fire> fireList,float sec)
	{
		foreach(var elem in fireList)
		{
			if (AllTile.visionTile.Contains(GameManager.instance.tilemapManager.ReturnTile(elem.gameObject)))
			{
				
			}
		}

	}
	public static void EndGame()
	{
		
	}
}
