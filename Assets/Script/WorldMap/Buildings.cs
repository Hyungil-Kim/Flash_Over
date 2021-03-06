using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{

    //public Level level;
    public int level;
    public int category;
    [SerializeField] private Color targetColor = Color.red;

    [SerializeField] private bool needBoxIndicator = true;

    [SerializeField] private bool needArrowIndicator = true;

    [SerializeField] private bool needDistanceText = true;

  
    [HideInInspector] public Indicator indicator;

    public Color TargetColor
    {
        get
        {
            return targetColor;
        }
    }


    public bool NeedBoxIndicator
    {
        get
        {
            return needBoxIndicator;
        }
    }

    public bool NeedArrowIndicator
    {
        get
        {
            return needArrowIndicator;
        }
    }


    public bool NeedDistanceText
    {
        get
        {
            return needDistanceText;
        }
    }

    private void OnEnable()
    {
        if (OffScreenIndicator.TargetStateChanged != null)
        {
            OffScreenIndicator.TargetStateChanged.Invoke(this, true);
        }
    }

  
    private void OnDisable()
    {
        if (OffScreenIndicator.TargetStateChanged != null)
        {
            OffScreenIndicator.TargetStateChanged.Invoke(this, false);
        }
    }

  
    public float GetDistanceFromCamera(Vector3 cameraPosition)
    {
        float distanceFromCamera = Vector3.Distance(cameraPosition, transform.position);
        return distanceFromCamera;
    }

    

}
