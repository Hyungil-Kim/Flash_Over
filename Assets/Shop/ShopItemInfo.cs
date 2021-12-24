using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShopItemInfo : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI stat;
    public void Init(int index)
    {
        var itemData = GameData.userData.shopItemList[index];
        var isHose = itemData as HoseData;
        var isBunkerGear = itemData as BunkerGearData;
        var isOxygenTank = itemData as OxygenTankData;

        icon.sprite = itemData.dataTable.iconSprite;
        stat.text = "";
        foreach (var enumValue in System.Enum.GetValues(typeof(CharacterStatType)))
        {
            var statType = (CharacterStatType)enumValue;
            if(itemData.GetStat(statType) != 0)
            {
                stat.text = stat.text.Insert(stat.text.Length, $"\n{statType.ToString()} : {itemData.GetStat(statType).ToString()}");
            }
        }
        stat.text.Remove(0, 1);
    }
    public void OnExit()
    {
        gameObject.SetActive(false);
    }
}
