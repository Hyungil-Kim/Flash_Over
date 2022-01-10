using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class WeaponTableData : ItemTableDataBase
{
}

[System.Serializable]
public class WeaponTable : MyDataTableBase<WeaponTableData>
{
    public List<WeaponTableData> normalItem = new List<WeaponTableData>();
    public List<WeaponTableData> rareItem = new List<WeaponTableData>();
    public List<WeaponTableData> uniqueItem = new List<WeaponTableData>();
    public List<WeaponTableData> specialItem = new List<WeaponTableData>();
    public WeaponTable() : base("HoseDataTable")
    {
        foreach (var table in tableList)
        {
            var tableData = new WeaponTableData();
            tableData.id = table["ID"];
            tableData.iconID = table["ICON_ID"];
            tableData.itemName = table["NAME"];
            tableData.grade = table["GRADE"];
            tableData.weight = int.Parse(table["WEIGHT"]);
            tableData.price = int.Parse(table["PRICE"]);

            tableData.hp = int.Parse(table["HP"]);
            tableData.str = int.Parse(table["STR"]);
            tableData.lung = int.Parse(table["LUNG"]);
            tableData.dmg = int.Parse(table["DMG"]);
            tableData.def = int.Parse(table["DEF"]);
            tableData.move = int.Parse(table["MOVE"]);
            tableData.vision = int.Parse(table["VISION"]);

            tables.Add(tableData);
        }

        normalItem = tables.Where((x) => x.grade == ItemGrade.Normal.ToString()).ToList();
        rareItem = tables.Where((x) => x.grade == ItemGrade.Rare.ToString()).ToList();
        uniqueItem = tables.Where((x) => x.grade == ItemGrade.Unique.ToString()).ToList();
        specialItem = tables.Where((x) => x.grade == ItemGrade.Special.ToString()).ToList();
    }
}
