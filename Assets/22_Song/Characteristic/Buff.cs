using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBuff
{
    public bool OnGoing;

    public bool InRangeClaimant;
    public bool InRangeFireman;
    public bool InRangeFire;
    [NonSerialized]
    public List<Player> playerList = new List<Player>();
}

public class Buff
{
    //public bool isBad;
    public bool isBase;

    public string id;
    public string name;

    public string timing;
    public BuffTiming buffTiming = new BuffTiming();

    public string desc;

    [NonSerialized]
    public CharacterData cd;

    public bool isCharacteristic;

    public bool isBadCharacteristic;
    public bool isInnate;

    public bool isPhysical;
    public bool isPsychological;

    public float checkValue;
    public float increaseValue;
    public float decreaseValue;

    public bool ing;
    public bool check;

    public CheckBuff checkingCondition = new CheckBuff();
    public CharacteristicData data;
    public void Init()
    {
        id = data.id;

        name = data.name;
        desc = data.desc;

        checkValue = data.checkValue;
        increaseValue = data.increaseStat;
        decreaseValue = data.decreaseStat;

        isBadCharacteristic = data.bad;
        isInnate = data.innate;
        isPhysical = data.physical;
        isPsychological = data.psychological;

    }
    public virtual bool Check(Player player)
    {
        if (check && !ing)
        {
            StartBuff();
            return true;
        }
        else if (!check && ing)
        {
            EndBuff();
            return false;
        }
        return ing;
    }
    public virtual void StartBuff()
    {
        ing = true;
    }
    public virtual void WhileBuff()
    {

    }
    public virtual void EndBuff()
    {
        ing = false;
    }
    public virtual void SetCharacter(CharacterData character)
    {
        cd = character;
    }
}
