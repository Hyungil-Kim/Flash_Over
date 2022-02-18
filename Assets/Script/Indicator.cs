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
    [SerializeField] private Sprite[] image;
    private Image indicatorImage;
    private Text distanceText;
    private UIOnOff uiOnOff;
    public int targetLevel;
    public int random;
    public int targetCategory;
    public int[] level1Map = new int[] { 0, 1};
    public int[] level2Map = new int[] { 2, 3 };
    public int[] level3Map = new int[] { 4, 7 };

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

    public void SetImageColor(Color color, IndicatorType type)
    {
        if (type == IndicatorType.MARKER)
        {
            indicatorImage.color = Color.white;
        }
        else if (type == IndicatorType.ARROW)
        {
            indicatorImage.color = color;

        }
    }

    public void SetIndicatorSprite(int level)
    {
        indicatorImage.sprite = image[level-1];
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
        uiOnOff.uiArray[9].SetActive(true);
        uiOnOff.uiArray[10].SetActive(false);

        if (targetCategory == 0)
        {
            if (targetLevel == 1)
            {
                random = Random.Range(0, 2);
                uiOnOff.OverviewOftheSite(level1Map[random]);
            }
            else if (targetLevel == 2)
            {
                random = Random.Range(0, 2);

                uiOnOff.OverviewOftheSite(level2Map[random]);
            }
            else if (targetLevel == 3)
            {
                random = Random.Range(0, 2);

                uiOnOff.OverviewOftheSite(level3Map[random]);
            }
        }
        else if (targetCategory == 1)
        {
            if (targetLevel == 1)
            {
                uiOnOff.OverviewOftheSite(5);
            } 
            else if (targetLevel == 2)
            {
                uiOnOff.OverviewOftheSite(6);
            }
        }
    }

    //public GameObject ButtonReturn()
    //{
    //    //return button = EventSystem.current.currentSelectedGameObject;
    //    //MyDataTableMgr.levelUpTable.GetTable
    //    //MyDataTableMgr.stageInfoTable.GetTable().;
    //}
}
