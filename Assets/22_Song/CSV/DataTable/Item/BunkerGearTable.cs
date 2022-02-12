using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class BunkerGearTableData : ItemTableDataBase
{

}
[System.Serializable]
public class BunkerGearTable : MyDataTableBase<BunkerGearTableData>
{
    public List<BunkerGearTableData> normalItem = new List<BunkerGearTableData>();
    public List<BunkerGearTableData> rareItem = new List<BunkerGearTableData>();
    public List<BunkerGearTableData> uniqueItem = new List<BunkerGearTableData>();
    public List<BunkerGearTableData> specialItem = new List<BunkerGearTableData>();

    public List<BunkerGearTableData> regularItem = new List<BunkerGearTableData>();
    public List<BunkerGearTableData> NonRegularItem = new List<BunkerGearTableData>();
    public BunkerGearTable() : base("BunkerGearDataTable")
    {
        foreach (var table in tableList)
        {
            var tableData = new BunkerGearTableData();
            tableData.type = ItemType.BunkerGear;
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
            tableData.durability = int.Parse(table["Durability"]);
            //tableData.regular = bool.Parse(table["Regular"]);

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
