using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ItemDataBase
{
    public int count;
    public ItemTableDataBase dataTable;
    [System.NonSerialized]
    public CharacterData owner;
    public bool isSold;

    public int GetStat(CharacterStatType statType)
    {
        switch (statType)
        {
            case CharacterStatType.Hp:
                return dataTable.hp;
            case CharacterStatType.Str:
                return dataTable.str;
            case CharacterStatType.Lung:
                return dataTable.lung;
            case CharacterStatType.Move:
                return dataTable.move;
            case CharacterStatType.Vision:
                return dataTable.vision;
            case CharacterStatType.Dmg:
                return dataTable.dmg;
            case CharacterStatType.Def:
                return dataTable.def;
            case CharacterStatType.Sta:
                return dataTable.sta;
            default:
                return 0;
        }
    }
}
