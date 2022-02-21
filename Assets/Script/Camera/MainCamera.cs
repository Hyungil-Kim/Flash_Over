using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
	public GameManager gameManager;
	public void Start()
	{
		gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
	}
	public void ChangeCameraPos(GameObject targetObject)
	{
		transform.position = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y + 6f, targetObject.transform.position.z -2);
	}


}
