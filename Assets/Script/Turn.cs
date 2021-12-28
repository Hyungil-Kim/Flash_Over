using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Turn
{
	public static List<Player> players = new List<Player>();
	public static List<Monster> monsters = new List<Monster>();
	//public static List<Npc> npcs = new List<Npc>();

	public static int maxArea = 10;
	public static IEnumerator CoTurnSystem()
	{
		foreach (var player in players)
		{
			if (player.curStateName != PlayerState.End)
			{
				yield break;
			}
		}

		for (int i = 1; i <= maxArea; i++)
		{
			var sortMonster = monsters.Where((x) => x.area == i);
			foreach (var monster in sortMonster)
			{
				monster.FireAct();
			}
			yield return new WaitForSeconds(0.5f);
			monsters[0].FireEndAct();
			yield return new WaitForSeconds(2f);
			
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
}
