using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public CharacterInfoInventory inventory;
    public CharacterInfoStat chaStatPanal;
    public CharacterInfoList chaListPanal;
    public ItemInfo info;
    public int currentCharacterIndex;
    public ItemType currentItemType;
    public CharacterData curCharacter;
    private void OnEnable()
    {
        //GameData.userData.LoadUserData(1);
    }
    public void OnChaIcon()
    {
        //chaListPanal.gameObject.SetActive(false);
        chaStatPanal.gameObject.SetActive(true);
    }
    public void OnExitStat()
    {
        chaStatPanal.gameObject.SetActive(false);
        chaListPanal.gameObject.SetActive(true);
    }
    public void OnInventory()
    {
        chaStatPanal.gameObject.SetActive(false);
        inventory.gameObject.SetActive(true);
        inventory.Init(currentItemType);
    }
    public void OnExitInventory()
    {
        chaStatPanal.gameObject.SetActive(true);
        inventory.gameObject.SetActive(false);
    }

    public void OnItemButton(int index)
    {
        currentItemType = (ItemType)index;
        OnInventory();
    }
}
