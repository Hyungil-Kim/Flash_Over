using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.EnhancedTouch;
using System.Collections;
public class ScreenTouch : MonoBehaviour
{
	public float Zoom { get; private set; }

	public MoveControlor mousePoint;
	private Coroutine zoomCoroutine;
	private StageSelect stageSelect;
	private Transform cameraTransform;

	[SerializeField]
	private float cameraSpeed = 4f;

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
		cameraTransform = Camera.main.transform;
		stageSelect = GameObject.Find("StageSelectFix").GetComponent<StageSelect>();

		
	}
    private void Start()
    {
		mousePoint.Mouse.UiMove.performed += val => stageSelect.ScreenMove(val.ReadValue<Vector2>());
		mousePoint.Mouse.UiTouch.started += val => stageSelect.ScreenDrag(mousePoint.Mouse.UiMove.ReadValue<Vector2>());
		mousePoint.Mouse.UiTouch.started += val => stageSelect.ScreenTouch(mousePoint.Mouse.UiMove.ReadValue<Vector2>());
		mousePoint.Mouse.SecondaryFingerContact.started += val => ZoomStart();
		mousePoint.Mouse.SecondaryFingerContact.canceled += val => ZoomEnd();
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
	private void ZoomStart()
	{
		zoomCoroutine = StartCoroutine(ZoomDetection());
	}
	private void ZoomEnd()
	{
		StopCoroutine(zoomCoroutine);
	}
	IEnumerator ZoomDetection()
	{
		
		float previousDistance = 0f;
		float distance = 0f;
		while (true)
		{
			distance = Vector2.Distance(mousePoint.Mouse.PrimaryFingerPosition.ReadValue<Vector2>(), mousePoint.Mouse.SecondaryFingerPosition.ReadValue<Vector2>());
			if (distance > previousDistance)
			{
				Vector3 targetPosition = cameraTransform.position;
				targetPosition.y -= 1;
				if (targetPosition.y <= 1) yield return targetPosition.y == 5;
				cameraTransform.position = Vector3.Slerp(cameraTransform.position, targetPosition, Time.deltaTime * cameraSpeed);
			}
			else if (distance < previousDistance)
			{
				Vector3 targetPosition = cameraTransform.position;
				targetPosition.y += 1;
				if (targetPosition.y > 1) yield return targetPosition.y == 700;

				cameraTransform.position = Vector3.Slerp(cameraTransform.position, targetPosition, Time.deltaTime * cameraSpeed);
			}
			previousDistance = distance;
			yield return null;
		}
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
