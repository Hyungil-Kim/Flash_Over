using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.EnhancedTouch;

public class ScreenTouch : MonoBehaviour
{
    public MoveControlor mousePoint;
    private StageSelect stageSelect;

    // Start is called before the first frame update
    void Awake()
    {
		mousePoint = new MoveControlor();
		stageSelect = GameObject.Find("StageSelect").GetComponent<StageSelect>();

		mousePoint.Mouse.UiTouch.started += val => stageSelect.ScreenTouch(mousePoint.Mouse.UiMove.ReadValue<Vector2>());
    }



	public void OnEnable()
	{
		mousePoint.Enable();//mouse포지션 실행\

	}
	public void OnDisable()
	{
		mousePoint.Disable();

	}
}
