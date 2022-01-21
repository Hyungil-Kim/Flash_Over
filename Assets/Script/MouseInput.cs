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
	private StageSelect stageSelect;
	public float minZoomInch = 0.2f;
	public float maxZoomInch = 0.5f;

	public void OnClick(InputAction.CallbackContext ctx)
    {
        switch (ctx.phase)
        {
            case InputActionPhase.Disabled:
                break;
            case InputActionPhase.Waiting:
                break;
            case InputActionPhase.Started:
                break;
            case InputActionPhase.Performed:
                switch (ctx.interaction)
                {
					case MultiTapInteraction:
						//GameManager.instance.uIManager.gameclearUI.SkipResult();
						break;
                    default:
                        break;
                }
                break;
            case InputActionPhase.Canceled:
                break;
            default:
                break;
        }
    }

    public void Awake()
	{
		gameManager = GetComponent<GameManager>();
		mousePoint = new MoveControlor();
		mousePoint.Mouse.Move.performed += val => gameManager.GetTilePosition(val.ReadValue<Vector2>());
		mousePoint.Mouse.Click.started += val => gameManager.GetClickedStartMouse(mousePoint.Mouse.Move.ReadValue<Vector2>());
		//mousePoint.Mouse.Touch.started += val => gameManager.CharacterChangeStart(mousePoint.Mouse.Move.ReadValue<Vector2>());
		mousePoint.Mouse.TestTouch.started += val => gameManager.CharacterChangeStart(mousePoint.Mouse.Move.ReadValue<Vector2>());
		mousePoint.Mouse.Click.performed += val => gameManager.GetClickingMouse();
		mousePoint.Mouse.Touch.performed += val => gameManager.ChangeMousePointer();
		mousePoint.Mouse.Click.canceled += val => gameManager.GetClickedEndMouse();
		//mousePoint.Mouse.Touch.canceled += val => gameManager.CharacterChanageEnd();
		mousePoint.Mouse.TestTouch.canceled += val => gameManager.CharacterChanageEnd();

		mousePoint.Mouse.Touch.performed += val => gameManager.uIManager.gameclearUI.SkipResult(val); 

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
		//var touchList = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.ToArray();
		//if (touchList.Length == 2)
		//{
		//	mulitiTouchCount(touchList);
		//}
	}
}
