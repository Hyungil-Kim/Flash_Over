using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
[System.Serializable]
public class TableDataBase
{
    public string id;
}
[System.Serializable]
public class ItemTableDataBase : TableDataBase
{
    public string iconID;
    public string itemName;
    [JsonIgnore]
    public Sprite iconSprite
    {
        get
        {
            return Resources.Load<Sprite>($"Sprites/Icons/{iconID}");
        }
    }
    public string grade;
    public int weight;
    public int price;

    public int hp;
    public int str;
    public int lung;
    public int dmg;
    public int def;
    public int move;
    public int vision;
    public int sta;
    public int durability;
}
public class StatTableDataBase : TableDataBase
{
    public int increaseStat;
    public int maxexp;
}