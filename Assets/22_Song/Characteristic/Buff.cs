using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    public string id;
    public string timing;
    public BuffTiming buffTiming = new BuffTiming();

    [NonSerialized]
    public CharacterData cd;

    public bool ing;
    public bool check;

    public virtual bool Check(bool checkCondision = false)
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
