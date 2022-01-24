using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.EnhancedTouch;

public class ScreenTouch : MonoBehaviour
{
	public float Zoom { get; private set; }

	public MoveControlor mousePoint;
    private StageSelect stageSelect;
	public float minZoomInch = 0.2f;
	public float maxZoomInch = 0.5f;
	// Start is called before the first frame update

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

	void Awake()
    {
		mousePoint = new MoveControlor();
		stageSelect = GameObject.Find("StageSelectFix").GetComponent<StageSelect>();

		mousePoint.Mouse.UiMove.performed += val => stageSelect.ScreenMove(val.ReadValue<Vector2>());
		mousePoint.Mouse.UiTouch.started += val => stageSelect.ScreenDrag(mousePoint.Mouse.UiMove.ReadValue<Vector2>());
		mousePoint.Mouse.UiTouch.started += val => stageSelect.ScreenTouch(mousePoint.Mouse.UiMove.ReadValue<Vector2>());
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
        //    mulitiTouchCount(touchList);
        //}
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
}
