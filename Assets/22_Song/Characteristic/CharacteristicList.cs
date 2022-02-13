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
    
    //Haughtiness,

    //StrongMind,
    //Boldness,
    //WidePersPective,
    //FriendShip,
    //MasterOfWeapon,
    //QuickHealing,
    //Hearing,
    //Resilience,
    //Coward,
    //Exaggerating,
    //Laziness,
    //Stubborn,
    //Mute,
    //TooMuchStress,
    //LowSelfEsteem,
    //Heroism,
    //Intelligent,
    //Nimble,
    //Inside,
    //FireTolerance,
    //Stronger,
    //Berserker,

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
}