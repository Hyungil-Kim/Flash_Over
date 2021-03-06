using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;
/// <summary>
/// Attach the script to the off screen indicator panel.
/// </summary>
[DefaultExecutionOrder(-1)]
public class OffScreenIndicator : MonoBehaviour
{
    [SerializeField] private float screenBoundOffset = 0.9f;

    private Camera mainCamera;
    private Vector3 screenCentre;
    private Vector3 screenBounds;

    public GameObject isTarget;

    public List<Buildings> targets;

    public static Action<Buildings, bool> TargetStateChanged;

    void Awake()
    {
        mainCamera = Camera.main;
        screenCentre = new Vector3(Screen.width, Screen.height, 0) / 2;
        screenBounds = screenCentre * screenBoundOffset;
        TargetStateChanged += HandleTargetStateChanged;
        targets = new List<Buildings>();
    }

    private void Update()
    {
        DrawIndicators();

    }
    void LateUpdate()
    {

    }

    /// <summary>
    /// Draw the indicators on the screen and set thier position and rotation and other properties.
    /// </summary>
    void DrawIndicators()
    {

        foreach (var target in targets)
        {
            Vector3 screenPosition = OffScreenIndicatorCore.GetScreenPosition(mainCamera, target.transform.position);
            bool isTargetVisible = OffScreenIndicatorCore.IsTargetVisible(screenPosition);
            float distanceFromCamera = target.NeedDistanceText ? target.GetDistanceFromCamera(mainCamera.transform.position) : float.MinValue;// Gets the target distance from the camera.
            Indicator indicator = null;

            if (target.NeedBoxIndicator && isTargetVisible)
            {   
                
                if(target.gameObject.GetComponentInChildren<VisualEffect>()!=null)
                {
                    if (target.gameObject.GetComponentInChildren<VisualEffect>().enabled==true)
                    {
                        screenPosition.z = 0;
                        indicator = GetIndicator(ref target.indicator, IndicatorType.MARKER); // Gets the box indicator from the pool.
                        indicator.targetLevel = target.level;
                        indicator.targetCategory = target.category;
                        indicator.SetIndicatorSprite(target.level);
                    }
                   
                }
                
            }

            else if (target.NeedArrowIndicator && !isTargetVisible)
            {
                if (target.gameObject.GetComponentInChildren<VisualEffect>() != null)
                {
                    if (target.gameObject.GetComponentInChildren<VisualEffect>().enabled == true)
                    {
                        float angle = float.MinValue;
                        OffScreenIndicatorCore.GetArrowIndicatorPositionAndAngle(ref screenPosition, ref angle, screenCentre, screenBounds);
                        indicator = GetIndicator(ref target.indicator, IndicatorType.ARROW); // Gets the arrow indicator from the pool.
                        indicator.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg); // Sets the rotation for the arrow indicator.
                    }
                }
            }
            else
            {
                target.indicator?.Activate(false);
                target.indicator = null;
            }
            if (indicator)
            {
                indicator.SetImageColor(target.TargetColor,target.indicator.Type);// Sets the image color of the indicator.
                indicator.SetDistanceText(distanceFromCamera); //Set the distance text for the indicator.
                indicator.transform.position = screenPosition; //Sets the position of the indicator on the screen.
                indicator.SetTextRotation(Quaternion.identity); // Sets the rotation of the distance text of the indicator.
            }
        }
    }

    /// <summary>
    /// 1. Add the target to targets list if <paramref name="active"/> is true.
    /// 2. If <paramref name="active"/> is false deactivate the targets indicator, 
    ///     set its reference null and remove it from the targets list.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="active"></param>
    private void HandleTargetStateChanged(Buildings target, bool active)
    {
        if (active)
        {
            targets.Add(target);
        }
        else
        {
            target.indicator?.Activate(false);
            target.indicator = null;
            targets.Remove(target);
        }
    }

    /// <summary>
    /// Get the indicator for the target.
    /// 1. If its not null and of the same required <paramref name="type"/> 
    ///     then return the same indicator;
    /// 2. If its not null but is of different type from <paramref name="type"/> 
    ///     then deactivate the old reference so that it returns to the pool 
    ///     and request one of another type from pool.
    /// 3. If its null then request one from the pool of <paramref name="type"/>.
    /// </summary>
    /// <param name="indicator"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private Indicator GetIndicator(ref Indicator indicator, IndicatorType type)
    {
        if (indicator != null)
        {
            if (indicator.Type != type)
            {
                indicator.Activate(false);
                indicator = type == IndicatorType.MARKER ? MarkerIndicator.current.GetPooledObject() : ArrowIndicators.current.GetPooledObject();
                indicator.Activate(true); // Sets the indicator as active.
            }
        }
        else
        {
            indicator = type == IndicatorType.MARKER ? MarkerIndicator.current.GetPooledObject() : ArrowIndicators.current.GetPooledObject();
            indicator.Activate(true); // Sets the indicator as active.
        }
        return indicator;
    }

    private void OnDestroy()
    {
        TargetStateChanged -= HandleTargetStateChanged;
    }
}
