using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManInfo : MonoBehaviour
{
    public FireTruckInventory fireTruckInventory;
    public void InventoryInit(ItemType type)
    {
        fireTruckInventory.Init(type);
    }
}
