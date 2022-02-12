using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FireTruckConsum : MonoBehaviour
{
    public int index;
    public Image icon;
    public TextMeshProUGUI stat;
    private ConsumableItemTableData consumData;
    private void OnEnable()
    {
        consumData = MyDataTableMgr.consumItemTable.GetTable(index);
        icon.sprite = consumData.iconSprite;
        stat.text = consumData.price.ToString();
    }
    
    public void OnClick()
    {
        var fireTruck = GetComponentInParent<FireTruck>();
        var itemData = new ConsumableItemData(consumData, 1);
        if (fireTruck.curcharacter != null)
        {
            var consum1 = fireTruck.curcharacter.consum1;
            var consum2 = fireTruck.curcharacter.consum2;

            if (GameData.userData.gold < itemData.itemData.cost || fireTruck.curcharacter.weight < itemData.itemData.weight)
            {
                return;
            }

            if (consum1 != null && consum1.dataTable.itemName == itemData.dataTable.itemName)
            {

                consum1.count++;

            }
            else if (consum2 != null && consum2.dataTable.itemName == itemData.dataTable.itemName)
            {

                consum2.count++;

            }
            else if (consum1 == null)
            {
                fireTruck.curcharacter.consum1 = itemData;
            }
            else if (consum2 == null)
            {
                fireTruck.curcharacter.consum2 = itemData;
            }
            else
            {
                return;
            }

            GameData.userData.gold -= itemData.itemData.cost;
            //fireTruck.fireTruckList.Init();
            fireTruck.characterInfoList.Init();

            var fireman = fireTruck.fireManList[fireTruck.curIndex].GetComponent<FireManInfoPrefab>();
            fireman.Init(fireTruck.curcharacter);

        }
    }
}
