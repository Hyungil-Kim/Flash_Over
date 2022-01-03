using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManInfo : MonoBehaviour
{
    private CharacterData curCha;
    public CharacterData CurCharacter
    {
        get
        {
            return curCha;
        }
        set
        {
            curCha = value;
        }
    }
    public FireTruckInventory fireTruckInventory;
    public ConsumShop consumShop;
    public FireManCharacter fireManCharacter;
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
    public void SetCharacter(CharacterData cd)
    {
        curCha = cd;
    }
    public void CharacterInit()
    {
        fireManCharacter.Init();
    }
}
