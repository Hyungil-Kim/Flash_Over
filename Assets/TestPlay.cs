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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameData.userData.characterList[0].exp += 10;
            GameData.userData.characterList[0].CheakExp();
            var cd = GameData.userData.characterList[0];
            Debug.Log($"Level : {cd.level}");
            Debug.Log($"HP : {cd.GetStat(CharacterStatType.Hp)}");
            Debug.Log($"STR : {cd.GetStat(CharacterStatType.Str)}");
            Debug.Log($"LUNG : {cd.GetStat(CharacterStatType.Lung)}");
        }
    }
}
