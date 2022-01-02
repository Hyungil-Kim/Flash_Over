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
    }
}
