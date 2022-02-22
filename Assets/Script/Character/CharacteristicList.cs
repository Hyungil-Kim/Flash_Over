using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacteristicList
{
    SaveClamant,
    Friendship,
    Excited,
    Movefaster,
    Stressfull,
    Ptsd,
}
public static class TotalCharacteristic
{
    public static List<Buff> list = new List<Buff>();
    public static List<CharacteristicList> innateList = new List<CharacteristicList>();
    public static void Init()
    {
        innateList.Add(CharacteristicList.Excited);
        innateList.Add(CharacteristicList.Movefaster);
        innateList.Add(CharacteristicList.SaveClamant);
    }
    public static string CharacteristicName(CharacteristicList type)
    {
        return MyDataTableMgr.characteristicTable.GetTable(type.ToString()).name;
    }
}