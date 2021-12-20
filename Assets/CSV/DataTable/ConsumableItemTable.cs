using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[System.Serializable]
public class ConsumableItemTableData : ItemTableDataBase
{
    public string prefabsID;
    public int mp;
    public string statStr;
    public string description;
    public int cost;
    public int hp;
    public int duration;
}
public class ConsumableItemTable : MyDataTableBase<ConsumableItemTableData>
{
    public List<ConsumableItemTableData> normalItem = new List<ConsumableItemTableData>();
    public List<ConsumableItemTableData> rareItem = new List<ConsumableItemTableData>();
    public List<ConsumableItemTableData> uniqueItem = new List<ConsumableItemTableData>();
    public List<ConsumableItemTableData> specialItem = new List<ConsumableItemTableData>();
    public ConsumableItemTable() : base("ConsumDataTable")
    {
        foreach (var table in tableList)
        {
            var tableData = new ConsumableItemTableData();

            tableData.id = table["ID"];
            tableData.iconID = table["ICON_ID"];
            tableData.prefabsID = table["PREFAB_ID"];
            tableData.itemName = table["NAME"];
            tableData.description = table["DESC"];
            tableData.cost = int.Parse(table["COST"]);
            tableData.hp = int.Parse(table["STAT_HP"]);
            tableData.mp = int.Parse(table["STAT_MP"]);
            tableData.statStr = table["STAT_STR"];
            tableData.duration = int.Parse(table["DURATION"]);
            tableData.grade = table["GRADE"];
            tableData.count = 1;
            tableData.iconSprite = Resources.Load<Sprite>($"Sprites/Icons/{tableData.iconID}");

            tables.Add(tableData);
        }
        normalItem = tables.Where((x) => x.grade == ItemGrade.Normal.ToString()).ToList();
        rareItem = tables.Where((x) => x.grade == ItemGrade.Rare.ToString() || x.grade == ItemGrade.Normal.ToString()).ToList();
        uniqueItem = tables.Where((x) => x.grade != ItemGrade.Special.ToString()).ToList();
        specialItem = tables;
    }
}
