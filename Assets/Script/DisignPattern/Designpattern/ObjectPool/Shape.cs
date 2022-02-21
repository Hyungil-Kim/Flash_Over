using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
public class Shape : MonoBehaviour
{
	private Action<Shape> kAction;

	public void Init(Action<Shape> killAction)
	{
		kAction = killAction;
	}

	//��������
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.CompareTag("Ground")) kAction(this);
	}

}
