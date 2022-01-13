using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[System.Serializable]
public static class MyDataTableMgr
{
    //public static WeaponTable weaponTable = new WeaponTable();
    public static ConsumableItemTable consumItemTable = new ConsumableItemTable();

    public static HoseTable hoseTable = new HoseTable();
    public static BunkerGearTable bunkerGearTable = new BunkerGearTable();
    public static OxygenTankTable oxygenTankTable = new OxygenTankTable();

    public static CharacterStatsTable chaStatTable = new CharacterStatsTable();
    public static ItemGradeTable itemGradeTable = new ItemGradeTable();
    public static LevelUpTable levelUpTable = new LevelUpTable();
}