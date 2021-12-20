using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlay : MonoBehaviour
{
    public InventoryItemList test;
    private void Start()
    {
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameData.userData.userName = "babo";
            GameData.userData.SaveUserData(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameData.userData.LoadUserData(1);
            Debug.Log(GameData.userData.userName);
            //test.Init(ItemType.Consumable);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameData.userData.AddItem("CON_0003",ItemType.Consumable);
            test.CurrentItemType = ItemType.Consumable;
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            CharacterData cd = new CharacterData();
            cd.SetCharacter();
            Debug.Log($"HP : {cd.GetStat(CharacterStatType.HP)}");
            Debug.Log($"STR : {cd.GetStat(CharacterStatType.STR)}");
            Debug.Log($"DEF : {cd.GetStat(CharacterStatType.DEF)}");
            Debug.Log($"MOVE : {cd.GetStat(CharacterStatType.MOVE)}");

            GameData.userData.characterList.Add(cd);
        }
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            GameData.userData.AddItem("WEA_0001", ItemType.Weapon);
            test.CurrentItemType = ItemType.Weapon;
        }
        if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            var cd = GameData.userData.characterList[0];
            cd.personality.CheakAllPersonality();
            Debug.Log($"HP : {cd.GetStat(CharacterStatType.HP)}");
            Debug.Log($"STR : {cd.GetStat(CharacterStatType.STR)}");
            Debug.Log($"DEF : {cd.GetStat(CharacterStatType.DEF)}");
            Debug.Log($"MOVE : {cd.GetStat(CharacterStatType.MOVE)}");
        }
        if(Input.GetKeyDown(KeyCode.Alpha7))
        {
            var itemGrade = new WeightList<ItemGrade>();
            itemGrade.AddGrade(ItemGrade.Normal, 40);
            itemGrade.AddGrade(ItemGrade.Rare, 30);
            itemGrade.AddGrade(ItemGrade.Unique, 20);
            itemGrade.AddGrade(ItemGrade.Special, 10);
            for (int i = 0; i < 1000; i++)
            {
                Debug.Log(itemGrade.GetRandomGrade().ToString());
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha8))
        {
            GameData.shopData.shopTime = System.DateTime.Now;
            Debug.Log(GameData.shopData.shopTime.ToString());
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Debug.Log(GameData.shopData.shopTime.ToString());
        }
    }
}
