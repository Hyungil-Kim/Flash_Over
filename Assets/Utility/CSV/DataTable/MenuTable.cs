using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[System.Serializable]
public class MenuTableData : TableDataBase
{
   public int CS1Count;
   public int CS1Cost;
   public int CS2Grade;
   public int CS2Cost;
   public float CS3Characteristic;
   public int CS3Cost;
   
   public int TS1Str;
   public int TS1Price;
   public int TS1Cost;
   public int TS2Hp;
   public int TS2Price;
   public int TS2Cost;
   public int TS3Lung;
   public int TS3Price;
   public int TS3Cost;
  
   public int RS1Tired;
   public int RS1Cost;
   public int RS2Psychological;
   public int RS2Cost;
   public int RS3Physical;
   public int RS3Cost;
   public int RS4Count;
   public int RS4Cost;
 
   public int IS1Count;
   public int IS1Cost;
   public float IS2Sale;
   public int IS2Cost;
   public int IS3Durability;
   public int IS3Cost;
    
    public int MC1Count;
    public int MC1Cost;
}
[System.Serializable]
public class MenuTable : MyDataTableBase<MenuTableData>
{
    public MenuTable() : base("MenuDataTable")
    {
        foreach (var table in tableList)
        {
            var tableData = new MenuTableData();

            tableData.id = table["ID"];
            tableData.CS1Count = int.Parse(table["CS1Count"]);
            tableData.CS1Cost = int.Parse(table["CS1Cost"]);
            tableData.CS2Grade = int.Parse(table["CS2Grade"]);
            tableData.CS2Cost = int.Parse(table["CS2Cost"]);
            tableData.CS3Characteristic = float.Parse(table["CS3Characteristic"]);
            tableData.CS3Cost = int.Parse(table["CS3Cost"]);

            tableData.TS1Str = int.Parse(table["TS1Str"]);
            tableData.TS1Price = int.Parse(table["TS1Price"]);
            tableData.TS1Cost = int.Parse(table["TS1Cost"]);
            tableData.TS2Hp = int.Parse(table["TS2Hp"]);
            tableData.TS2Price = int.Parse(table["TS2Price"]);
            tableData.TS2Cost = int.Parse(table["TS2Cost"]);
            tableData.TS3Lung = int.Parse(table["TS3Lung"]);
            tableData.TS3Price = int.Parse(table["TS3Price"]);
            tableData.TS3Cost = int.Parse(table["TS3Cost"]);
            
            tableData.RS1Tired = int.Parse(table["RS1Tired"]);
            tableData.RS1Cost = int.Parse(table["RS1Cost"]);
            tableData.RS2Psychological = int.Parse(table["RS2Psychological"]);
            tableData.RS2Cost = int.Parse(table["RS2Cost"]);
            tableData.RS3Physical = int.Parse(table["RS3Physical"]);
            tableData.RS3Cost = int.Parse(table["RS3Cost"]);
            tableData.RS4Count = int.Parse(table["RS4Count"]);
            tableData.RS4Cost = int.Parse(table["RS4Cost"]);

            tableData.IS1Count = int.Parse(table["IS1Count"]);
            tableData.IS1Cost = int.Parse(table["IS1Cost"]);
            tableData.IS2Sale = float.Parse(table["IS2Sale"]);
            tableData.IS2Cost = int.Parse(table["IS2Cost"]);
            tableData.IS3Durability = int.Parse(table["IS3Durability"]);
            tableData.IS3Cost = int.Parse(table["IS3Cost"]);


            tableData.MC1Count = int.Parse(table["MC1Count"]);
            tableData.MC1Cost = int.Parse(table["MC1Cost"]);

            tables.Add(tableData);
        }

    }
}
