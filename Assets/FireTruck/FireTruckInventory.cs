using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireTruckInventory : MonoBehaviour
{
    public GameObject content;
    public GameObject itemPrefab;
    public List<GameObject> itemObjects =
    new List<GameObject>();
    private int maxItem = GameData.userData.maxItem;
    public void Init(ItemType type)
    {
        if (itemObjects.Count == 0)
        {
            for (int i = 0; i < maxItem; i++)
            {
                var item = Instantiate(itemPrefab, content.transform);
                var button = item.GetComponent<Button>();
                var itemObject = item.GetComponent<InfoInventoryItem>();
                
                item.SetActive(false);
                itemObjects.Add(item);
            }
        }
        foreach (var item in itemObjects)
        {
            item.SetActive(false);
        }
        var hoses = GameData.userData.hoseList;
        var bunkerGears = GameData.userData.bunkerGearList;
        var oxygenTanks = GameData.userData.oxygenTankList;
        switch (type)
        {
            case ItemType.Hose:
                for (int i = 0; i < hoses.Count; i++)
                {
                    var itemObject = itemObjects[i].GetComponent<FireTruckItem>();
                    itemObject.Init(hoses[i]);
                    itemObjects[i].SetActive(true);
                }
                break;
            case ItemType.BunkerGear:
                for (int i = 0; i < bunkerGears.Count; i++)
                {
                    var itemObject = itemObjects[i].GetComponent<FireTruckItem>();
                    itemObject.Init(bunkerGears[i]);
                    itemObjects[i].SetActive(true);
                }
                break;
            case ItemType.OxygenTank:
                for (int i = 0; i < oxygenTanks.Count; i++)
                {
                    var itemObject = itemObjects[i].GetComponent<FireTruckItem>();
                    itemObject.Init(oxygenTanks[i]);
                    itemObjects[i].SetActive(true);
                }
                break;
            default:
                break;
        }
    }
}
