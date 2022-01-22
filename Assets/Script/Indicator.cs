using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine.EventSystems;

public enum IndicatorType
{
    ARROW,
    MARKER
}
public class Indicator : MonoBehaviour
{
    [SerializeField] private IndicatorType indicatorType;
    private Image indicatorImage;
    private Text distanceText;
    private UIOnOff uiOnOff;
    public int targetLevel;

    public bool Active
    {
        get
        {
            return transform.gameObject.activeInHierarchy;
        }
    }


    public IndicatorType Type
    {
        get
        {
            return indicatorType;
        }
    }

    void Awake()
    {
        indicatorImage = transform.GetComponent<Image>();
        distanceText = transform.GetComponentInChildren<Text>();
        uiOnOff = GameObject.Find("UIOnOff").GetComponent<UIOnOff>();
       
    }

    public void SetImageColor(Color color)
    {
        indicatorImage.color = color;
    }


    public void SetDistanceText(float value)
    {
        distanceText.text = value >= 0 ? Mathf.Floor(value) + " m" : "";
    }


    public void SetTextRotation(Quaternion rotation)
    {
        distanceText.rectTransform.rotation = rotation;
    }


    public void Activate(bool value)
    {

        transform.gameObject.SetActive(value);
    }

    public void MarkerClick()
    {
        uiOnOff.uiArray[12].SetActive(true);
        uiOnOff.uiArray[13].SetActive(false);
     
        uiOnOff.OverviewOftheSite(targetLevel);

    }

    //public GameObject ButtonReturn()
    //{
    //    //return button = EventSystem.current.currentSelectedGameObject;
    //    //MyDataTableMgr.levelUpTable.GetTable
    //    //MyDataTableMgr.stageInfoTable.GetTable().;
    //}
}
