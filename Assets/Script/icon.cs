using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icon : MonoBehaviour
{
	private MeshRenderer meshRenderer;
	public void Awake()
	{
		meshRenderer = GetComponent<MeshRenderer>();
	}
	public void Start()
	{
		meshRenderer.material.renderQueue = 3005;
	}
}
