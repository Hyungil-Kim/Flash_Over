using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    public string id;
    public string timing;
    public BuffTiming buffTiming;
    

    public virtual bool Cheak()
    {
        return false;
    }
    public virtual void StartBuff()
    {

    }
    public virtual void WhileBuff()
    {

    }
    public virtual void EndBuff()
    {

    }
}
