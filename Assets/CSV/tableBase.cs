using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[System.Serializable]
public class TableDataBase
{
    public string id;
}
//[System.Serializable]
public class ItemTableDataBase : TableDataBase
{
    public string iconID;
    public string itemName;
    [System.NonSerialized]
    public Sprite iconSprite;

    public string grade;
    public int count;
}
