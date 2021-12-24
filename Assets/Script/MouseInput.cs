using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseInput : MonoBehaviour
{
	public MoveControlor mousePoint;
	private GameManager gameManager;
	public void Awake()
	{
		gameManager = GetComponent<GameManager>();
		mousePoint = new MoveControlor();
		mousePoint.Mouse.Move.performed += val => gameManager.GetTilePosition(val.ReadValue<Vector2>());
		mousePoint.Mouse.Click.started += val => gameManager.GetClickedStartMouse();
		mousePoint.Mouse.Click.performed += val => gameManager.GetClickingMouse();
		mousePoint.Mouse.Click.canceled += val => gameManager.GetClickedEndMouse();
	}

	public void OnEnable()
	{
		mousePoint.Enable();//mouse포지션 실행
	}
	public void OnDisable()
	{
		mousePoint.Disable();
	}
	
}
