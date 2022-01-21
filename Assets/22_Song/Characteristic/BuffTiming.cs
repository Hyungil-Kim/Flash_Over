using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BuffTiming
{
    public BuffTimingEnum bufftiming;

    [Flags]
    public enum BuffTimingEnum
    {
        TurnStart = 1 << 0,
        TurnEnd = 1 << 1,
        Move = 1 << 2,
    }
    public bool Check(BuffTimingEnum timing)
    {
        return bufftiming.HasFlag(timing);
        //return (bufftiming & timing) != 0;
    }
    public void AddType(int index)
    {
        var personalityName = Enum.GetNames(typeof(BuffTimingEnum))[index];
        bufftiming |= StringToEnum.SToE<BuffTimingEnum>(personalityName);
    }
    public void AddType(BuffTimingEnum timing)
    {
        bufftiming |= timing;
    }
}