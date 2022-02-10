using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum TrainingRoomUpgrade
{
    Str,
    Hp,
    Lung
}
public class TrainingRoomUpgradeData
{
    public int hp;
    public int lung;
    public int str;
}
public class TrainingRoom : MonoBehaviour
{
    public Button[] buttons;
    public TrainingSlider[] sliders;
    public TextMeshProUGUI gold;
    private TrainingCharacter tc;
    private int totalGold = 0;
    private bool isTraining = false;

    public RawImage icon;
    public GameObject upgrade;
    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            var index = i;
            buttons[index].onClick.AddListener(() => SetExp(index));
            ButtonInit(index);
        }
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].Init((TrainingStatType)i);
        }
        tc = GetComponentInParent<TrainingCharacter>();
        //Init();
    }
    private void OnEnable()
    {
        tc = GetComponentInParent<TrainingCharacter>();
        Init();
    }
    public void Init()
    {
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].Init((TrainingStatType)i);
        }
        var index = GetComponentInParent<TrainingCharacter>().curIndex;
        icon.texture = Resources.Load<RenderTexture>($"Icon/icon {index}");
        var st = $"Gold : {GameData.userData.gold}";
        gold.text = totalGold != 0 ? st.Insert(st.Length, $" - {totalGold}") : st;
        for (int i = 0; i < buttons.Length; i++)
        {
            ButtonInit(i);
        }
    }
    public void ButtonInit(int index)
    {
        buttons[index].interactable = true;
        if (GameData.userData.gold - (totalGold + GetCost(index)) < 0  || isTraining || tc.curCharacter.isTraining)
        {   
            buttons[index].interactable = false;    
        }
    }
    public void SetExp(int index)
    {
        var trainingCost = Mathf.FloorToInt(100 * (GameData.userData.trainingRoomLevel * 1.5f));
        //var trainingExp = 20 * (GameData.userData.trainingRoomLevel * 2);
        var trainingExp = 0;
        var trainingType = (TrainingType)index;
        switch (trainingType)
        {
            case TrainingType.Str:
                trainingExp = MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.str).TS1Str;
                sliders[index].Init(TrainingStatType.Str, trainingExp);
                break;
            case TrainingType.Lung:
                trainingExp = MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.lung).TS3Lung;
                sliders[index].Init(TrainingStatType.Lung, trainingExp);
                break;
            case TrainingType.Hp:
                trainingExp = MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.hp).TS2Hp;
                sliders[index].Init(TrainingStatType.Hp, trainingExp);
                break;
            case TrainingType.Balence:
                break;
            case TrainingType.Random:
                break;
            default:
                break;
        }
        totalGold += trainingCost;
        isTraining = true;  
        Init();
    }
    public int GetCost(int index)
    {
        var trainingType = (TrainingType)index;
        switch (trainingType)
        {
            case TrainingType.Str:
                return MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.str).TS1Price;
            case TrainingType.Lung:
                return MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.lung).TS3Lung;
            case TrainingType.Hp:
                return MyDataTableMgr.menuTable.GetTable(GameData.userData.traingShopData.hp).TS2Hp;
            case TrainingType.Balence:
                return 100;
            case TrainingType.Random:
                return 100;
            default:
                return 100;
        }
    }
    public void SettingExp()
    {
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].SetExp((TrainingStatType)i);
        }
        GameData.userData.gold -= totalGold;
        totalGold = 0;
        if (isTraining)
        {
            tc.curCharacter.isTraining = isTraining;
        }
        Init();
    }
    public void Back()
    {
        totalGold = 0;
        isTraining = false;
        
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].Back();
        }
        Init();
    }
    public void ShopLevelUp()
    {
        GameData.userData.trainingRoomLevel++;
    }
    public void OnTrainingUpgrade()
    {
        upgrade.SetActive(true);
    }
    public void OffTrainingUpgrade()
    {
        upgrade.SetActive(false);
    }
    public void TrainingUpgrade(int index)
    {
        var upgradeIndex = (TrainingRoomUpgrade)index;
        switch (upgradeIndex)
        {
            case TrainingRoomUpgrade.Str:
                GameData.userData.traingShopData.str++;
                break;
            case TrainingRoomUpgrade.Hp:
                if (GameData.userData.gold >= 1000)
                {
                    GameData.userData.traingShopData.hp = Mathf.Clamp(GameData.userData.traingShopData.hp + 1, 0, 5);
                    Init();
                }
                else
                {
                    UIOnOff.instance.OnNotEnoughMoney();
                }
                //GameData.userData.traingShopData.balence++;
                break;
            case TrainingRoomUpgrade.Lung:
                GameData.userData.traingShopData.lung++;
                break;
            default:
                break;
        }
    }
}
