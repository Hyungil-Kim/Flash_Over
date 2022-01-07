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
        var firemanInfo = GetComponentInParent<FireManInfo>();
        var itemData = new ConsumableItemData(consumData, 1);
        var consum1 = firemanInfo.CurCharacter.consum1;
        var consum2 = firemanInfo.CurCharacter.consum2;

        if(GameData.userData.gold < itemData.itemData.cost || firemanInfo.CurCharacter.weight < itemData.itemData.weight)
        {
            return;
        }

        if(consum1 != null && consum1.dataTable.itemName == itemData.dataTable.itemName)
        {
            
            consum1.count++;

        }
        else if (consum2 != null && consum2.dataTable.itemName == itemData.dataTable.itemName)
        {

            consum2.count++;

        }
        else if (consum1 == null)
        {
            firemanInfo.CurCharacter.consum1 = itemData;
        }
        else if (consum2 == null)
        {
            firemanInfo.CurCharacter.consum2 = itemData;
        }
        else
        {
            return;
        }
        GameData.userData.gold -= itemData.itemData.cost;
        firemanInfo.CharacterInit();
    }
}
