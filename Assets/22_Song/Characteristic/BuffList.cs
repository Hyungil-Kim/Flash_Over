using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testBuff : Buff
{

    public testBuff(CharacterData characterdata)
    {
        cd = characterdata;
    }
    public override bool Check()
    {
        if(cd.weight < 20 )
        {
            StartBuff();
            return true;
        }

        EndBuff();
        return false;

    }

    public override void StartBuff()
    {
        if(!ing)
        cd.totalStats.move -= 1;

        base.StartBuff();
    }

    public override void WhileBuff()
    {
        if(!ing)
        {
            return;
        }
        base.WhileBuff();
    }
    public override void EndBuff()
    {
        base.EndBuff();
    }
}