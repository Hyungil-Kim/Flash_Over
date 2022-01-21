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

    public static FireTable fireTable = new FireTable();
    public static ObjTable objTable = new ObjTable();
    public static SmokeTable smokeTable = new SmokeTable();
    public static TileTable tileTable = new TileTable();

    public static StageInfoTable stageInfoTable = new StageInfoTable();

    public static HpStatTable hpStatTable = new HpStatTable();
    public static LungStatTable lungStatTable = new LungStatTable();
    public static StrStatTable strStatTable = new StrStatTable();

    public static ClaimantTable claimantTable = new ClaimantTable();
    public static EventTable eventTable = new EventTable();
}
