using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManInfo : MonoBehaviour
{
    public FireTruckInventory fireTruckInventory;
    public ConsumShop consumShop;
    public void InventoryInit(ItemType type)
    {
        consumShop.gameObject.SetActive(false);
        fireTruckInventory.gameObject.SetActive(true);
        fireTruckInventory.Init(type);
    }
    public void ConsumShopInit()
    {
        consumShop.gameObject.SetActive(true);
    }
}
