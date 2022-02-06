using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHose : MonoBehaviour
{
	public IEnumerator CheckFireHoseStop(Player player)
	{
			yield return new WaitForSeconds(player.waterStraight.main.duration);
			player.animator.SetBool("shoot", false);
	}
}
