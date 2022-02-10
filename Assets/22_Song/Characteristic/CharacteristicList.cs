using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacteristicList
{
    HeavyWeight,
    SaveClamant,
    Haughtiness,

    StrongMind,
    Boldness,
    WidePersPective,
    FriendShip,
    MasterOfWeapon,
    QuickHealing,
    Hearing,
    Resilience,
    Coward,
    Exaggerating,
    Laziness,
    Stubborn,
    Mute,
    TooMuchStress,
    LowSelfEsteem,
    Heroism,
    Intelligent,
    Nimble,
    Inside,
    FireTolerance,
    Stronger,
    Berserker,

}

public static class TotalCharacteristic
{
    public static List<Buff> list = new List<Buff>();
    public static List<CharacteristicList> innateList = new List<CharacteristicList>();
    public static void Init()
    {
        innateList.Add(CharacteristicList.StrongMind);
        innateList.Add(CharacteristicList.WidePersPective);
        innateList.Add(CharacteristicList.FriendShip);
        innateList.Add(CharacteristicList.MasterOfWeapon);
        innateList.Add(CharacteristicList.QuickHealing);
        innateList.Add(CharacteristicList.Coward);
        innateList.Add(CharacteristicList.Laziness);
        innateList.Add(CharacteristicList.LowSelfEsteem);
        innateList.Add(CharacteristicList.Intelligent);
        innateList.Add(CharacteristicList.Nimble);
        innateList.Add(CharacteristicList.FireTolerance);
        innateList.Add(CharacteristicList.Stronger);
        innateList.Add(CharacteristicList.Stronger);


    }
}