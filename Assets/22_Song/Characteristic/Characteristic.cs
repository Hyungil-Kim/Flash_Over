using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characteristic
{
}
public class HeavyWeight : Buff
{
    private int prevValue;
    public HeavyWeight(CharacterData characterdata)
    {
        cd = characterdata;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.Move);
    }
    public override bool Check(bool checkCondition = false)
    {
        check = cd.weight < 20;

        base.Check();
        return ing;

    }

    public override void StartBuff()
    {
        if (!ing)
        {
            prevValue = cd.totalStats.move;
            cd.totalStats.move -= 1;
        }
        base.StartBuff();
    }

    public override void WhileBuff()
    {
        if (!ing)
        {
            return;
        }
        base.WhileBuff();
    }
    public override void EndBuff()
    {
        cd.totalStats.move = prevValue;
        base.EndBuff();
    }

}

public class SaveClaimant : Buff
{
    private int prevValue;
    public SaveClaimant(CharacterData characterdata)
    {
        cd = characterdata;
        buffTiming.AddType(BuffTiming.BuffTimingEnum.Move);
    }

    public override bool Check(bool checkCondition = false)
    {
        check = checkCondition;
        base.Check();
        return ing;
    }



    public override void StartBuff()
    {
        if(!ing)
        {
            prevValue = cd.totalStats.str.stat;
            cd.totalStats.str.stat += 50;
        }
        base.StartBuff();
    }

    public override void WhileBuff()
    {
        base.WhileBuff();
    }
    public override void EndBuff()
    {
        cd.totalStats.str.stat = prevValue;
        
        base.EndBuff();
    }


}