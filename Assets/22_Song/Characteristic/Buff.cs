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

    public List<Player> playerList = new List<Player>();
}

public class Buff
{
    public bool isBad;
    
    public string id;
    public string name;

    public string timing;
    public BuffTiming buffTiming = new BuffTiming();

    [NonSerialized]
    public CharacterData cd;

    public bool isCharacteristic;
    public bool isBadCharacteristic;
    public bool isInnate;

    public bool ing;
    public bool check;

    public CheckBuff checkingCondition = new CheckBuff();

    public virtual bool Check(Player player)
    {
        if (check)
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
