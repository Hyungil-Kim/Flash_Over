using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class OxygenTankTableData : ItemTableDataBase
{

}
[System.Serializable]
public class OxygenTankTable : MyDataTableBase<OxygenTankTableData>
{
    public List<OxygenTankTableData> normalItem = new List<OxygenTankTableData>();
    public List<OxygenTankTableData> rareItem = new List<OxygenTankTableData>();
    public List<OxygenTankTableData> uniqueItem = new List<OxygenTankTableData>();
    public List<OxygenTankTableData> specialItem = new List<OxygenTankTableData>();

    public List<OxygenTankTableData> regularItem = new List<OxygenTankTableData>();
    public List<OxygenTankTableData> NonRegularItem = new List<OxygenTankTableData>();

    public OxygenTankTable() : base("OxygenTankDataTable")
    {
        foreach (var table in tableList)
        {
            var tableData = new OxygenTankTableData();
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
            tableData.sta = int.Parse(table["STA"]);
            tableData.durability = int.Parse(table["Durability"]);
            tableData.regular = bool.Parse(table["Regular"]);

            tables.Add(tableData);
        }

        normalItem = tables.Where((x) => x.grade == ItemGrade.Normal.ToString()).ToList();
        rareItem = tables.Where((x) => x.grade == ItemGrade.Rare.ToString()).ToList();
        uniqueItem = tables.Where((x) => x.grade == ItemGrade.Unique.ToString()).ToList();
        specialItem = tables.Where((x) => x.grade == ItemGrade.Special.ToString()).ToList();

        regularItem = tables.Where((x) => x.regular).ToList();
        NonRegularItem = tables.Where((x) => !x.regular).ToList();
    }
}
