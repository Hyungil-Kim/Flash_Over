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
    
    public void SetCharacter(CharacterData cd,int index = 0)
    {
        
        curCha = cd;
        switch (index)
        {
            case 1:
                if (cd.consum1 != null)
                {
                    GameData.userData.gold += cd.consum1.itemData.cost;
                    cd.UseConsumItem(1);
                }
                break;
            case 2:
                if (cd.consum2 != null)
                {
                    GameData.userData.gold += cd.consum2.itemData.cost;
                    cd.UseConsumItem(2);
                }
                break;
            default:
                break;
        }
        CharacterInit();
    }
    public void CharacterInit()
    {
        fireManCharacter.Init();
    }
    public void OnExit()
    {
        var fireTruck = GetComponentInParent<FireTruck>();
        fireTruck.OnExitConsumShop();
        gameObject.SetActive(false);
    }
}
