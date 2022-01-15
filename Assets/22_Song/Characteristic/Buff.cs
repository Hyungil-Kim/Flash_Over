using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    public string id;
    public string timing;
    public BuffTiming buffTiming;
    public CharacterData cd;
    public bool ing;
    public virtual bool Check()
    {
        return false;
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
}
