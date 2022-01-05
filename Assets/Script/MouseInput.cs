using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.EnhancedTouch;

public class MouseInput : MonoBehaviour
{
	public float Zoom { get; private set; }
	public float RotateAngle { get; private set; }

	public MoveControlor mousePoint;
	private GameManager gameManager;
	public float minZoomInch = 0.2f;
	public float maxZoomInch = 0.5f;
	public void Awake()
	{
		gameManager = GetComponent<GameManager>();
		mousePoint = new MoveControlor();
		mousePoint.Mouse.Move.performed += val => gameManager.GetTilePosition(val.ReadValue<Vector2>());
		mousePoint.Mouse.Click.started += val => gameManager.GetClickedStartMouse(mousePoint.Mouse.Move.ReadValue<Vector2>());
		mousePoint.Mouse.Click.performed += val => gameManager.GetClickingMouse();
		mousePoint.Mouse.Click.canceled += val => gameManager.GetClickedEndMouse();
	}

	public void OnEnable()
	{
		mousePoint.Enable();//mouse포지션 실행\
		EnhancedTouchSupport.Enable();
	}
	public void OnDisable()
	{
		mousePoint.Disable();
		EnhancedTouchSupport.Disable();
	}
	public void mulitiTouchCount(UnityEngine.InputSystem.EnhancedTouch.Touch[] touches)
	{
		var touch0 = touches[0];
		var touch1 = touches[1];

		// Pinch / Zoom
		var touch0PrevPos = touch0.screenPosition - touch0.delta; //이전프레임에서의 위치
		var touch1PrevPos = touch1.screenPosition - touch1.delta; //이전프레임에서의 위치

		var diffPrev = Vector2.Distance(touch0PrevPos, touch1PrevPos);
		var diffCurr = Vector2.Distance(touch0.screenPosition, touch1.screenPosition);

		var diffPixels = diffCurr - diffPrev; //+ : 확대 / - : 축소
		var diffInch = diffPixels / Screen.dpi;

		diffInch = Mathf.Clamp(diffInch, -minZoomInch, maxZoomInch);
		var scale = diffInch * Time.deltaTime;
		Zoom = diffInch;
	}
	private void LateUpdate()
	{
		Zoom = 0f;
	}
	public void Update()
	{
		var touchList = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.ToArray();
		if (touchList.Length == 2)
		{
			mulitiTouchCount(touchList);
		}
	}
}
