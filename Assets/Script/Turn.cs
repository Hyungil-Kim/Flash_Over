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

	public static int maxArea = 4;
	public static List<Smoke> copylist = new List<Smoke>();
	public static IEnumerator CoTurnSystem()
	{
		foreach (var player in players)
		{
			if (player.curStateName != PlayerState.End)
			{
				yield break;
			}
		}

		for (int i = 0; i <= maxArea; i++)
		{
			var sortMonster = fires.Where((x) => x.fireArea == i);
			if(sortMonster.ToList().Count == 0)// 시야 조건 추가필요
			{
				ChangeStateIdle(i);
				continue;
			}
			foreach (var monster in sortMonster)
			{
				monster.FireAct();
				monster.ChangeState(FireState.End);
			}
			yield return new WaitForSeconds(0.5f);
			fires[0].FireEndAct();

			//연기 이동=>

			//foreach(var smoke in sortSmoke)
			//{
			//	smoke.SpreadSmoke();
			//}


			yield return new WaitForSeconds(2f);

			ChangeStateIdle(i);
		}
		for(int i =0; i<= maxArea;i++)
		{
			var sorClaimant = claimants.Where((x) => x.claimantArea == i);
			if (sorClaimant.ToList().Count == 0)// 시야 조건 추가필요
			{
				ChangeStateIdle(i);
				continue;
			}
			foreach (var claimant in sorClaimant)
			{
				claimant.ClimantAct();
				claimant.ChangeState(ClaimantState.End);
			}
			yield return new WaitForSeconds(0.5f);

		}
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

		//for (int i = 1; i <= maxArea; i++)
		//{
		//    var sortNpc = npcs.Where((x) => x.areaNumber == i);
		//    foreach (var npc in sortNpc)
		//    {
		//        npc.Act();
		//    }
		//    Debug.Log($"{i}번 구역 NPC 완료");
		//    yield return new WaitForSeconds(2f);
		//}
	}

	public static void ChangeStateIdle(int area)
	{
		if (area == maxArea)
		{
			foreach (var player in players)
			{
				player.ChangeState(PlayerState.Idle);
			}
			foreach (var fire in fires)
			{
				fire.ChangeState(FireState.Idle);
			}
			foreach (var claimant in claimants)
			{
				claimant.ChangeState(ClaimantState.Idle);
			}
		}
	}
}
